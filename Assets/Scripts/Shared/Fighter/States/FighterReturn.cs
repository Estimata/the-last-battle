using UnityEngine;

public class FighterReturn : State<FighterController>
{
    private int _velocityHash = Animator.StringToHash("Velocity");
    
    public override void Enter(FighterController fighter)
    {
        fighter.Return();
        fighter.Animator.SetFloat(_velocityHash, 1f);
    }

    public override void Update(FighterController fighter)
    {
        fighter.LookForward();
        if (fighter.HasArrived())
        {
            fighter.ChangeState(fighter.StandbyState);
        }
    }

    public override void Exit(FighterController fighter)
    {
        fighter.Animator.SetFloat(_velocityHash, 0f);
    }
}
