using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private CharacterController _character;
    public bool GravityEnabled = true;
    public Vector3 Velocity { get; private set; }

    private void Update() {
        HandleGravity();
    }

    public void Move(Transform relativeTo, Vector3 moveDirection, float runningSpeed)
    {
        Vector3 relativeMoveDirection = relativeTo.TransformDirection(moveDirection);

        relativeMoveDirection.y = 0f;
        Velocity = relativeMoveDirection * runningSpeed;
        
        _character.Move(Velocity * Time.deltaTime);
    }

    void HandleGravity()
    {
        if (GravityEnabled && !_character.isGrounded)
        {
            _character.Move(Physics.gravity * Time.deltaTime);
        }
    }

    public float GetMovementLean(Transform relativeTo)
    {
        float angle = Vector3.SignedAngle(relativeTo.forward, Velocity.normalized, Vector3.up);
        return Mathf.Sin(angle * Mathf.Deg2Rad);
    }
}
