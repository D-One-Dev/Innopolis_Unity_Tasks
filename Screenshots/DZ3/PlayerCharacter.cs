using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private int health = 5;

    public void Hurt(int damage)
    {
        if (health <= 0) return;

        health -= damage;
        Debug.Log($"Player health: {health}");

        if (health <= 0) Debug.Log("Player is dead");
    }
}