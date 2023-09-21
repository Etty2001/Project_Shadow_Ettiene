using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTest : MonoBehaviour
{
    [SerializeField] private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerDoor()
    {
        if (animator.GetBool("DoorIsOpen"))
        {
            CloseDoor();
        }
        else
        {
            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        animator.SetBool("DoorIsOpen", true);
    }

    public void CloseDoor()
    {
        animator.SetBool("DoorIsOpen", false);
    }
}
