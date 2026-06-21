using UnityEngine;

public class PlayerAttack : State<PlayerController>
{
    public override void Enter(PlayerController player)
    {
        Debug.Log("Entering Attack State");
    }  
    public override void Exit(PlayerController player)
    {
        Debug.Log("Exiting Attack State");
    }
    public override void UpdateState(PlayerController player)
    {
        Debug.Log("Updating Attack State");
    }
}
