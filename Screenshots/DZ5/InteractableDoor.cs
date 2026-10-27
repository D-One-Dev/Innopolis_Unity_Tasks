public class InteractableDoor : ControlledDoor, IInteractable
{
    public void Interact()
    {
        SwitchState();
    }
}