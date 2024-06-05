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
    private bool isLaunch;
    private Vector3 target;
    private float range;

    private float speed;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnInit()
    {
        isLaunch = false;
    }

    private void Start()
    {
        //Destroy(this.gameObject, 5f);
    }

    private void Update()
    {
        if (GameManager.Ins.state == GameManager.GameState.WaitToStart)
        {
            Destroy(this.gameObject);
            return;
        }

        if (GameManager.Ins.state != GameManager.GameState.Playing)
        {
            return;
        }

        if (isLaunch)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
    }

    public void Launch(Vector3 target, float buff = 0)
    {
        SetTarget(target);
        if (buff != 0f)
        {
            speed = force * (1+ buff/100);
        }
        else
        {
            speed = force;
        }
        isLaunch = true;
        //rb.AddForce(direction.normalized * force, ForceMode.Impulse);
        //rb.AddTorque(Vector3.up * torqueMag);
        if (weaponType == WeaponType.Rotate || weaponType == WeaponType.Bomerang)
        {
            rb.AddTorque(Vector3.up * torqueMag, ForceMode.Impulse);
        }
        Destroy(gameObject, range / force);
        //Invoke(nameof(DeSpawn), range / force);
    } 

    public void SetRange(float value)
    {
        range = value;
    }

    private void SetTarget(Vector3 newTarget)
    {
        newTarget = new Vector3(newTarget.x, transform.position.y, newTarget.z);
        target = newTarget;
    }

    public void SetParent(Transform parent)
    {
        this.parent = parent;
    }

    public bool CompareParent(Transform other)
    {
        return other == parent;
    }

    public void DeSpawn()
    {
        SimplePool.Despawn(this);
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
                parent.gameObject.GetComponent<Character>().ChangeScore((int) (character.score/10) + 1);
                character.OnDeath();
            }
            else
            {
                return;
            }
        }
        //Debug.Log(other + " " + other.gameObject.tag);
        //Destroy(this.gameObject);
        DeSpawn();
    }
}
