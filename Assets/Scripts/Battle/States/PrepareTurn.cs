using UnityEngine;
using System.Collections.Generic;

public class PrepareTurn : State<BattleController>
{
    private BattleController _battle;
    public override void Enter(BattleController battle)
    {
        _battle = battle;
        List<FighterController> initiatedQueue =  battle.CreateQueue();
        battle.BattleUI.RegisterFighterTurn(initiatedQueue, OnComplete);
    }

    private void OnComplete()
    {
        FighterController fighterTurn = _battle.GetFighterTurn();
        if (fighterTurn.IsAlly)
        {
            _battle.ChangeState(_battle.PlayerTurnState);
        }
        else
        {
            _battle.ChangeState(_battle.EnemyTurnState);
        }
    }
}
