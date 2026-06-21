using UnityEngine;

public class PlayerRun : State<PlayerController>
{
    public override void UpdateState(PlayerController player)
    {
        Vector2 _moveDirection = player.MoveAction.action.ReadValue<Vector2>();

        player.Animator.SetFloat("Velocity", _moveDirection.magnitude);
        
        Vector3 move = new Vector3(_moveDirection.x, 0, _moveDirection.y);
        player.CharacterController.Move(move * player.RunningSpeed * Time.deltaTime);
        if (_moveDirection == Vector2.zero)
        {
            player.StateMachine.ChangeState(player.IdleState);
        }

    }
}
