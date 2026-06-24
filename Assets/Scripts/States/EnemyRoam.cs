using UnityEngine;
using UnityEngine.AI;

public class EnemyRoam : State<EnemyController>
{
    private int _velocityHash = Animator.StringToHash("Velocity");
    private Vector3 _initialPosition;
    private bool _isMoving;
    private float _moveDistance = 10f;
    private float _idleDuration = 15f;
    private float _idleDurationTimer = 0f;

    public override void Enter(EnemyController enemy)
    {
        _initialPosition = enemy.transform.position;
        _idleDurationTimer = _idleDuration;
        _isMoving = false;
    }
    
    public override void UpdateState(EnemyController enemy)
    {
        if (_isMoving) {
            if (HasArrived(enemy))
            {
                enemy.Animator.SetFloat(_velocityHash, 0f);
                _isMoving = false;
            }

            return;
        }

        _idleDurationTimer -= Time.deltaTime;
        if (_idleDurationTimer <= 0f)
        {
            enemy.Animator.SetFloat(_velocityHash, 0.3f);
            enemy.NavAgent.SetDestination(GetRandomPosition());

            _idleDurationTimer = _idleDuration;
            _isMoving = true;
        }

    }

    private bool HasArrived(EnemyController enemy)
    {
        return (
            !enemy.NavAgent.pathPending &&
            enemy.NavAgent.remainingDistance <= enemy.NavAgent.stoppingDistance &&
            !enemy.NavAgent.hasPath);
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 randomPosition = Random.insideUnitSphere * _moveDistance;
        randomPosition += _initialPosition;

        NavMeshHit hit;
        NavMesh.SamplePosition(randomPosition, out hit, _moveDistance, NavMesh.AllAreas);

        return hit.position;
    }

}
