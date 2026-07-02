using UnityEngine;

public class PlayerTurn : State<BattleController>
{
    private BattleController _battle;
    private bool _inAction = false;
    public override void Enter(BattleController battle)
    {
        _battle = battle;
        _inAction = false;
        battle.SetPlayerAction();
        battle.ShowActionMenu();
        battle.FighterSelector.OnFighterSelected += FighterSelected;
        
    }

    public async void FighterSelected(FighterController fighter) {
        if (_inAction) return;

        if (fighter == null) {
            _battle.BattleUI.HideFighterDetail();
            _battle.CancelAction();
            return;
        }

        if (_battle.IsActionSelected())
        {
            _inAction = true;
            FighterController user = _battle.GetFighterTurn();
            _battle.SelectFighter(user);
            await _battle.BattleUI.ClearActionButtons();
            await _battle.ExecuteAction(fighter);

            _battle.SelectFighter(null);
            _battle.CancelAction();
            await user.ReturnToBattlePosition();
            
            _battle.ChangeState(_battle.EndTurnState);
            return;
        }

        _battle.BattleUI.ShowFighterDetail(fighter.GetFighterData(), fighter.GetCurrentHP(), fighter.Stats);
    }

    public override void Exit(BattleController battle)
    {
        battle.FighterSelector.OnFighterSelected -= FighterSelected;
    }
}
