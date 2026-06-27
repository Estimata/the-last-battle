using UnityEngine;
using System.Collections.Generic;

public class BattleContext
{
    public FighterController Initiator { get; }
    public FighterController Target { get; }
    public IReadOnlyList<FighterController> Fighters { get; }
    public Vector3 BattleCenter { get; }

    public BattleContext(
        FighterController initiator,
        FighterController target,
        IReadOnlyList<FighterController> fighters,
        Vector3 battleCenter
    )
    {
        Initiator = initiator;
        Target = target;
        Fighters = fighters;
        BattleCenter = battleCenter;
    }
}