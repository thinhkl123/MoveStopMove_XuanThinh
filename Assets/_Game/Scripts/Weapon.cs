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
    private Vector3 dir;
    private float range;

    private float speed;

    private Rigidbody rb;

    //Bomerang
    private bool isBack;
    private float timeToBack;

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

        if (weaponType == WeaponType.Bomerang)
        {
            if (isBack)
            {
                transform.position = Vector3.MoveTowards(transform.position, parent.position, speed * Time.deltaTime);
                return;
            }

            timeToBack += Time.deltaTime;
            if (timeToBack >= range / speed)
            {
                isBack = true;
            }
        }

        if (isLaunch)
        {
            //transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
            /*
            if (parent.transform == Player.Instance.transform)
            {
                Debug.Log(transform.position + " " + dir.normalized);
            }
            */
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
        //Destroy(gameObject, range / force);
        if (weaponType != WeaponType.Bomerang)
        {
            Invoke(nameof(DeSpawn), range / speed);
        }
        else
        {
            Invoke(nameof(DeSpawn), range*2 / speed);
            timeToBack = 0f;
            isBack = false;
        }
    } 

    public void SetRange(float value)
    {
        range = value;
    }

    private void SetTarget(Vector3 newTarget)
    {
        newTarget = new Vector3(newTarget.x, transform.position.y, newTarget.z);
        target = newTarget;
        dir = (target - this.transform.position);
        /*
        if (parent.transform == Player.Instance.transform)
        {
            Debug.Log(dir.normalized + " " +  target);
        }
        */
        //Debug.Log(dir.normalized);
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
        parent.gameObject.GetComponent<Character>().ResetAttack();
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
