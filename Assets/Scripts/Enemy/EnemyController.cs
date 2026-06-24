using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Animator Animator;
    public NavMeshAgent NavAgent;

    public StateMachine<EnemyController> EnemyState { get; private set; }
    public EnemyRoam RoamState { get; private set; }
    
    private void Awake()
    {
        RoamState = new EnemyRoam();

        EnemyState = new StateMachine<EnemyController>(this, RoamState);
    }

    private void Update() {
        EnemyState.Update();
    }

    private void OnTriggerEnter(Collider other) {
        
    }
}
