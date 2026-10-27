using UnityEngine;

public class InteractableFanController : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform fan;
    [SerializeField] private float speed;
    [SerializeField] private float startStopSmoothness;

    private bool _enabled;
    private float _currentSpeed;

    public void Interact()
    {
        _enabled = !_enabled;
    }

    private void FixedUpdate()
    {
        _currentSpeed = Mathf.Lerp(_currentSpeed, _enabled? speed : 0f, startStopSmoothness);

        fan.transform.eulerAngles += new Vector3(0f, _currentSpeed, 0f);
    }
}