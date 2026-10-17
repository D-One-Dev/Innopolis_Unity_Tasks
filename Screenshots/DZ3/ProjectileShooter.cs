using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float launchSpeed = 15f;
    [SerializeField] private float spawnDistance = 1f;

    private Camera _cam;
    private InputSystem_Actions _input;

    void Awake()
    {
        _input = new InputSystem_Actions();

        _input.Player.AttackAlt.performed += ctx => Shoot();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void Start()
    {
        _cam = Camera.main;
    }

    private void Shoot()
    {
        if (projectilePrefab == null) return;

        Transform camTransform = _cam.transform;
        GameObject projectile = Instantiate(projectilePrefab, camTransform.position + camTransform.forward * spawnDistance, camTransform.rotation);

        if (projectile.TryGetComponent(out Rigidbody rb))
        {
            rb.linearVelocity = camTransform.forward * launchSpeed;
        }
    }
}