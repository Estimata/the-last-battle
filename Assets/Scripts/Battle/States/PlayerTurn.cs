using UnityEngine;

public class PlayerTurn : State<BattleController>
{
    private BattleController _battle;
    public override void Enter(BattleController battle)
    {
        _battle = battle;
        battle.SetPlayerAction();
        battle.ShowActionMenu();
        battle.FighterSelector.OnFighterSelected += FighterSelected;
        
    }

    public void FighterSelected(FighterController fighter) {
        if (fighter == null) {
            _battle.BattleUI.HideFighterDetail();
            _battle.CancelAction();
            return;
        }

        if (_battle.IsActionSelected())
        {
            _battle.ExecuteAction(fighter);
            _battle.SelectFighter(null);
            TurnOver(_battle);
            return;
        }

        _battle.BattleUI.ShowFighterDetail(fighter.GetFighterData());
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
            battle.SetPlayerAction();
            battle.ShowActionMenu();
        }
        else
        {
            battle.ChangeState(battle.EnemyTurnState);
        }
    }

    public override void Exit(BattleController battle)
    {
        Debug.Log("Exiting PlayerTurn");
        battle.FighterSelector.OnFighterSelected -= FighterSelected;
    }
}
