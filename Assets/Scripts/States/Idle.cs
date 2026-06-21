using UnityEngine;

public class Idle : State
{
    public override void Enter()
    {
        Debug.Log("Entering Idle State");
    }

    public override void Exit()
    {
        Debug.Log("Exiting Idle State");
    }

    public override void Update()
    {
        Debug.Log("Updating Idle State");
    }
}
