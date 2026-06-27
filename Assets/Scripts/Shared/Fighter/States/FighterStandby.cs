using UnityEngine;
using UnityEngine.AI;

public class FighterStandby : State<FighterController>
{
    private int _velocityHash = Animator.StringToHash("Velocity");
    private Vector3 _initialPosition;
    private bool _isMoving;
    private float _moveDistance = 10f;
    private float _idleDuration = 30f;
    private float _idleDurationTimer = 0f;

    public override void Enter(FighterController fighter)
    {
        _initialPosition = fighter.transform.position;
        _idleDurationTimer = _idleDuration;
        _isMoving = false;
    }
    
    public override void Update(FighterController fighter)
    {
        if (_isMoving) {
            fighter.LookForward();
            if (fighter.HasArrived())
            {
                fighter.Animator.SetFloat(_velocityHash, 0f);
                _isMoving = false;
            }

            return;
        }

        _idleDurationTimer -= Time.deltaTime;
        if (_idleDurationTimer <= 0f)
        {
            fighter.Animator.SetFloat(_velocityHash, 0.3f);
            fighter.SetDestination(GetRandomPosition());

            _idleDurationTimer = _idleDuration;
            _isMoving = true;
        }

    }

    private Vector3 GetRandomPosition()
    {
        Vector3 randomPosition = Random.insideUnitSphere * _moveDistance;
        randomPosition += _initialPosition;

        NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, _moveDistance, NavMesh.AllAreas);
        return hit.position;    

    }
}
