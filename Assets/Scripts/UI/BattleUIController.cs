using UnityEngine;
using System;
using System.Collections.Generic;

public class BattleUIController : MonoBehaviour
{
    [SerializeField] private ActionMenu _actionMenu;
    [SerializeField] private UIFighterTurnPanel _turnPanel;
    [SerializeField] private FighterDetail _fighterDetail;
    
    public void RegisterFighterTurn(List<FighterController> fighters, Action onComplete) => _turnPanel.RegisterFighterTurn(fighters, onComplete);
    public void RegisterBasicActionButton(FighterAction action) => _actionMenu.RegisterBasicActionButton(action);
    public void ShowFighterQueue() => _turnPanel.gameObject.SetActive(true);
    public void ShowActionMenu() => _actionMenu.ShowActionMenu();
    public void ShowFighterDetail(FighterData data) => _fighterDetail.ShowDetail(data);
}
