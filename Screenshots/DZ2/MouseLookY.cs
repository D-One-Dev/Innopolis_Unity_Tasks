using UnityEngine;

public class MouseLookY : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float mouseSensY;

    private InputSystem_Actions _input;
    private float _pitch;

    private void Awake()
    {
        _input = new InputSystem_Actions();
    }

    private void OnEnable() => _input.Enable();
    private void OnDisable() => _input.Disable();


    private void Update()
    {
        float mouseY = _input.Player.Look.ReadValue<Vector2>().y;

        _pitch -= mouseY * Time.deltaTime * mouseSensY;
        _pitch = Mathf.Clamp(_pitch, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(_pitch, 0f, 0f);
    }
}
