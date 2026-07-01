using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.Collections.Generic;

public class BattleUIController : MonoBehaviour
{
    [SerializeField] private ActionMenu _actionMenu;
    [SerializeField] private Button _skillButton;
    [SerializeField] private Button _returnButton;
    [SerializeField] private SkillMenu _skillMenu;
    [SerializeField] private UIFighterTurnPanel _turnPanel;
    [SerializeField] private FighterDetail _fighterDetail;
    
    private List<Button> _actionButtons = new List<Button>();

    private void Awake() {
        _skillButton.onClick.AddListener(ShowSkillMenu);
        _returnButton.onClick.AddListener(ReturnToActionMenu);
    }

    public async Task RegisterFighterTurn(List<FighterController> fighters) => await _turnPanel.RegisterFighterTurn(fighters);
    public async Task RemoveFighterFromQueue(FighterController fighter) => await _turnPanel.RemoveFighterFromQueue(fighter);
    public Button RegisterBasicActionButton(FighterAction action) {
        Button button = _actionMenu.RegisterBasicActionButton(action);
        _actionButtons.Add(button);
        button.onClick.AddListener(() => ButtonSelected(button));
        return button;
    }
    public Button RegisterSkillActionButton(FighterAction action) {
        Button button = _skillMenu.RegisterSkillActionButton(action);
        _actionButtons.Add(button);
        button.onClick.AddListener(() => ButtonSelected(button));
        return button;
    }
    
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
        UnselectAllButtons();
        ShowActionMenu();
    }
    public void ShowFighterDetail(FighterData data) => _fighterDetail.ShowDetail(data);
    public void HideFighterDetail() => _fighterDetail.HideDetail();
    
    public void UnselectAllButtons()
    {
        foreach (Button button in _actionButtons)
        {
            FighterActionButton actionButton = button.GetComponent<FighterActionButton>();
            actionButton.Unselected();
        }
    }
    public async Task ClearActionButtons()
    {
        _actionButtons.Clear();
        await _actionMenu.HideActionMenu();
        await _skillMenu.HideSkillMenu();
    }

    private void ButtonSelected(Button button)
    {
        foreach (Button btn in _actionButtons)
        {
            FighterActionButton actionButton = btn.GetComponent<FighterActionButton>();
            if (btn == button)
            {
                actionButton.Selected();
            }
            else
            {
                actionButton.Unselected();
            }
        }
    }
}
