using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifetime = 10f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        ReactiveTarget target = collision.collider.GetComponentInParent<ReactiveTarget>();
        if (target != null)
        {
            target.ReactToHit();
        }
        Destroy(gameObject);
    }
}