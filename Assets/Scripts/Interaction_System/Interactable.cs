using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    // Add or Remove an "InteractionEvents" to this GameObject
    public bool useEvents;

    // Message displayed to player when looking at an interactable
    public string promptMessage;

    // This function will be called from our player.
    public void BaseInteraction()
    {
        if (useEvents)
            GetComponent<InteractionEvents>().OnInteract.Invoke();

        Interact();
    }

    public void ItemInteraction(Inventory inventory)
    {
        if (useEvents)
            GetComponent<InteractionEvents>().OnInteract.Invoke();

        Interact(inventory);
    }

    protected virtual void Interact()
    {
        // We wont have any code written in this function.
        // This is only a template function to be overridden by subclasses.

        Debug.Log("MAIN INTERACT!");
    }

    protected virtual void Interact(Inventory inventory)
    {
        // We wont have any code written in this function.
        // This is only a template function to be overridden by subclasses.

        Debug.Log("ITEM INTERACT!");
    }
}
