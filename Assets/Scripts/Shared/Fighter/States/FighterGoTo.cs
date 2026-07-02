using UnityEngine;

public class FighterGoTo : State<FighterController>
{
    private int _velocityHash = Animator.StringToHash("Velocity");
    public override void Enter(FighterController fighter)
    {
        Vector3 dir = (fighter.transform.position - fighter.Target.position).normalized;
        Vector3 destination = fighter.Target.position + dir * 2f;
        fighter.SetDestination(destination);
    }

    public override void Update(FighterController fighter)
    {
        fighter.Animator.SetFloat(_velocityHash, 1);
        
        fighter.LockIn();
        if (fighter.HasArrived())
        {
            fighter.ChangeState(fighter.StandbyState);
        }
    }

    public override void Exit(FighterController fighter)
    {
        fighter.Animator.SetFloat(_velocityHash, 0);
    }
}