using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private InputActionReference _moveAction;
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 _moveDirection;

    void Update()
    {
        _moveDirection = _moveAction.action.ReadValue<Vector2>();

        Vector3 movement = new Vector3(
            _moveDirection.x,
            0f,
            _moveDirection.y
        );

        _characterController.Move(movement * moveSpeed * Time.deltaTime);
    }
}