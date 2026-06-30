using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FighterActionButton : MonoBehaviour
{
    [SerializeField] TMP_Text _text;

    public void Initialize(FighterAction action)
    {
        _text.text = action.ActionName;
    }

    public void Selected()
    {
        
    }

    public void Unselected()
    {
        
    }
}
