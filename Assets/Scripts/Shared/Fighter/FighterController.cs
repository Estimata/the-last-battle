using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class FighterController : MonoBehaviour
{
    public Animator Animator;
    public Stats Stats;
    public NavMeshAgent Agent;
    [SerializeField] private FighterData _fighterData;
    [SerializeField] private NavAgentMovement agentMovement;
    [SerializeField] private Rotation _rotation;
    [SerializeField] private Health _health;

    private StateMachine<FighterController> _fighterState;
    public FighterStandby StandbyState { get; private set; }
    public FighterReturn ReturnState { get; private set; }
    public FighterGoTo GoToTargetState { get; private set; }
    public FighterDead FighterDeadState { get; private set; }

    public bool IsAlly;
    public Transform Target { get; private set; }
    private Vector3 _battlePosition;
    private float _turningSpeed = 5;

    private void Awake() {
        InitializeStates();
        _health.Initialize(_fighterData);
        //Sementara tidak ada stat Modifier
        Stats.Initialize(_fighterData);

    }

    public void InitializeStates()
    {
        StandbyState = new FighterStandby();
        ReturnState = new FighterReturn();
        GoToTargetState = new FighterGoTo();
        FighterDeadState = new FighterDead();

        _fighterState = new StateMachine<FighterController>(this, StandbyState, false);
        
    }

    private void Update() {
        _fighterState.Update();
    }

    public void InitiatePosition(Vector3 position)
    {
        _battlePosition = position;
        InterruptState(ReturnState);
        
    }

    public async Task GoToTarget(Transform target)
    {
        Target = target;
        InterruptState(GoToTargetState);
        await _fighterState.WaitForState(StandbyState);
    }

    public async Task ReturnToBattlePosition()
    {
        InterruptState(ReturnState);
        await _fighterState.WaitForState(StandbyState);
    }

    public void ChangeState(IState<FighterController> state) => _fighterState.ChangeState(state);
    public void InterruptState(IState<FighterController> state) => _fighterState.Interrupt(state);

    public void SetDestination(Vector3 target) => Agent.SetDestination(target);
    public void Return() => Agent.SetDestination(_battlePosition);
    public bool HasArrived() => agentMovement.HasArrived();

    public void LookForward() => _rotation.LookForward(Agent.velocity.normalized, _turningSpeed);
    public void LockIn() => _rotation.transform.LookAt(Target.position);

    public bool HasTarget() => Target != null;
    public void SetTarget(Transform target) => Target = target;

    public FighterData GetFighterData() => _fighterData;
    public FighterAction GetBasicAction() => _fighterData.BasicAction;
    public List<FighterAction> GetSkills() => _fighterData.Skills;

    public int GetCurrentHP() => _health.CurrentHP;
    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);
        Animator.SetTrigger("Hit");
        if (IsDead())
        {
            InterruptState(FighterDeadState);
        }
    }
    public bool IsDead() => _health.IsDead();
    public void DestroyFighter() => Destroy(gameObject);

    private void BattleEntered() => _fighterState.Enable();
    private void BattleExited() {
        if (IsDead()) return;

        _fighterState.Disable();
    }
    private void OnEnable() {
        BattleInitiator.OnBattleInitiated += BattleEntered;
        BattleController.OnBattleEnded += BattleExited;
    }
    private void OnDisable() {
        BattleInitiator.OnBattleInitiated -= BattleEntered;
        BattleController.OnBattleEnded -= BattleExited;
    }
}
