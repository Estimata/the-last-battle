using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Fighter", menuName = "Game Data/Fighter Data")]
public class FighterData : ScriptableObject
{
    [Header("Info")]
    public string fighterName;

    [Header("Stats")]
    public int maxHp = 100;
    public int attack = 10;
    public int defense = 5;
    public int speed = 8;
    public float critChance = 0.1f;

    [Header("Battle")]
    public FighterAction baseAction;
    public List<FighterAction> skills;
}