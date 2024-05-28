using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : GameUnit
{
    public enum WeaponType
    {
        Rotate,
        Straight,
        Bomerang
    }

    public WeaponType weaponType;

    [SerializeField] private float force = 10f;
    [SerializeField] private float torqueMag = 5f;

    private Transform parent;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        //Destroy(this.gameObject, 5f);
    }

    public void Launch(Vector3 direction)
    {
        rb.AddForce(direction.normalized * force, ForceMode.Impulse);
        //rb.AddTorque(Vector3.up * torqueMag);
        if (weaponType == WeaponType.Rotate || weaponType == WeaponType.Bomerang)
        {
            rb.AddTorque(Vector3.up * torqueMag, ForceMode.Impulse);
        }
    } 

    public void SetParent(Transform parent)
    {
        this.parent = parent;
    }

    public bool CompareParent(Transform other)
    {
        return other == parent;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Circle") || other.CompareTag("Weapon"))
        {
            return;
        }

        if (other.CompareTag("Character"))
        {
            if (!CompareParent(other.gameObject.transform))
            {
                //Character character = other.gameObject.GetComponent<Character>();
                Character character = Cache.GetCharacter(other);
                character.OnDeath();
            }
        }
        Destroy(this.gameObject);
    }
}
