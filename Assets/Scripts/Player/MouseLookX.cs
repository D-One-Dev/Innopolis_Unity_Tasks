using UnityEngine;

public class MouseLookX : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float mouseSensX;

    private InputSystem_Actions _input;
    private float _yaw;

    private void Awake()
    {
        _input = new InputSystem_Actions();
        _yaw = playerTransform.localEulerAngles.y;
    }

    private void OnEnable() => _input.Enable();
    private void OnDisable() => _input.Disable();

    private void Update()
    {
        float mouseX = _input.Player.Look.ReadValue<Vector2>().x;

        _yaw += mouseX * Time.deltaTime * mouseSensX;

        playerTransform.localRotation = Quaternion.Euler(0f, _yaw, 0f);
    }
}
