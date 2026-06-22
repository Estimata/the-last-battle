using UnityEngine;

public class PlayerRun : State<PlayerController>
{
    private int _velocityHash = Animator.StringToHash("Velocity");
    public override void UpdateState(PlayerController player)
    {
        Vector2 moveInput = player.MoveAction.action.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
        Vector3 lookDirection = player.LookDirection.TransformDirection(moveDirection);

        lookDirection.y = 0f;
        player.CharacterController.Move(lookDirection.normalized * player.RunningSpeed * Time.deltaTime);
        if (lookDirection != Vector3.zero)
        {
            player.Model.transform.rotation = Quaternion.Slerp(
                player.Model.transform.rotation, 
                Quaternion.LookRotation(lookDirection), 
                player.TurningSpeed * Time.deltaTime);
        }
            
        player.Animator.SetFloat(_velocityHash, moveInput.magnitude);

        if (moveInput.magnitude < 0.01f)
        {
            player.StateMachine.ChangeState(player.IdleState);
        }

    }
}
