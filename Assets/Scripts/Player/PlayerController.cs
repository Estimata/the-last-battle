using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Animator Animator;
    public BoxCollider WeaponHitBox;
    private Transform _model;

    [SerializeField] Movement _movement;
    [SerializeField] LookDirection _lookDirection;
    [SerializeField] Rotation _rotation;

    public InputActionReference MoveAction;

    private StateMachine<PlayerController> _playerState;
    public PlayerIdle IdleState { get; private set; }
    public PlayerMove MoveState { get; private set; }
    public PlayerAttack AttackState { get; private set; }

    public Transform _target;    
    public float _runningSpeed = 5f;
    public float _turningSpeed = 5f;
    public float Acceleration = 1f;
    public float Deceleration = 0.1f;
    public float AttackCooldown = 1f;
    private float _attackCooldownTimer = 0f;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

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
    public bool HasTarget() => _target != null;
    public void Move(Vector3 moveDirection) => _movement.Move(_lookDirection.transform, moveDirection, _runningSpeed);
    public float GetMovementLean() => _movement.GetMovementLean(_rotation.transform);
    public Vector3 GetVelocity() => _movement.Velocity;
    public void LookForward() => _rotation.LookForward(_movement.Velocity.normalized, _turningSpeed);
    public void LockIn() => _rotation.transform.LookAt(_target.position);
}
