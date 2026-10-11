using UnityEngine;

public class FPSInput : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;

    private InputSystem_Actions _input;

    private void Awake()
    {
        _input = new InputSystem_Actions();

        _input.Player.Jump.performed += ctx => Jump();
    }

    private void OnEnable() => _input.Enable();
    private void OnDisable() => _input.Disable();


    private void Update()
    {
        Vector2 movement = _input.Player.Move.ReadValue<Vector2>() * movementSpeed;
        if (_input.Player.Sprint.ReadValue<float>() > 0f) movement *= 2;
        rb.linearVelocity = rb.transform.forward * movement.y + Vector3.up * rb.linearVelocity.y + rb.transform.right * movement.x;
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jumpForce);
    }
}
