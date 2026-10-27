using UnityEngine;

public class ControlledDoor : MonoBehaviour
{
    [SerializeField] private Transform doorTarnsform;

    [SerializeField] private float closedAngle;
    [SerializeField] private float openAngle;
    [SerializeField] private float animationSpeed;


    private bool _isOpen;
    
    public void Open()
    {
        _isOpen = true;
    }

    public void Close()
    {
        _isOpen = false;
    }

    public void SwitchState()
    {
        _isOpen = !_isOpen;
    }

    private void FixedUpdate()
    {
        doorTarnsform.localEulerAngles = new Vector3(
            doorTarnsform.localEulerAngles.x,
            Mathf.LerpAngle(doorTarnsform.localEulerAngles.y, _isOpen? openAngle : closedAngle, animationSpeed),
            doorTarnsform.localEulerAngles.z);
    }
}
