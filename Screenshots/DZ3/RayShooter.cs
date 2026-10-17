using System.Collections;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
    [SerializeField] private float indicatorLifetime = 1f;

    private Camera _cam;
    private InputSystem_Actions _input;

    private void Awake()
    {
        _input = new InputSystem_Actions();

        _input.Player.Attack.performed += ctx => Shoot();
    }

    private void Start()
    {
        _cam = Camera.main;
    }

    private void OnEnable() => _input.Enable();
    private void OnDisable() => _input.Disable();

    private void Shoot()
    {
        Ray ray = new Ray(_cam.transform.position, _cam.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            ReactiveTarget target = hit.transform.gameObject.GetComponentInParent<ReactiveTarget>();
            if (target != null) target.ReactToHit();
            else StartCoroutine(SphereIndicator(hit.point));
        }
    }

    private IEnumerator SphereIndicator(Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;
        yield return new WaitForSeconds(indicatorLifetime);
        Destroy(sphere);
    }

    private void OnGUI()
    {
        int size = 12;
        float posX = _cam.pixelWidth / 2 - size / 4;
        float posY = _cam.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }
}