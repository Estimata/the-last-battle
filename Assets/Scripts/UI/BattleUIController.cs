using UnityEngine;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

public class BattleUIController : MonoBehaviour
{
    [SerializeField] private UIFighterTurnPanel _turnPanel;
    
    public void RegisterFighterTurn(List<FighterController> fighters, Action onComplete) => _turnPanel.RegisterFighterTurn(fighters, onComplete);
    public void ShowFighterQueue() => _turnPanel.gameObject.SetActive(true);
}
