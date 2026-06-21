using UnityEngine;

public class PlayerFall : State<PlayerController>
{
    public override void Enter(PlayerController player)
    {
        Debug.Log("Entering Fall State");
    }

    public override void Exit(PlayerController player)
    {
        Debug.Log("Exiting Fall State");
    }

    public override void UpdateState(PlayerController player)
    {
        Debug.Log("Updating Fall State");
    }
}
