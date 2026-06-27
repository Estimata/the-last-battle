using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Animator Animator;
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] NavAgentMovement _agentMovement;
    [SerializeField] Rotation _rotation;

    private StateMachine<EnemyController> _enemyState;
    public EnemyRoam RoamState { get; private set; }
    
    public float _turningSpeed = 5f;

    private void Awake()
    {
        InitializeStates();
    }

    
    public void InitializeStates()
    {
        RoamState = new EnemyRoam();

        _enemyState = new StateMachine<EnemyController>(this, RoamState);
        
    }

    private void Update() {
        _enemyState.Update();
    }

    public void ChangeState(IState<EnemyController> state) => _enemyState.ChangeState(state);
    public void InterruptState(IState<EnemyController> state) => _enemyState.Interrupt(state);
    private void BattleEntered() => _enemyState.Disable();

    public void SetDestination(Vector3 target) => _agent.SetDestination(target);
    public bool HasArrived() => _agentMovement.HasArrived();

    public void LookForward() => _rotation.LookForward(_agent.velocity.normalized, _turningSpeed);

    private void OnEnable() {
        BattleInitiator.OnBattleInitiated += BattleEntered;
    }
}
