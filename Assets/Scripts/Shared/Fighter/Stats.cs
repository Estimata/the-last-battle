using UnityEngine;

public class Stats : MonoBehaviour
{

    private int _maxMP;
    private int _attack;
    private int _defense;
    private int _speed;
    private float _crit;

    public void Initialize(FighterData fighterData)
    {
        _maxMP = fighterData.MP;
        _attack = fighterData.ATK;
        _defense = fighterData.DEF;
        _speed = fighterData.SPD;
        _crit = fighterData.CRIT;
    }
}
