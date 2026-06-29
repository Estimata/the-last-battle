using UnityEngine;

public class Stats : MonoBehaviour
{

    public int MaxMP;
    public int Attack;
    public int Defense;
    public int Speed;
    public float Crit;

    public void Initialize(FighterData fighterData)
    {
        MaxMP = fighterData.MP;
        Attack = fighterData.ATK;
        Defense = fighterData.DEF;
        Speed = fighterData.SPD;
        Crit = fighterData.CRIT;
    }
}
