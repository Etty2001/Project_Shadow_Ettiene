using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public enum AIState { Idle, Patrolling, Chasing, Attacking, Death }
public enum AttackType { Range, Melee }

public class AIBehaviour : MonoBehaviour
{
    private HealthSystem enemyHealth;

    [Header("AI")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private AISight aISight;
    [SerializeField] private GameObject player;
    [SerializeField] private AIState stateMachine = AIState.Idle;

    [Header("Combat")]
    [SerializeField] private AttackType attackType = AttackType.Melee;
    [SerializeField] private bool canAttack = true;
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private float attackRange;
    [SerializeField] private GameObject damageOutput;

    public AIState GetAIState() { return stateMachine; }

    void Start()
    {
        if (!agent)
            agent = GetComponent<NavMeshAgent>();

        canAttack = true;

        enemyHealth = GetComponent<HealthSystem>();
        enemyHealth.onDeath += KillSelf;

        attackRange = agent.stoppingDistance;
    }

    private void OnDisable()
    {
        enemyHealth.onDeath -= KillSelf;
    }

    void Update()
    {
        if (aISight.player)
        {
            player = aISight.player;

            if (!CheckDistance(player.transform.position))
            {
                canAttack = true;
                stateMachine = AIState.Chasing;
            }
            else if (CheckDistance())
            {
                if (canAttack)
                    stateMachine = AIState.Attacking;
            }
            else
                stateMachine = AIState.Chasing;
        }
        else
        {
            player = null;

            if (CheckDistance())
            {
                if (Random.Range(1, 11) == 5)
                    stateMachine = AIState.Idle;
                else
                    stateMachine = AIState.Patrolling;
            }
        }

        HandleBehaviour();
    }

    private bool CheckDistance()
    {
        float distance = Vector3.Distance(transform.position, agent.destination);

        return distance <= attackRange;
    }

    private bool CheckDistance(Vector3 position)
    {
        float distance = Vector3.Distance(position, agent.destination);

        return distance <= 1.5f;
    }

    private void HandleBehaviour()
    {
        switch (stateMachine)
        {
            case AIState.Idle:
                break;
            case AIState.Patrolling:
                agent.speed = 1.5f;
                if (CheckDistance())
                    agent.SetDestination(GetRandomPatrol());
                break;
            case AIState.Chasing:
                agent.speed = 3.5f;
                if (!player) return;
                agent.SetDestination(player.transform.position);
                break;
            case AIState.Attacking:
                break;
            default:
                break;
        }
    }

    private Vector3 GetRandomPatrol()
    {
        float x = transform.position.x + Random.Range(-20, 21);
        float y = transform.position.y;
        float z = transform.position.z + Random.Range(-20, 21);

        return new Vector3(x, y, z);
    }

    private void StopAttacking()
    {
        stateMachine = AIState.Idle;
        canAttack = false;

        Invoke("ResetAttack", attackSpeed);
    }

    private void ResetAttack() => canAttack = true;
    private void ResetDamageOutput() => damageOutput.SetActive(false);

    private void HandleAttack()
    {
        switch (attackType)
        {
            case AttackType.Range:
                break;
            case AttackType.Melee:
                damageOutput.SetActive(true);
                Invoke("ResetDamageOutput", 0.5f);
                break;
            default:
                break;
        }
    }

    private void KillSelf()
    {
        stateMachine = AIState.Death;
        agent.enabled = false;
        this.enabled = false;
    }
}
