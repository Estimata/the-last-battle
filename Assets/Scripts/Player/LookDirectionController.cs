using UnityEngine;

public class LookDirectionController : MonoBehaviour
{
    [SerializeField] private float _mouseSensitivity = 25f;
    private float _yaw = 0f;
    private float _pitch = 0f;

    public void Look(Vector2 lookDirection)
    {
        float mouseX = lookDirection.x * _mouseSensitivity * Time.deltaTime;
        float mouseY = lookDirection.y * _mouseSensitivity * Time.deltaTime;

        _yaw += mouseX;
        _pitch -= mouseY;

        _pitch = Mathf.Clamp(_pitch, -25f, 80f);

        transform.localRotation = Quaternion.Euler(_pitch, _yaw, 0);
    }
}
