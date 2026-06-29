using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private LookDirectionController _lookDirection;

    private bool _inBattle = false;

    private void OnLook(InputValue lookDirection)
    {
        if (!_inBattle) _lookDirection.Look(lookDirection.Get<Vector2>());
    }
    private void OnAttack(){
       if (!_inBattle) _player.InterruptState(_player.AttackState);
    }

    private void BattleEntered() => _inBattle = true;
    private void OnEnable() => BattleInitiator.OnBattleInitiated += BattleEntered;
    private void OnDisable() => BattleInitiator.OnBattleInitiated -= BattleEntered;
}
