using UnityEngine;
using System.Collections.Generic;

public class PlayerAction : MonoBehaviour
{
    private FighterAction _basicAction;
    private List<FighterAction> _skills = new List<FighterAction>();
    private FighterAction _selectedAction;
    public void SetAction(FighterController fighter)
    {
        _basicAction = fighter.GetBasicAction();
        _skills = fighter.GetSkills();
    }

    public void SelectedAction(FighterAction action) => _selectedAction = action;
    public FighterAction GetSelectedAction() => _selectedAction;
    public void ExecuteAction(FighterController user, FighterController target, BattleContext context)
    {
        if (_selectedAction == null) return;
        Debug.Log($"Executing {_selectedAction.ActionName} from {user.name} to {target.name}");
        // _selectedAction.Execute(user, target, context);
    }

}
