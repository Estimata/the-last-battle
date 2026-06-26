using UnityEngine;
using System.Collections.Generic;

public class BattleController : MonoBehaviour
{
    public StateMachine<BattleController> BattleState { get; private set; }

    public void BattleEntered(IReadOnlyList<FighterController> fighters)
    {
        foreach (FighterController fighter in fighters)
        {
            Debug.Log(fighter);
        }
    }

    public void BattleExited()
    {
        
    }

    private void OnEnable() {
        BattleInitiator.OnBattleInitiated += BattleEntered;
    }

    private void OnDisable() {
        BattleInitiator.OnBattleInitiated -= BattleEntered;
    }
}
