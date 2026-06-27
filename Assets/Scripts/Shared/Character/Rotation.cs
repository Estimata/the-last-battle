using UnityEngine;

public class Rotation : MonoBehaviour
{
    public void LookForward(Vector3 direction, float turningSpeed) {
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation, 
                Quaternion.LookRotation(direction), 
                turningSpeed * Time.deltaTime);
        }

    }
}
