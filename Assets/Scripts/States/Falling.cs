using UnityEngine;

public class Falling : State
{
    public override void Enter()
    {
        Debug.Log("Entering Falling State");
    }

    public override void Exit()
    {
        Debug.Log("Exiting Falling State");
    }

    public override void Update()
    {
        Debug.Log("Updating Falling State");
    }
}
