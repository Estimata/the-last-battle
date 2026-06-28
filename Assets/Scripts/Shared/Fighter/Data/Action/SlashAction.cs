using UnityEngine;

[CreateAssetMenu(fileName = "New Slash Action", menuName = "Game Data/Fighter Action/Slash Action")]
public class SlashAction : FighterAction {
    public override void Execute(BattleContext context)
    {
        Debug.Log("Slash");
    }
}