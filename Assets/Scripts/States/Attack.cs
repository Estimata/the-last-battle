using UnityEngine;

public class Attack : State
{
    public override void Enter()
    {
        Debug.Log("Entering Attack State");
    }  
    public override void Exit()
    {
        Debug.Log("Exiting Attack State");
    }
    public override void Update()
    {
        Debug.Log("Updating Attack State");
    }
}
