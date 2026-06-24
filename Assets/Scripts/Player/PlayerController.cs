using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Animator Animator;
    public CharacterController CharacterController;
    public BoxCollider WeaponHitBox;
    public Transform Model;
    public Transform LookDirection;

    public InputActionReference MoveAction;
    public InputActionReference LookAction;

    public StateMachine<PlayerController> PlayerState { get; private set; }
    public PlayerIdle IdleState { get; private set; }
    public PlayerMove MoveState { get; private set; }
    public PlayerAttack AttackState { get; private set; }
    
    public bool GravityEnabled = true;
    public float RunningSpeed = 5f;
    public float Acceleration = 1f;
    public float Deceleration = 0.1f;
    public float TurningSpeed = 5f;
    public float AttackCooldown = 1f;
    private float _attackCooldownTimer = 0f;
    [SerializeField] private float _mouseSensitivity = 100f;
    float _yaw = 0f;
    float _pitch = 0f;

    void Awake()
    {
        IdleState = new PlayerIdle();
        MoveState = new PlayerMove();
        AttackState = new PlayerAttack();

        PlayerState = new StateMachine<PlayerController>(this, IdleState);
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        PlayerState.Update();
        HandleLookDirection();
        HandleGravity();
        HandleTimer();
    }
    
    //Function untuk menggerakkan player relatif arah pandang player (arah kamera)
    public void MoveLookDirection(Vector3 moveDirection, float speed)
    {
        Vector3 lookDirection = LookDirection.TransformDirection(moveDirection);

        lookDirection.y = 0f;
        Vector3 velocity = lookDirection * RunningSpeed;
        
        CharacterController.Move(velocity * Time.deltaTime);

        if (lookDirection != Vector3.zero)
        {
            Model.transform.rotation = Quaternion.Slerp(
                Model.transform.rotation, 
                Quaternion.LookRotation(lookDirection), 
                TurningSpeed * Time.deltaTime);
        }
    }

    void HandleLookDirection()
    {
        Vector2 lookInput = LookAction.action.ReadValue<Vector2>();
        float mouseX = lookInput.x * _mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * _mouseSensitivity * Time.deltaTime;

        _yaw += mouseX;
        _pitch -= mouseY;

        _pitch = Mathf.Clamp(_pitch, -25f, 80f);

        LookDirection.localRotation = Quaternion.Euler(_pitch, _yaw, 0);
    }

    void HandleGravity()
    {
        if (GravityEnabled && !CharacterController.isGrounded)
        {
            CharacterController.Move(Physics.gravity * Time.deltaTime);
        }
    }

    void HandleTimer()
    {
        if (_attackCooldownTimer > 0) _attackCooldownTimer -= Time.deltaTime;
    }

    void OnAttack()
    {
        if (_attackCooldownTimer > 0) return;

        _attackCooldownTimer = AttackCooldown;
        PlayerState.Interrupt(AttackState);
    }
}
