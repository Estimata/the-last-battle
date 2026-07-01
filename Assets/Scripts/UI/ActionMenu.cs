using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Threading.Tasks;
using PrimeTween;

public class ActionMenu : MonoBehaviour
{
    [SerializeField] private FighterActionButton _actionButton;
    [SerializeField] private RectTransform _skillButton;
    [SerializeField] private RectTransform _defendButton;
    private FighterActionButton _basicAction;
    private List<FighterActionButton> _skillActions = new List<FighterActionButton>();

    public Button RegisterBasicActionButton(FighterAction action)
    {
        FighterActionButton actionButton = GetComponentInChildren<FighterActionButton>();
        if (actionButton) Destroy(actionButton.gameObject);

        _basicAction = Instantiate(_actionButton, transform);
        _basicAction.Initialize(action);
        _basicAction.transform.SetAsFirstSibling();       
        return _basicAction.GetComponent<Button>();
    }

    public void ShowActionMenu()
    {
        RectTransform actionButton = _basicAction.GetComponent<RectTransform>();
        if (actionButton.localScale == Vector3.one) return;

        Sequence.Create()
            .Insert(0.00f, Tween.Scale(actionButton, Vector3.one, 0.45f, Ease.OutBack))
            .Insert(0.08f, Tween.Scale(_skillButton, Vector3.one, 0.45f, Ease.OutBack))
            .Insert(0.16f, Tween.Scale(_defendButton, Vector3.one, 0.45f, Ease.OutBack));      
    }

    public async Task HideActionMenu()
    {
        RectTransform actionButton = _basicAction.GetComponent<RectTransform>();
        if (actionButton.localScale == Vector3.zero) return;

        Sequence sequence = Sequence.Create()
            .Insert(0.00f, Tween.Scale(actionButton, Vector3.zero, 0.45f, Ease.InBack))
            .Insert(0.08f, Tween.Scale(_skillButton, Vector3.zero, 0.45f, Ease.InBack))
            .Insert(0.16f, Tween.Scale(_defendButton, Vector3.zero, 0.45f, Ease.InBack));
        
        await sequence;
    }
}
