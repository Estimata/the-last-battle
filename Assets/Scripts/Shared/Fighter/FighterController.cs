using UnityEngine;
using UnityEngine.AI;

public class FighterController : MonoBehaviour
{
    public Animator Animator;
    public Stats Stats;
    [SerializeField] private FighterData _fighterData;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private NavAgentMovement agentMovement;
    [SerializeField] private Rotation _rotation;
    [SerializeField] private Health _health;

    private StateMachine<FighterController> _fighterState;
    public FighterStandby StandbyState { get; private set; }
    public FighterReturn ReturnState { get; private set; }
    public bool IsAlly;
    private Transform _target;
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

    public void ChangeState(IState<FighterController> state) => _fighterState.ChangeState(state);
    public void InterruptState(IState<FighterController> state) => _fighterState.Interrupt(state);

    public void SetDestination(Vector3 target) => _agent.SetDestination(target);
    public void Return() => _agent.SetDestination(_battlePosition);
    public bool HasArrived() => agentMovement.HasArrived();

    public void LookForward() => _rotation.LookForward(_agent.velocity.normalized, _turningSpeed);
    public void LockIn() => _rotation.transform.LookAt(_target.position);

    public bool HasTarget() => _target != null;
    public void SetTarget(Transform target) => _target = target;

    private void BattleEntered() => _fighterState.Enable();
    private void OnEnable() {
        BattleInitiator.OnBattleInitiated += BattleEntered;
    }
}
