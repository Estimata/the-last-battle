using UnityEngine;

public class EnemyTurn : State<BattleController>
{
    public override async void Enter(BattleController battle)
    {
        FighterController enemy = battle.GetFighterTurn();
        if (enemy == null)
        {
            battle.ChangeState(battle.PrepareTurnState);
            return;
        }

        FighterAction action = enemy.GetBasicAction();
        FighterController target = battle.FindNearestTarget(enemy);
        battle.SelectFighter(enemy);
        await action.Execute(enemy, target, battle, battle.BattleContext);
        battle.SelectFighter(null);
        await enemy.ReturnToBattlePosition();
        battle.ChangeState(battle.EndTurnState);
    }
}