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

    public void SelectedAction()
    {
        
    }


}
