using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
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
    [SerializeField] private float rotationSpeed = 90f;

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
        if (weaponType == WeaponType.Rotate)
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
        if (other.CompareTag("Character"))
        {
            if (!CompareParent(other.gameObject.transform))
            {
                Character character = other.gameObject.GetComponent<Character>();
                character.OnDeath();
                Destroy(this.gameObject);
            }
        }
    }
}
