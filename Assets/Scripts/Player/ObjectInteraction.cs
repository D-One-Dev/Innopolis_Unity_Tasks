using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    private Camera _cam;
    private InputSystem_Actions _input;

    private void Awake()
    {
        _input = new InputSystem_Actions();

        _input.Player.Interact.performed += ctx => Interact();
    }

    private void Start()
    {
        _cam = Camera.main;
    }

    private void OnEnable() => _input.Enable();
    private void OnDisable() => _input.Disable();

    private void Interact()
    {
        Ray ray = new Ray(_cam.transform.position, _cam.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            IInteractable target = hit.transform.gameObject.GetComponentInParent<IInteractable>();
            if (target != null) target.Interact();
        }
    }
}