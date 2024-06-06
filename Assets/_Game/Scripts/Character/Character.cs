using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character : GameUnit
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected Transform visual;
    [SerializeField] protected float initalRadius = 1.25f * 5f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Collider col;

    //scale
    public float scale = 1f;
    public float newScale;
    protected float radius;

    //Skin
    [SerializeField] protected Transform rightHand;
    [SerializeField] protected Transform leftHand;
    [SerializeField] protected Transform hair;
    [SerializeField] protected SkinnedMeshRenderer pant;
    [SerializeField] protected SkinnedMeshRenderer body;

    //Attack 
    [SerializeField] protected Weapon weaponPrefab;
    [SerializeField] private Transform launchPoint;
    [SerializeField] protected GameObject weaponHand;
    private Collider[] colliders;
    public bool canAttack;
    public Transform target;

    //Dead
    protected bool isDead = false;

    //Score
    public int score;

    //ScoreBarUI
    [SerializeField] private ScoreBarUI scoreBarUI;

    //Buff
    protected float rangeBuf = 0;
    protected float speedBuf = 0;
    protected float goldBuf = 0;

    public virtual void OnInit()
    {
        scale = 1f;
        newScale = 1f;
        canAttack = true;
        isDead = false;
        //visual.rotation = Quaternion.identity;
        //transform.rotation = Quaternion.identity;
        animator.SetFloat("Speed", 0);
        animator.SetTrigger("Idle");
        score = 0;
        scoreBarUI.UpdateBar(score);
        radius = initalRadius * scale;
    }

    public void GetTarget()
    {
        colliders = Physics.OverlapSphere(transform.position, radius, layerMask);

        int index = Array.IndexOf(colliders, col);
        if (index >= 0)
        {
            colliders = colliders.Where((val, idx) => idx != index).ToArray();
        }

        if (colliders.Length <= 0)
        {
            return;
        }

        float minDis = Vector3.Distance(transform.position, colliders[0].gameObject.transform.position);
        target = colliders[0].gameObject.transform;

        for (int i = 1; i < colliders.Length; i++)
        {
            //minDis = Mathf.Min(minDis, Vector3.Distance(transform.position, colliders[i].transform.position));
            if (minDis > Vector3.Distance(transform.position, colliders[i].gameObject.transform.position))
            {
                minDis = Vector3.Distance(transform.position, colliders[i].gameObject.transform.position);
                target = colliders[i].gameObject.transform;
            }
        }
    }

    public bool CanAttack()
    {
        GetTarget();

        return colliders.Length > 0;
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");

        //target.transform.position = new Vector3(target.position.x, 0f, target.position.z);

        //Debug.Log(target.transform.position);

        visual.forward = target.position - transform.position;

        canAttack = false;
        StartCoroutine(CoolDownAttackTime());
    }

    IEnumerator CoolDownAttackTime()
    {
        yield return new WaitForSeconds(3f);
        if (!isDead)
        {
            ResetAttack();
        }
    }

    public virtual void LaunchWeapon()
    {
        //Weapon weapon = Instantiate(weaponPrefab, launchPoint.position, weaponPrefab.transform.rotation);
        Weapon weapon = SimplePool.Spawn<Weapon>(weaponPrefab, launchPoint.position, weaponPrefab.transform.rotation);
        //Debug.Log(weapon);
        if (weapon != null)
        {
            weapon.OnInit();
            weapon.SetParent(transform);
            weapon.SetRange(radius);
            weapon.Launch(target.position, speedBuf);
        }
        weaponHand.SetActive(false);
    }

    public virtual void ResetAttack()
    {
        canAttack = true;
        weaponHand.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        /*
        if (other.CompareTag("Weapon"))
        {
            Weapon weapon = other.gameObject.GetComponent<Weapon>();
            if (!weapon.CompareParent(this.transform))
            {
                OnDeath();
            }
        }
        */
    }

    public virtual void OnDeath()
    {
        if (isDead)
        {
            return;
        }

        //Debug.Log("OnDeath");

        isDead = true;
        col.enabled = false;
        animator.SetTrigger("Dead");
    }

    public void ChangeBody(Material material)
    {
        body.material = material;
    }

    public void ChangeColorBar(Color color)
    {
        scoreBarUI.ChangeColorBar(color);
    }

    public virtual void ChangeScore(int value)
    {
        //Debug.Log(value);
        score += value;
        int curScore = (int) (scale - 1) * 10 + 10;
        scoreBarUI.UpdateBar(score);
        if (score >= curScore)
        {
            scale += 0.1f;
            if (scale <= 2f)
            {
                newScale = scale;
                if (rangeBuf != 0)
                {
                    newScale += scale * (rangeBuf / 100);
                }
                transform.localScale = new Vector3(newScale, newScale, newScale);
                radius = initalRadius * newScale;
            }
        }
    }

    public void UpdateScore(int value)
    {
        for (int i = 0; i < value; i++)
        {
            ChangeScore(1);
        }
    }
}
