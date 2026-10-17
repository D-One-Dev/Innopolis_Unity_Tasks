using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 1;

    private void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerCharacter player = other.GetComponentInParent<PlayerCharacter>();
        if (player != null) player.Hurt(damage);
        
        Destroy(gameObject);
    }
}