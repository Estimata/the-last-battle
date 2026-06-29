using UnityEngine;

public class PlayerTurn : State<BattleController>
{
    public override void Enter(BattleController battle)
    {
        battle.SetPlayerAction();
        battle.BattleUI.ShowActionMenu();
    }
}
