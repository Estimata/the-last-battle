using UnityEngine;

public class PlayerAttack : State<PlayerController>
{
    private int _attackHash = Animator.StringToHash("Attack");
    private int _velocityHash = Animator.StringToHash("Velocity");
    public override void Enter(PlayerController player)
    {
        player.Animator.SetTrigger(_attackHash);
        player.WeaponHitBox.enabled = true;
    }

    public override void Update(PlayerController player)
    {
        Vector2 moveInput = player.MoveAction.action.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);

        player.MoveLookDirection(moveDirection, player.RunningSpeed);
        
        player.Animator.SetFloat(_velocityHash, moveInput.magnitude);

        AnimatorStateInfo stateInfo = player.Animator.GetCurrentAnimatorStateInfo(1);
        if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 0.9f)
        {
            if (player.CharacterController.velocity != Vector3.zero)
            {
                player.PlayerState.ChangeState(player.MoveState);
            }
            else
            {
                player.PlayerState.ChangeState(player.IdleState);
            }
        }
    }

    public override void Exit(PlayerController player)
    {
        player.WeaponHitBox.enabled = false;
    }
    
    public override bool CanBeInterrupted(IState<PlayerController> newState)
    {
        // TODO Tambahkan kondisi yang lebih spesifik
        return false;
    }
}
