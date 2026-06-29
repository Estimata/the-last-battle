using UnityEngine;
using System.Collections.Generic;

public class FighterTurn : MonoBehaviour
{
    public List<FighterController> FighterQueue { get; private set; }
    
    public List<FighterController> CreateQueue(List<FighterController> fighters)
    {
        fighters.Sort((a, b) => b.Stats.Speed.CompareTo(a.Stats.Speed));
        FighterQueue = fighters;
        return FighterQueue;
    }

    public FighterController GetTurnAndAdvance()
    {
        FighterController fighterTurn = FighterQueue[0];
        FighterQueue.RemoveAt(0);
        FighterQueue.Add(fighterTurn);

        return FighterQueue[0];
    }

}
