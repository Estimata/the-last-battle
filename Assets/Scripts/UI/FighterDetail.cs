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
    public void ShowDetail(FighterData data, int currentHealth, Stats stats)
    {
        _fighterName.text = data.FighterName;
        _healthLabel.text = $"HP {currentHealth}";
        _manaLabel.text = $"MP {stats.MaxMP}";
        _attackLabel.text = $"ATK {stats.Attack}";
        _speedLabel.text = $"SPD {stats.Speed}";
        _defendLabel.text = $"DEF {stats.Defense}";
        _criticalLabel.text = $"CRT {stats.Crit}";

        Tween.Scale(transform, Vector3.zero, Vector3.one, 0.45f, Ease.OutBack);
    }
    public void HideDetail() {
        if (transform.localScale == Vector3.zero) return;
        Tween.Scale(transform, Vector3.zero, 0.45f, Ease.InBack);
    }
}
