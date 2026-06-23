using UnityEngine;

public class PlayerMove : State<PlayerController>
{
    private int _velocityHash = Animator.StringToHash("Velocity");
    public override void UpdateState(PlayerController player)
    {
        Vector2 moveInput = player.MoveAction.action.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);

        player.MoveLookDirection(moveDirection, player.RunningSpeed);

        player.Animator.SetFloat(_velocityHash, moveInput.magnitude);
        
        if (moveInput.magnitude < 0.01f)
        {
            player.StateMachine.ChangeState(player.IdleState);
        }

    }
}
