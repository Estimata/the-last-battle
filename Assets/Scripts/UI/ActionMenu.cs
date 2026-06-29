using UnityEngine;
using System.Collections.Generic;
using PrimeTween;

public class ActionMenu : MonoBehaviour
{
    [SerializeField] private FighterActionButton _actionButton;
    [SerializeField] private RectTransform _skillButton;
    [SerializeField] private RectTransform _defendButton;
    private FighterActionButton _basicAction;
    public void RegisterBasicActionButton(FighterAction action)
    {
        FighterActionButton actionButton = GetComponentInChildren<FighterActionButton>();
        if (actionButton) Destroy(actionButton.gameObject);

        _basicAction = Instantiate(_actionButton, transform);
        _basicAction.Initialize(action);
        _basicAction.transform.SetAsFirstSibling();       
    }

    public void ShowActionMenu()
    {
        RectTransform actionButton = _basicAction.GetComponent<RectTransform>();
        Sequence.Create()
            .Insert(0.00f, Tween.Scale(actionButton, Vector3.one, 0.45f, Ease.OutBack))
            .Insert(0.08f, Tween.Scale(_skillButton, Vector3.one, 0.45f, Ease.OutBack))
            .Insert(0.16f, Tween.Scale(_defendButton, Vector3.one, 0.45f, Ease.OutBack));      
    }
}
