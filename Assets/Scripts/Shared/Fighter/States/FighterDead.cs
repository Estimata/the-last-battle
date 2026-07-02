using UnityEngine;
using System;

public class FighterDead : State<FighterController>
{
    private int _DeadHash = Animator.StringToHash("Dead");
    private float _remainDuration = 60f;
    private float _timer = 0f;
    public override void Enter(FighterController fighter)
    {
        fighter.Animator.SetBool(_DeadHash, true);
        
    }

    public override void Update(FighterController fighter)
    {
        _timer += Time.deltaTime;
        if (_timer >= _remainDuration)
        {
            fighter.DestroyFighter();
        }
    }

    public override bool CanBeInterrupted(IState<FighterController> newState)
    {
        return false;
    }
}
