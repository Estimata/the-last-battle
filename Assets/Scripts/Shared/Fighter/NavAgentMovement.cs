using UnityEngine;
using UnityEngine.AI;

public class NavAgentMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;

    private void Awake() {
        _agent.updateRotation = false;
    }

    public bool HasArrived()
    {
        return (
            !_agent.pathPending &&
            _agent.remainingDistance <= _agent.stoppingDistance &&
            (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
        );
    }
}
