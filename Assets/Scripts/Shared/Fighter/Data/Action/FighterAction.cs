using UnityEngine;

public abstract class FighterAction : ScriptableObject {
    public string ActionName;
    [TextArea] public string Description;
    public abstract void Execute(BattleContext context);
}