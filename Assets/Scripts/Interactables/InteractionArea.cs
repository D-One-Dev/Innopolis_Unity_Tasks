using UnityEngine;

public class InteractionArea : MonoBehaviour
{
    [SerializeField] private ControlledDoor door;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            door.Open();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            door.Close();
        }
    }
}
