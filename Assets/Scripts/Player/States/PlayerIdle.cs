using UnityEngine;

public class PlayerIdle : State<PlayerController>
{
    private int _velocityHash = Animator.StringToHash("Velocity");

    public override void Update(PlayerController player)
    {
        Vector2 _moveDirection = player.MoveAction.action.ReadValue<Vector2>();

        player.Animator.SetFloat(_velocityHash, _moveDirection.magnitude);
        if (_moveDirection != Vector2.zero)
        {
            player.PlayerState.ChangeState(player.MoveState);
        }
    }
}
