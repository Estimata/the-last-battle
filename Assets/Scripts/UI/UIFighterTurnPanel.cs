using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using PrimeTween;

public class UIFighterTurnPanel : MonoBehaviour
{
    [SerializeField] private GameObject _fighterAvatar;
    [SerializeField] private HorizontalLayoutGroup _horizontalLayoutGroup;
    List<GameObject> _fighterAvatars = new List<GameObject>();

    public void RegisterFighterTurn(List<FighterController> fighters, Action onComplete)
    {
        _fighterAvatars.Clear();
        Sequence sequence = Sequence.Create();
        foreach(FighterController fighter in fighters) _fighterAvatars.Add(Instantiate(_fighterAvatar, transform));

        Canvas.ForceUpdateCanvases();
        foreach(GameObject fighterAvatar in _fighterAvatars)
        {
            RectTransform rect = fighterAvatar.GetComponent<RectTransform>();
            Image image = fighterAvatar.GetComponent<Image>();
            Color color = image.color;
            color.a = 1f;

            Vector2 initialPosition = rect.anchoredPosition;
            rect.anchoredPosition = initialPosition + Vector2.left * 300;
            image.color = color;
            
            sequence.Chain(
                Tween.UIAnchoredPosition(
                    rect, 
                    endValue: initialPosition,
                    duration: 0.45f, 
                    ease: Ease.OutCubic
                )
            );
        }
        
        sequence.OnComplete(onComplete);

    }
}
