using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FighterActionButton : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _background;

    private Color _normalBackground = Color.white;
    private Color _pressedBackground = new Color(0.6f, 0.6f, 0.6f);

    private Color _normalText = Color.black;
    private Color _pressedText = Color.white;

    public void Initialize(FighterAction action)
    {
        _text.text = action.ActionName;
        Unselected();
    }

    public void Selected()
    {
        _background.color = _pressedBackground;
        _text.color = _pressedText;
    }

    public void Unselected()
    {
        _background.color = _normalBackground;
        _text.color = _normalText;
    }
}