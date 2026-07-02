using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    private int _maxHP;
    public int CurrentHP;
    public void Initialize(FighterData fighterData)
    {
        _maxHP = fighterData.HP;
        CurrentHP = _maxHP;
    }

    public void TakeDamage(int damage)
    {
        CurrentHP -= damage;
        CurrentHP = Math.Max(CurrentHP, 0);
    }

    public bool IsDead()
    {
        return CurrentHP <= 0;
    }
}
