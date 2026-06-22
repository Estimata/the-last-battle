using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Animator Animator;
    public CharacterController CharacterController;
    public Transform Model;
    public Transform LookDirection;

    public InputActionReference MoveAction;
    public InputActionReference LookAction;

    public StateMachine<PlayerController> StateMachine { get; private set; }
    public PlayerIdle IdleState { get; private set; }
    public PlayerRun RunState { get; private set; }
    
    public bool GravityEnabled = true;
    public float RunningSpeed = 5f;
    public float TurningSpeed = 5f;
    [SerializeField] private float _mouseSensitivity = 100f;
    float _yaw = 0f;
    float _pitch = 0f;

    void Awake()
    {
        IdleState = new PlayerIdle();
        RunState = new PlayerRun();

        StateMachine = new StateMachine<PlayerController>(this, IdleState);
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        StateMachine.Update();
        _handleLookDirection();
        _handleGravity();
    }
    
    void _handleLookDirection()
    {
        Vector2 lookInput = LookAction.action.ReadValue<Vector2>();
        float mouseX = lookInput.x * _mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * _mouseSensitivity * Time.deltaTime;

        _yaw += mouseX;
        _pitch -= mouseY;

        _pitch = Mathf.Clamp(_pitch, -40f, 80f);

        LookDirection.localRotation = Quaternion.Euler(_pitch, _yaw, 0);
    }

    void _handleGravity()
    {
        if (GravityEnabled && !CharacterController.isGrounded)
        {
            CharacterController.Move(Physics.gravity * Time.deltaTime);
        }
    }
}
