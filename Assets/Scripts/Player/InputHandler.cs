using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private LookDirectionController _lookDirection;

    void OnLook(InputValue lookDirection) => _lookDirection.Look(lookDirection.Get<Vector2>());
    void OnAttack() => _player.InterruptState(_player.AttackState);
}
