using UnityEngine;
using System.Collections.Generic;

public class PrepareTurn : State<BattleController>
{
    public async override void Enter(BattleController battle)
    {
        List<FighterController> initiatedQueue = battle.CreateQueue();
        await battle.BattleUI.RegisterFighterTurn(initiatedQueue);
        FighterController fighterTurn = battle.GetFighterTurn();
        if (fighterTurn.IsAlly)
        {
            battle.ChangeState(battle.PlayerTurnState);
        }
        else
        {
            battle.ChangeState(battle.EnemyTurnState);
        }
    }
}
