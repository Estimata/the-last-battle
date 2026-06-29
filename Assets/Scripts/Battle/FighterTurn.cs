using UnityEngine;
using System.Collections.Generic;

public class FighterTurn : MonoBehaviour
{
    private List<FighterController> _fighterQueue;
    
    public List<FighterController> CreateQueue(List<FighterController> fighters)
    {
        fighters.Sort((a, b) => a.Stats.Speed.CompareTo(b.Stats.Speed));
        _fighterQueue = fighters;
        return _fighterQueue;
    }

    public FighterController GetCurrentFighter()
    {
        return _fighterQueue[0];
    }

}
