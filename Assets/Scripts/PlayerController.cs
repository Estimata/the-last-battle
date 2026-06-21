using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Animator Animator { get; private set; }
    public CharacterController CharacterController { get; private set; }

    public float RunningSpeed = 5f;
    
    public InputActionReference MoveAction;

    public StateMachine<PlayerController> StateMachine { get; private set; }
    public PlayerIdle IdleState { get; private set; }
    public PlayerRun RunState { get; private set; }

    void Awake()
    {
        Animator = GetComponent<Animator>();
        CharacterController = GetComponent<CharacterController>();
        
        IdleState = new PlayerIdle();
        RunState = new PlayerRun();
        StateMachine = new StateMachine<PlayerController>(this, IdleState);
    }

    void Update()
    {
        StateMachine.Update();
    }
}
