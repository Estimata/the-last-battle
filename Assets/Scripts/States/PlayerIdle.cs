using UnityEngine;

public class PlayerIdle : State<PlayerController>
{
    public override void UpdateState(PlayerController player)
    {
        Vector2 _moveDirection = player.MoveAction.action.ReadValue<Vector2>();

        player.Animator.SetFloat("Velocity", _moveDirection.magnitude);
        if (_moveDirection != Vector2.zero)
        {
            player.StateMachine.ChangeState(player.MoveState);
        }
    }
}
