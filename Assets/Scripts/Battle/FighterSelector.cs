using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class FighterSelector : MonoBehaviour
{
    public event Action<FighterController> OnFighterSelected;
    [SerializeField] private Camera _camera;
    private InputSystem_Actions _controls;
    public FighterController SelectedFighter { get; set; }
    
    private void Awake() {
        _controls = new InputSystem_Actions();
        _controls.Battle.Disable();
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        Vector2 target = _controls.Battle.Point.ReadValue<Vector2>();
        Ray ray = _camera.ScreenPointToRay(target);
        
        if (!Physics.Raycast(ray, out RaycastHit hit)) return;
        if (!hit.collider.TryGetComponent(out FighterController fighter)) return;
        if (fighter == SelectedFighter) return;

        SelectedFighter = fighter;
        OnFighterSelected?.Invoke(fighter);
    }

    public void SetFighter(FighterController fighter)
    {
        SelectedFighter = fighter;
        OnFighterSelected?.Invoke(fighter);
    }    

    private void BattleEntered() => _controls.Battle.Enable();
    private void OnEnable()
    {
        _controls.Battle.Click.performed += OnClick;
        BattleInitiator.OnBattleInitiated += BattleEntered;
    }
    private void OnDisable() {
        
        _controls.Battle.Click.performed -= OnClick;
        BattleInitiator.OnBattleInitiated -= BattleEntered;
    }
}
