using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Animator Animator;
    public BoxCollider WeaponHitBox;

    [SerializeField] Movement _movement;
    [SerializeField] LookDirectionController _lookDirection;
    [SerializeField] Rotation _rotation;

    public InputActionReference MoveAction;

    private StateMachine<PlayerController> _playerState;
    public PlayerIdle IdleState { get; private set; }
    public PlayerMove MoveState { get; private set; }
    public PlayerAttack AttackState { get; private set; }

    public Transform _target;    
    public float _runningSpeed = 5f;
    public float _turningSpeed = 5f;
    public float AttackCooldown = 1f;
    private float _attackCooldownTimer = 0f;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        InitializeStates();
    }
    
    public void InitializeStates()
    {
        IdleState = new PlayerIdle();
        MoveState = new PlayerMove();
        AttackState = new PlayerAttack();

        _playerState = new StateMachine<PlayerController>(this, IdleState);
    }

    void Update()
    {
        _playerState.Update();
        HandleTimer();
    }

    void HandleTimer()
    {
        if (_attackCooldownTimer > 0) _attackCooldownTimer -= Time.deltaTime;
    }
    
    public void ChangeState(IState<PlayerController> state) => _playerState.ChangeState(state);
    public void InterruptState(IState<PlayerController> state) => _playerState.Interrupt(state);
    private void BattleEntered()
    {
        _target = null;
        _playerState.Disable();
    }
    
    public bool HasTarget() => _target != null;

    public void Move(Vector3 moveDirection) => _movement.Move(_lookDirection.transform, moveDirection, _runningSpeed);
    public float GetMovementLean() => _movement.GetMovementLean(_rotation.transform);
    public Vector3 GetVelocity() => _movement.Velocity;
    
    public void LookForward() => _rotation.LookForward(_movement.Velocity.normalized, _turningSpeed);
    public void LockIn() => _rotation.transform.LookAt(_target.position);

    private void OnEnable() => BattleInitiator.OnBattleInitiated += BattleEntered;
    private void OnDisable() => BattleInitiator.OnBattleInitiated -= BattleEntered;
}
