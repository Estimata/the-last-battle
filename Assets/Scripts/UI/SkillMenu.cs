using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Threading.Tasks;
using PrimeTween;

public class SkillMenu : MonoBehaviour
{
    [SerializeField] private FighterActionButton _actionButton;
    [SerializeField] private RectTransform _returnButton;
    private List<FighterActionButton> _skillActions = new List<FighterActionButton>();
    public Button RegisterSkillActionButton(FighterAction action)
    {
        FighterActionButton skillButton = Instantiate(_actionButton, transform);
        skillButton.Initialize(action);
        skillButton.transform.SetAsFirstSibling();
        _skillActions.Insert(0, skillButton);
        return skillButton.GetComponent<Button>();
    }
    public void RemoveSkillActionButtons()
    {
        _skillActions.Clear();
        FighterActionButton[] fighterActionButtons = GetComponentsInChildren<FighterActionButton>();
        foreach (FighterActionButton button in fighterActionButtons) Destroy(button.gameObject);
    }

    public void ShowSkillMenu()
    {
        float delay = 0.0f;
        Sequence sequence = Sequence.Create();
        foreach (FighterActionButton button in _skillActions)
        {
            RectTransform actionButton = button.GetComponent<RectTransform>();
            sequence.Insert(delay, Tween.Scale(actionButton, Vector3.one, 0.45f, Ease.OutBack));
            delay += 0.08f;
        }
        sequence.Insert(delay, Tween.Scale(_returnButton, Vector3.one, 0.45f, Ease.OutBack));
    }

    public async Task HideSkillMenu()
    {
        float delay = 0.0f;
        Sequence sequence = Sequence.Create();
        foreach (FighterActionButton button in _skillActions)
        {
            RectTransform actionButton = button.GetComponent<RectTransform>();
            sequence.Insert(delay, Tween.Scale(actionButton, Vector3.zero, 0.45f, Ease.InBack));
            delay += 0.08f;
        }

        sequence.Insert(delay, Tween.Scale(_returnButton, Vector3.zero, 0.45f, Ease.InBack));
        await sequence;
    }

}
