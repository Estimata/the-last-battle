using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private LookDirectionController _lookDirection;
    private InputSystem_Actions _controls;

    private void Awake() => _controls = new InputSystem_Actions();

    private void OnLook(InputAction.CallbackContext lookDirection) => _lookDirection.Look(lookDirection.ReadValue<Vector2>());
    private void OnAttack(InputAction.CallbackContext context) => _player.InterruptState(_player.AttackState);

    private void BattleEntered() => _controls.Player.Disable();
    private void BattleExited() => _controls.Player.Enable();

    private void OnEnable()
    { 
        _controls.Player.Enable();
        _controls.Player.Attack.performed += OnAttack;
        _controls.Player.Look.performed += OnLook;
        BattleInitiator.OnBattleInitiated += BattleEntered;
        BattleController.OnBattleEnded += BattleExited;
    }
    
    private void OnDisable() 
    {
        _controls.Player.Disable();
        _controls.Player.Attack.performed -= OnAttack;
        _controls.Player.Look.performed -= OnLook;
        BattleInitiator.OnBattleInitiated -= BattleEntered;
        BattleController.OnBattleEnded -= BattleExited;
    }
}
