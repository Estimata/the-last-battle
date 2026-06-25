using UnityEngine;
using System.Collections.Generic;

public class BattleController : MonoBehaviour
{
    public StateMachine<BattleController> BattleState { get; private set; }

    public void BattleEntered(IReadOnlyList<FighterController> fighters)
    {
        Debug.Log(fighters);
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
