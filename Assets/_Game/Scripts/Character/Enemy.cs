using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    private enum EnemyState
    {
        Idle,
        Seek,
        Attack,
        Dead,
    }

    [SerializeField] private EnemyState state;

    [SerializeField] private GameObject targetCircle;
    [SerializeField] private float timeIdle = 2f;

    private float currentTimeIdle;
    private NavMeshAgent agent;
    private Vector3 destination;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        OnInit();
    }

    private void OnInit()
    {
        ChangeToState(EnemyState.Idle);
        destination = transform.position;
        canAttack = true;
        //agent.stoppingDistance = radius;
    }

    private void Update()
    {
        switch (state)
        {
            case EnemyState.Idle:
                currentTimeIdle += Time.deltaTime;
                if (currentTimeIdle >= timeIdle)
                {
                    ChangeToState(EnemyState.Seek);
                }
                break;
            case EnemyState.Seek:
                if (CanAttack() && canAttack)
                {
                    agent.enabled = false;
                    ChangeToState(EnemyState.Attack);
                }
                if (Vector3.Distance(transform.position, destination) <= 0.1f)
                {
                    agent.enabled = false;
                    ChangeToState(EnemyState.Idle);
                }
                break;
            case EnemyState.Attack:
                break;
            case EnemyState.Dead:
                break;
        }
    }

    private void ChangeToState(EnemyState newState)
    {
        //Exit state
        switch (state)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Seek:
                break;
            case EnemyState.Attack:
                break;
            case EnemyState.Dead:
                break;
        }

        state = newState;

        //Enter State
        switch (state)
        {
            case EnemyState.Idle:
                currentTimeIdle = 0;
                animator.SetFloat("Speed", 0);
                break;
            case EnemyState.Seek:
                agent.enabled = true;
                destination = transform.position;
                if (GetRandomPoint(transform.position, out destination, 10f))
                {
                    //Debug.Log("Seek");
                    agent.SetDestination(destination);
                    animator.SetFloat("Speed", 1);
                }
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Dead:
                agent.enabled = false;
                break;
        }
        }

    private bool GetRandomPoint(Vector3 center, out Vector3 result, float range = 10.0f)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    public void ShowTarget()
    {
        targetCircle.SetActive(true);
    }

    public void HideTarget()
    {
        targetCircle.SetActive(false);
    }

    public override void OnDeath()
    {
        base.OnDeath();
        ChangeToState(EnemyState.Dead);
        HideTarget();
    }
}
