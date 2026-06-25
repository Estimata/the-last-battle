using UnityEngine;
using System;
using System.Collections.Generic;

public class BattleInitiator : MonoBehaviour
{
    public static event Action<IReadOnlyList<FighterController>> OnBattleInitiated;
    public static bool InBattle { get; private set;}
    public bool IsAlly = false;
    private LayerMask _fighterLayer;

    private void Awake() {
        InBattle = false;
        _fighterLayer = LayerMask.GetMask("Ally", "Enemy");
    }

    public void Initiated()
    {
        Debug.Log(IsAlly);
    }

    private void OnTriggerEnter(Collider other) {
        if ((_fighterLayer & (1 << other.gameObject.layer)) == 0) return;

        InBattle = true;
        Collider[] hits = Physics.OverlapSphere(transform.position, 30f, _fighterLayer);
        List<FighterController> fighters = new();
        foreach(Collider hit in hits)
        {
            FighterController fighter = hit.GetComponent<FighterController>();
            if (fighter) fighters.Add(fighter);
        }


        OnBattleInitiated?.Invoke(fighters);
    }
}
