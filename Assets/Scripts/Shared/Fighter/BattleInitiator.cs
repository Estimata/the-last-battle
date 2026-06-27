using UnityEngine;
using System;
using System.Collections.Generic;

public class BattleInitiator : MonoBehaviour
{
    public static event Action OnBattleInitiated;
    public static event Action<BattleContext> OnBattleReady;
    public static bool InBattle { get; private set;}
    private LayerMask _fighterLayer;

    private void Awake() {
        InBattle = false;
        _fighterLayer = LayerMask.GetMask("Ally", "Enemy");
    }

    private List<FighterController> GetFighters()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 30f, _fighterLayer);
        List<FighterController> fighters = new();
        foreach(Collider hit in hits)
        {
            FighterController fighter = hit.GetComponent<FighterController>();
            if (fighter) fighters.Add(fighter);
        }
        return fighters;
    }

    private Vector3 GetBattleCenter(List<FighterController> fighters)
    {
        Vector3 center = Vector3.zero;
        foreach(FighterController fighter in fighters)
        {
            center += fighter.transform.position;
        }
        return center /= fighters.Count;
    }

    private void OnTriggerEnter(Collider other) {
        if (InBattle || (_fighterLayer & (1 << other.gameObject.layer)) == 0) return;

        InBattle = true;

        OnBattleInitiated?.Invoke();
        
        FighterController initiator = GetComponent<FighterController>();
        FighterController target = other.GetComponent<FighterController>();
        List<FighterController> fighters = GetFighters();
        Vector3 battleCenter = GetBattleCenter(fighters);
        BattleContext battleContext = new BattleContext(
            initiator,
            target,
            fighters,
            battleCenter
        );

        OnBattleReady?.Invoke(battleContext);
    }
}
