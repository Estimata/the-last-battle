using UnityEngine;

public class Stats : MonoBehaviour
{

    public int MaxMP { get; private set; }
    public int Attack { get; private set; }
    public int Defense { get; private set; }
    public int Speed { get; private set; }
    public float Crit { get; private set; }

    public void Initialize(FighterData fighterData)
    {
        MaxMP = fighterData.MP;
        Attack = fighterData.ATK;
        Defense = fighterData.DEF;
        Speed = fighterData.SPD;
        Crit = fighterData.CRIT;
    }
}
