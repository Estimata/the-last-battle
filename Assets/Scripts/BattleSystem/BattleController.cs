using UnityEngine;
using Unity.Cinemachine;
using System.Collections.Generic;

public class BattleController : MonoBehaviour
{
    [SerializeField] private CinemachineTargetGroup _fighterGroup;
    [SerializeField] private BattleNavigation _battleNavigation;
    private StateMachine<BattleController> _battleState;
    
    public void BattleEntered(BattleContext context)
    {
        _battleNavigation.SetupNavigation(
            context.Fighters,
            context.BattleCenter
        );

        foreach (FighterController fighter in context.Fighters)
        {
            Vector3 initialPosition = _battleNavigation.FindInitialPosition(fighter);
            _fighterGroup.AddMember(fighter.transform, 1f, 1f);

            fighter.InitiatePosition(initialPosition);
        }

        BattleStart();
    }

    public void BattleStart()
    {
        
    }

    public void BattleExited()
    {
        
    }

    private void OnEnable() {
        BattleInitiator.OnBattleReady += BattleEntered;
    }

    private void OnDisable() {
        BattleInitiator.OnBattleReady -= BattleEntered;
    }
}
