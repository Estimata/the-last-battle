using UnityEngine;

public class EnemyTurn : State<BattleController>
{
    public override void Enter(BattleController battle)
    {
        Debug.Log(battle.GetFighterTurn());
    }
}