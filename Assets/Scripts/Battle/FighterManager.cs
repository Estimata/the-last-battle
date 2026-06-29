using UnityEngine;
using System.Collections.Generic;

public class FighterManager : MonoBehaviour
{
    List<FighterController> _allies = new List<FighterController>();
    List<FighterController> _enemies = new List<FighterController>();

    public void SetFighterSides(List<FighterController> fighters)
    {
        _allies.Clear();
        _enemies.Clear();
        foreach(FighterController fighter in fighters)
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
    public Transform FindNearestTarget(FighterController finder)
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
            return _enemies[0].transform;
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
            return _allies[0].transform;
        }
    }
}
