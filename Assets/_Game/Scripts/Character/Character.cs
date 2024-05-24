using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected Transform visual;
    [SerializeField] protected float radius = 1.25f * 5f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Collider col;

    public float scale = 1f;

    //Skin
    [SerializeField] protected Transform rightHand;
    [SerializeField] protected Transform leftHand;
    [SerializeField] protected Transform hair;
    [SerializeField] protected SkinnedMeshRenderer pant;

    //Attack 
    [SerializeField] private Weapon weaponPrefab;
    [SerializeField] private Transform launchPoint;
    [SerializeField] private GameObject weaponHand;
    private Collider[] colliders;
    public bool canAttack;
    private Transform target;

    //Dead
    private bool isDead = false;


    public bool CanAttack()
    {
        colliders = Physics.OverlapSphere(transform.position, radius, layerMask);

        int index = Array.IndexOf(colliders, col);
        if (index >= 0)
        {
            colliders = colliders.Where((val, idx) => idx != index).ToArray();
        }

        return colliders.Length > 0;
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");

        float minDis = Vector3.Distance(transform.position, colliders[0].transform.position);
        target = colliders[0].transform;

        for (int i = 1; i < colliders.Length; i++)
        {
            //minDis = Mathf.Min(minDis, Vector3.Distance(transform.position, colliders[i].transform.position));
            if (minDis > Vector3.Distance(transform.position, colliders[i].transform.position))
            {
                minDis = Vector3.Distance(transform.position, colliders[i].transform.position);
                target = colliders[i].transform;
            }
        }

        visual.forward = target.position - transform.position;

        canAttack = false;
        //StartCoroutine(CoolDownAttackTime());
    }

    IEnumerator CoolDownAttackTime()
    {
        yield return new WaitForSeconds(2f);
        canAttack = true;
    }

    public void LaunchWeapon()
    {
        Weapon weapon = Instantiate(weaponPrefab, launchPoint.position, weaponPrefab.transform.rotation);
        if (weapon != null)
        {
            weapon.Launch(target.position - transform.position);
            weapon.SetParent(transform);
        }
        weaponHand.SetActive(false);
    }

    public void ResetAttack()
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
}
