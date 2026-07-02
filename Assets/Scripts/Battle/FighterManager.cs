using UnityEngine;
using System.Collections.Generic;

public class FighterManager : MonoBehaviour
{
    public List<FighterController> RemainingFighter = new List<FighterController>();
    private List<FighterController> _allies = new List<FighterController>();
    private List<FighterController> _enemies = new List<FighterController>();

    public void SetFighterSides()
    {
        _allies.Clear();
        _enemies.Clear();
        foreach(FighterController fighter in RemainingFighter)
        {    
            if (fighter.IsAlly)
            {
                _allies.Add(fighter);
            }
            else
            {
                _enemies.Add(fighter);
            }
        }
    }
    public FighterController FindNearestTarget(FighterController finder)
    {
        if (finder.IsAlly)
        {
            _enemies.Sort((a, b) => Vector3.Distance(
                a.transform.position, 
                finder.transform.position
                ).CompareTo(Vector3.Distance(
                    b.transform.position, 
                    finder.transform.position)
                )
            );
            return _enemies[0];
        }
        else
        {
            _allies.Sort((a, b) => Vector3.Distance(
                a.transform.position, 
                finder.transform.position
                ).CompareTo(Vector3.Distance(
                    b.transform.position, 
                    finder.transform.position)
                )
            );
            return _allies[0];
        }
    }

    public FighterController GetDiedFighter()
    {
        foreach (FighterController fighter in RemainingFighter) if (fighter.IsDead()) return fighter;
        return null;
    }
    public bool IsBattleOver(List<FighterController> remainingFighters)
    {
        int allyCount = 0;
        int enemyCount = 0;

        foreach (FighterController fighter in remainingFighters)
        {
            if (fighter.IsAlly)
            {
                allyCount++;
            }
            else
            {
                enemyCount++;
            }
        }

        return allyCount == 0 || enemyCount == 0;
    }
}
