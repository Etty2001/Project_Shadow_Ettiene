using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera camera;

    [SerializeField] private float distance = 3f;

    [SerializeField] private LayerMask mask;

    private PlayerUI playerUI;

    private InputManager inputManager;

    void Start()
    {
        camera = GetComponent<PlayerCamera>().GetCamera;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
    }

    void Update()
    {
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance, UnityEngine.Color.green);

        // Viariable stores our collision information.
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, distance, mask)) 
        {
            Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
            SimpleCubeInteract cube = hitInfo.collider.GetComponent<SimpleCubeInteract>();

            if (interactable != null)
            {
                //interactable.BaseInteraction();
                playerUI.UpdatePromptText(interactable.promptMessage);

                if (inputManager.onFootActions.Interact.triggered)
                {
                    if (typeof(ItemInteract).IsAssignableFrom(interactable.GetType()))
                        interactable.ItemInteraction(GetComponent<Inventory>());
                    else
                        interactable.BaseInteraction();
                }
            }
        }
        else
        {
            playerUI.UpdatePromptText("");
        }
    }
}
