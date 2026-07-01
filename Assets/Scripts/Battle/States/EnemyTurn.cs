using UnityEngine;

public class EnemyTurn : State<BattleController>
{
    public override void Enter(BattleController battle)
    {
        FighterController enemy = battle.GetFighterTurn();
        if (enemy == null)
        {
            battle.ChangeState(battle.PrepareTurnState);
            return;
        }

        FighterAction action = enemy.GetBasicAction();
        FighterController target = battle.FindNearestTarget(enemy);
        battle.ExecuteAction(target);
        TurnOver(battle);
    }

    private async void TurnOver(BattleController battle)
    {
        FighterController fighterTurn = await battle.GetTurnAndAdvance();
        if (fighterTurn == null)
        {
            battle.ChangeState(battle.PrepareTurnState);
            return;
        }
        
        if (fighterTurn.IsAlly)
        {
            battle.ChangeState(battle.PlayerTurnState);
        }
        else
        {
            Enter(battle);
        }
    }
}