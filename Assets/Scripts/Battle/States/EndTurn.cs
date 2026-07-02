using UnityEngine;

public class EndTurn : State<BattleController>
{
    public override async void Enter(BattleController battle)
    {
        FighterController fighter = battle.GetDiedFighter();
        if (fighter) await battle.RemoveFighter(fighter);

        if (battle.IsBattleOver()) return;

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
            battle.ChangeState(battle.EnemyTurnState);
        }
    }
}
