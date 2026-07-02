using UnityEngine;
using System.Threading.Tasks;

[CreateAssetMenu(fileName = "New Slash Action", menuName = "Game Data/Fighter Action/Slash Action")]
public class SlashAction : FighterAction {
    public override async Task Execute(FighterController user, FighterController target, BattleController battle,  BattleContext context)
    {
        await user.GoToTarget(target.transform);
        user.Animator.SetTrigger("Attack");
        target.TakeDamage(user.Stats.Attack);
        await Task.Delay(1000);
    }
}