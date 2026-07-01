using UnityEngine;
using System.Collections.Generic;

public class FighterTurn : MonoBehaviour
{
    public List<FighterController> FighterQueue { get; private set; }
    
    public List<FighterController> CreateQueue(List<FighterController> fighters)
    {
        FighterQueue = new List<FighterController>(fighters);
        FighterQueue.Sort((a, b) => b.Stats.Speed.CompareTo(a.Stats.Speed));
        return FighterQueue;
    }

    public FighterController GetTurnAndAdvance()
    {
        FighterQueue.RemoveAt(0);
        if (FighterQueue.Count == 0) return null;
        return FighterQueue[0];
    }

}
