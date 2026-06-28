using UnityEngine;

public class Health : MonoBehaviour
{
    private int _maxHP;
    private int _currentHP;
    public void Initialize(FighterData fighterData)
    {
        _maxHP = fighterData.HP;
        _currentHP = _maxHP;
    }
}
