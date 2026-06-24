using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Animator Animator;
    public NavMeshAgent NavAgent;

    public StateMachine<EnemyController> StateMachine { get; private set; }
    public EnemyRoam RoamState { get; private set; }
    
    private void Awake()
    {
        RoamState = new EnemyRoam();

        StateMachine = new StateMachine<EnemyController>(this, RoamState);
    }

    private void Update() {
        StateMachine.Update();
    }
}
