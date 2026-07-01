using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PrimeTween;

public class FighterDetail : MonoBehaviour
{
    [SerializeField] private TMP_Text _fighterName;
    [SerializeField] private TMP_Text _healthLabel;
    [SerializeField] private TMP_Text _manaLabel;
    [SerializeField] private TMP_Text _attackLabel;
    [SerializeField] private TMP_Text _speedLabel;
    [SerializeField] private TMP_Text _defendLabel;
    [SerializeField] private TMP_Text _criticalLabel;
    public void ShowDetail(FighterData data)
    {
        _fighterName.text = data.FighterName;
        _healthLabel.text = $"HP {data.HP}";
        _manaLabel.text = $"MP {data.MP}";
        _attackLabel.text = $"ATK {data.ATK}";
        _speedLabel.text = $"SPD {data.SPD}";
        _defendLabel.text = $"DEF {data.DEF}";
        _criticalLabel.text = $"CRT {data.CRIT}";

        Tween.Scale(transform, Vector3.zero, Vector3.one, 0.45f, Ease.OutBack);
    }
    public void HideDetail()
    {
        if (transform.localScale == Vector3.zero) return;
        Tween.Scale(transform, Vector3.zero, 0.45f, Ease.InBack);
    }
}
