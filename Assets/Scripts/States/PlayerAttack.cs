using UnityEngine;

public class PlayerAttack : State<PlayerController>
{
    private int _attackHash = Animator.StringToHash("Attack");
    private int _velocityHash = Animator.StringToHash("Velocity");
    public override void Enter(PlayerController player)
    {
        player.Animator.SetTrigger(_attackHash);
    }

    public override void UpdateState(PlayerController player)
    {
        Vector2 moveInput = player.MoveAction.action.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);

        player.MoveLookDirection(moveDirection, player.RunningSpeed);
        
        player.Animator.SetFloat(_velocityHash, moveInput.magnitude);

        AnimatorStateInfo stateInfo = player.Animator.GetCurrentAnimatorStateInfo(1);
        if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 0.9f)
        {
            if (player.Velocity == Vector3.zero)
            {
                player.StateMachine.ChangeState(player.MoveState);
            }
            else
            {
                player.StateMachine.ChangeState(player.IdleState);
            }
        }
    }

    public override bool CanBeInterrupted(IState<PlayerController> newState)
    {
        // TODO add spesific condition
        return false;
    }
}
