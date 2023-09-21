using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimtion : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AIBehaviour aiBehaviour;

    void Start()
    {
        animator = GetComponent<Animator>();
        aiBehaviour = GetComponent<AIBehaviour>();  
    }

    void Update()
    {
        switch (aiBehaviour.GetAIState())
        {
            case AIState.Idle:
                animator.SetBool("patrolling", false);
                animator.SetBool("chasing", false);
                animator.SetBool("attacking", false);
                break;
            case AIState.Patrolling:
                animator.SetBool("patrolling", true);
                animator.SetBool("chasing", false);
                animator.SetBool("attacking", false);
                break;
            case AIState.Chasing:
                animator.SetBool("patrolling", false);
                animator.SetBool("chasing", true);
                animator.SetBool("attacking", false);
                break;
            case AIState.Attacking:
                animator.SetBool("attacking", true);
                animator.SetBool("patrolling", false);
                animator.SetBool("chasing", false);
                break; 
            case AIState.Death:
                animator.Play("skeleton_Death");
                break;
            default:
                break;
        }
    }
}
