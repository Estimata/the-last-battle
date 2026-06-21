using UnityEngine;

public class Jump : State
{
    public override void Enter()
    {
        Debug.Log("Entering Jump State");
    }  
    public override void Exit()
    {
        Debug.Log("Exiting Jump State");
    }
    public override void Update()
    {
        Debug.Log("Updating Jump State");
    }
}
