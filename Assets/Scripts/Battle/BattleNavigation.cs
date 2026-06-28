using UnityEngine;
using System.Collections.Generic;

public class BattleNavigation : MonoBehaviour
{
    private float _groupDistance = 5f;
    private Vector2 _positionOffsetY = new Vector2(-5f, 5f);
    private Vector2 _positionOffsetX = new Vector2(-2f, 2f);
    private bool _isAllyOnLeft;
    private Vector3 _battleCenter;

    public void SetupNavigation(IReadOnlyList<FighterController> fighters, Vector3 BattleCenter)
    {
        _isAllyOnLeft = GetAllySide(fighters);
        _battleCenter = BattleCenter;
    }

    public Vector3 FindInitialPosition(FighterController fighter)
    {
        Vector3 initialPosition = Vector3.right * _groupDistance;
        initialPosition += new Vector3(
            RandomRange(_positionOffsetX), 
            0, 
            RandomRange(_positionOffsetY)
        );

        if (fighter.IsAlly)
        {
            return _isAllyOnLeft ? _battleCenter - initialPosition : _battleCenter + initialPosition;
        }
        return _isAllyOnLeft ? _battleCenter + initialPosition : _battleCenter - initialPosition;

    }

    private bool GetAllySide(IReadOnlyList<FighterController> fighters)
    {
        float allySumX = 0f;
        float enemySumX = 0f;
        int allyCount = 0;
        int enemyCount = 0;
        foreach(FighterController fighter in fighters)
        {
            if (fighter.IsAlly)
            {
                allySumX += fighter.transform.position.x;
                allyCount += 1;
                continue;
            }
            enemySumX += fighter.transform.position.x;
            enemyCount += 1;
        }
        
        allySumX /= allyCount;
        enemySumX /= enemyCount;

        return allySumX < enemySumX;
    }

    private float RandomRange(Vector2 range) => Random.Range(range.x, range.y);
}
