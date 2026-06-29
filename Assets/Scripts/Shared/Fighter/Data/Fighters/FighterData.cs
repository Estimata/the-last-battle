using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Fighter", menuName = "Game Data/Fighter Data")]
public class FighterData : ScriptableObject
{
    [Header("Info")]
    public string FighterName;

    [Header("Stats")]
    public int HP = 100;
    public int MP = 100;
    public int ATK = 10;
    public int DEF = 5;
    public int SPD = 8;
    public float CRIT = 0.1f;

    [Header("Battle")]
    public FighterAction BasicAction;
    public List<FighterAction> Skills;
}