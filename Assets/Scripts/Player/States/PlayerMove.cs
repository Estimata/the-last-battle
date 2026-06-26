using UnityEngine;

public class PlayerMove : State<PlayerController>
{
    private int _velocityHash = Animator.StringToHash("Velocity");
    private int _horizontalDirectionHash = Animator.StringToHash("HorizontalDirection");

    public override void Update(PlayerController player)
    {
        Vector2 moveInput = player.MoveAction.action.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);

        player.Move(moveDirection);

        if (player.HasTarget())
        {
            player.LockIn();
            float horizontal = player.GetMovementLean();
            if (Mathf.Abs(horizontal) >= 0.3){
                player.Animator.SetLayerWeight(2, 1f);
            }
            else
            {
                player.Animator.SetLayerWeight(2, 0f);
                
            }

            player.Animator.SetFloat(_horizontalDirectionHash, horizontal == 0 ? 0 : Mathf.Sign(horizontal));
        }
        else
        {        
            player.LookForward();
            player.Animator.SetFloat(_velocityHash, moveInput.magnitude);
        }

        if (moveInput.magnitude < 0.01f)
        {
            player.ChangeState(player.IdleState);
        }
    }
}
