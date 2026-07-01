using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class BattleUIController : MonoBehaviour
{
    [SerializeField] private ActionMenu _actionMenu;
    [SerializeField] private Button _skillButton;
    [SerializeField] private Button _returnButton;
    [SerializeField] private SkillMenu _skillMenu;
    [SerializeField] private UIFighterTurnPanel _turnPanel;
    [SerializeField] private FighterDetail _fighterDetail;
    
    private void Awake() {
        _skillButton.onClick.AddListener(ShowSkillMenu);
        _returnButton.onClick.AddListener(ReturnToActionMenu);
    }

    public void RegisterFighterTurn(List<FighterController> fighters, Action onComplete) => _turnPanel.RegisterFighterTurn(fighters, onComplete);
    public Button RegisterBasicActionButton(FighterAction action) => _actionMenu.RegisterBasicActionButton(action);
    public Button RegisterSkillActionButton(FighterAction action) => _skillMenu.RegisterSkillActionButton(action);
    public void RemoveSkillActionButtons() => _skillMenu.RemoveSkillActionButtons();
    public void ShowFighterQueue() => _turnPanel.gameObject.SetActive(true);
    public void ShowActionMenu() {
        _actionMenu.ShowActionMenu();
        _actionMenu.transform.SetAsLastSibling();
    }

    public async void ShowSkillMenu()
    {
        await _actionMenu.HideActionMenu();
        _skillMenu.ShowSkillMenu();
        _skillMenu.transform.SetAsLastSibling();
    }

    public async void ReturnToActionMenu()
    {
        await _skillMenu.HideSkillMenu();
        ShowActionMenu();
    }
    public void ShowFighterDetail(FighterData data) => _fighterDetail.ShowDetail(data);
}
