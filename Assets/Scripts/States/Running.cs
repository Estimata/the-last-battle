using UnityEngine;

public class Running : State
{
    public override void Enter()
    {
        Debug.Log("Entering Running State");
    }  
    public override void Exit()
    {
        Debug.Log("Exiting Running State");
    }
    public override void Update()
    {
        Debug.Log("Updating Running State");
    }
}
