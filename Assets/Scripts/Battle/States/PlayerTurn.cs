using UnityEngine;

public class PlayerTurn : State<BattleController>
{
    public override void Enter(BattleController battle)
    {
        battle.SetPlayerAction();
        battle.ShowActionMenu();
        battle.FighterSelector.OnFighterSelected += (fighter) => FighterSelected(fighter, battle);
        
    }

    public void FighterSelected(FighterController fighter, BattleController battle) {
        if (fighter == null) {
            battle.BattleUI.HideFighterDetail();
            battle.CancelAction();
            return;
        }

        if (battle.IsActionSelected())
        {
            battle.ExecuteAction(fighter);
            battle.SelectFighter(null);
            return;
        }

        battle.BattleUI.ShowFighterDetail(fighter.GetFighterData());
    }

    public override void Exit(BattleController battle)
    {
        battle.FighterSelector.OnFighterSelected -= (fighter) => FighterSelected(fighter, battle);
    }
}
