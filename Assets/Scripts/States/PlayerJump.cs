using UnityEngine;

public class PlayerJump : State<PlayerController>
{
    public override void Enter(PlayerController player)
    {
        Debug.Log("Entering Jump State");
    }  
    public override void Exit(PlayerController player)
    {
        Debug.Log("Exiting Jump State");
    }
    public override void UpdateState(PlayerController player)
    {
        Debug.Log("Updating Jump State");
    }
}
