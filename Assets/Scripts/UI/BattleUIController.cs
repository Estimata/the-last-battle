using UnityEngine;

public class BattleUIController : MonoBehaviour
{
    [SerializeField] private UIFighterTurnPanel _turnPanel;
    
    public void RegisterFighterTurn(Sprite[] sprites) => _turnPanel.RegisterFighterTurn(sprites);
}
