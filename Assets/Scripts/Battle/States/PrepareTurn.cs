using UnityEngine;
using System.Collections.Generic;

public class PrepareTurn : State<BattleController>
{
    public override void Enter(BattleController battle)
    {
        List<FighterController> initiatedQueue =  battle.CreateQueue();
        battle.BattleUI.RegisterFighterTurn(initiatedQueue, OnComplete);
    }

    private void OnComplete()
    {
        Debug.Log("Do Something");
    }
}
