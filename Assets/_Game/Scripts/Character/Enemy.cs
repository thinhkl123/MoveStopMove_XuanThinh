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
    [SerializeField] private float timeIdle;
    public Target targetArrow;

    private float currentTimeIdle;
    private NavMeshAgent agent;
    private Vector3 destination;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        //OnInit();
    }

    public override void OnInit()
    {
        base.OnInit();
        
        ChangeToState(EnemyState.Idle);
        destination = transform.position;
        agent.enabled = false;
        ChangeWeapon();
        //agent.stoppingDistance = radius;
    }

    private void Update()
    {
        if (GameManager.Ins.state != GameManager.GameState.Playing)
        {
            return;
        }

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
                timeIdle = Random.Range(2, 5f);
                break;
            case EnemyState.Seek:
                agent.enabled = true;
                destination = transform.position;
                if (GetRandomPoint(transform.position, out destination, 20f))
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
                GameManager.Ins.UpdateAliveChar();
                agent.enabled = false;
                /*
                if (weapon != null)
                {
                    //Destroy(weapon.gameObject);
                    weapon.DeSpawn();
                }
                */
                Invoke(nameof(DeSpawn), 1f);
                break;
        }
    }

    private void DeSpawn()
    {
        SimplePool.Despawn(this);
    }

    private bool GetRandomPoint(Vector3 center, out Vector3 result, float range = 10.0f)
    {
        for (int i = 0; i < 100; i++)
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

    public override void ResetAttack()
    {
        base.ResetAttack();
        ChangeToState(EnemyState.Seek);
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

    public void ChangeColorTarget(Color color)
    {
        targetArrow.targetColor = color;
    }

    private void ChangeWeapon()
    {
        WeaponSO weaponSO = SOManager.Ins.GetWeaponSO();

        if (weaponHand != null)
        {
            Destroy(weaponHand.gameObject);
        }

        Vector3 localPos = weaponSO.weaponOnHand.transform.position;
        Quaternion localRot = weaponSO.weaponOnHand.transform.rotation;

        weaponHand = Instantiate(weaponSO.weaponOnHand, Vector3.zero, Quaternion.identity, rightHand);

        weaponHand.transform.localPosition = localPos;
        weaponHand.transform.localRotation = localRot;

        weaponPrefab = weaponSO.weapon;
    }
}
