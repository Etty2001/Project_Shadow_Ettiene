using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimpleCubeInteract : Interactable
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    protected override void Interact()
    {
        //base.Interact();
        Debug.Log("CHILD INTERACT!");
    }
}
