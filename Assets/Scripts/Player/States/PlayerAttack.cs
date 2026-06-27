using UnityEngine;

public class PlayerAttack : State<PlayerController>
{
    private int _attackHash = Animator.StringToHash("Attack");
    private int _velocityHash = Animator.StringToHash("Velocity");
    private int _horizontalDirectionHash = Animator.StringToHash("HorizontalDirection");
    public override void Enter(PlayerController player)
    {
        player.Animator.SetTrigger(_attackHash);
        player.WeaponHitBox.enabled = true;
    }

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
                player.Animator.SetFloat(_horizontalDirectionHash, horizontal == 0 ? 0 : Mathf.Sign(horizontal));
            }
            else
            {
                player.Animator.SetLayerWeight(2, 0f);
                player.Animator.SetFloat(_velocityHash, moveInput.magnitude);
            }

        }
        else
        {
            player.LookForward();
            player.Animator.SetFloat(_velocityHash, moveInput.magnitude);   
        }

        AnimatorStateInfo stateInfo = player.Animator.GetCurrentAnimatorStateInfo(1);
        if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 0.9f)
        {
            if (player.GetVelocity() != Vector3.zero)
            {
                player.ChangeState(player.MoveState);
            }
            else
            {
                player.ChangeState(player.IdleState);
            }
        }
    }

    public override void Exit(PlayerController player)
    {
        player.WeaponHitBox.enabled = false;
        player.Animator.SetLayerWeight(2, 0f);

    }
    
    public override bool CanBeInterrupted(IState<PlayerController> newState)
    {
        // TODO Tambahkan kondisi yang lebih spesifik
        return false;
    }
}
