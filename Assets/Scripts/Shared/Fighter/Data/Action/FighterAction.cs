using UnityEngine;
using System.Threading.Tasks;

public abstract class FighterAction : ScriptableObject {
    public string ActionName;
    [TextArea] public string Description;
    public abstract Task Execute(FighterController user, FighterController target, BattleController battle, BattleContext context);
}