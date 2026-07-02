using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using PrimeTween;

public class UIFighterTurnPanel : MonoBehaviour
{
    [SerializeField] private GameObject _fighterAvatar;
    [SerializeField] private HorizontalLayoutGroup _horizontalLayoutGroup;
    Dictionary<FighterController, GameObject> _fighterAvatars = new Dictionary<FighterController, GameObject>();

    public async Task RegisterFighterTurn(List<FighterController> fighters)
    {
        _fighterAvatars.Clear();
        Sequence sequence = Sequence.Create();
        foreach(FighterController fighter in fighters) _fighterAvatars.Add(fighter, Instantiate(_fighterAvatar, transform));

        Canvas.ForceUpdateCanvases();
        foreach(GameObject fighterAvatar in _fighterAvatars.Values)
        {
            RectTransform rect = fighterAvatar.GetComponent<RectTransform>();
            Image image = fighterAvatar.GetComponent<Image>();
            Color color = image.color;
            color.a = 1f;

            Vector2 initialPosition = rect.anchoredPosition;
            rect.anchoredPosition = initialPosition + Vector2.left * 300;
            image.color = color;
            
            _ = sequence.Chain(
                Tween.UIAnchoredPosition(rect, initialPosition, 0.45f, Ease.OutCubic)
            );
        }
        
        await sequence;

    }

    public async Task RemoveFighterFromQueue(FighterController fighter)
    {
        if (_fighterAvatars.TryGetValue(fighter, out GameObject fighterAvatar))
        {
            if (fighterAvatar.TryGetComponent<RectTransform>(out RectTransform rect)) await Tween.Scale(rect, Vector2.zero, 0.3f, Ease.InCubic);
            _fighterAvatars.Remove(fighter);
            fighterAvatar.transform.SetParent(null);
            Destroy(fighterAvatar);
        }
    }
}
