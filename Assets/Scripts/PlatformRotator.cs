using UnityEngine;

public class PlatformRotator : MonoBehaviour
{
    [SerializeField] private Vector3 rotationSpeed;

    void FixedUpdate()
    {
        transform.localEulerAngles += rotationSpeed;
    }
}
