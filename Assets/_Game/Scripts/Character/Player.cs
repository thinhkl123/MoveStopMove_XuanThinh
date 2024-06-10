using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player : Character
{
    public static Player Instance
    {
        get;
        private set;
    }

    public static event EventHandler OnLose;
    public static event EventHandler<OnGetRewardEventArgs> OnGetReward;

    public class OnGetRewardEventArgs: EventArgs
    {
        public int reward;
        public float buff;
    }

    [SerializeField] private Joystick joystick;
    [SerializeField] private float speed = 5f;
    [SerializeField] private Material defaultMaterialPant;
    [SerializeField] private Material defaultMaterialBody;

    private Rigidbody rb;
    private float horizontal;
    private float vertical;
    private Vector3 movement;

    //Skin
    private GameObject hairOb;
    private GameObject shieldOb;

    //SetFull
    [SerializeField] private Transform wingPos;
    [SerializeField] private Transform tailPos;
    [SerializeField] private Transform headPos;

    private GameObject wingOb;
    private GameObject tailOb;
    private GameObject headOb;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        LevelManager.Ins.OnLoadLevel += LevelManager_OnLoadLevel;
        GameManager.Ins.OnWin += GameManager_OnWin;

        ChangeCurrentSkin();
        //OnInit();
    }

    private void GameManager_OnWin(object sender, System.EventArgs e)
    {
        animator.SetTrigger("Dead");
        OnGetReward?.Invoke(this, new OnGetRewardEventArgs()
        {
            reward = score
        });
    }

    private void LevelManager_OnLoadLevel(object sender, System.EventArgs e)
    {
        OnInit();
    }

    public override void OnInit()
    {
        base.OnInit();
        visual.rotation = Quaternion.identity;
        transform.position = Vector3.zero;
    }

    private void Update()
    {
        if (GameManager.Ins.state != GameManager.GameState.Playing)
        {
            return;
        }

        if (isDead)
        {
            rb.velocity = Vector3.zero;
            return;
        }

        GetTarget();

        horizontal = joystick.Horizontal;
        vertical = joystick.Vertical;

        //horizontal = Input.GetAxisRaw("Horizontal");
        //vertical = Input.GetAxisRaw("Vertical");

        movement = new Vector3(horizontal * speed, 0, vertical * speed);
        rb.velocity = movement;
        if (movement != Vector3.zero)
        {
            visual.forward = movement;
            animator.SetFloat("Speed", movement.magnitude);
            ResetAttack();
        }
        else
        {
            //Debug.Log(Input.GetMouseButton(1));
            if (canAttack && CanAttack())
            {
                Attack();
            }
            else
            {
                //Debug.Log("Idle");
                animator.SetFloat("Speed", 0);
            }
        }

        //Debug.Log(CanAttack());
    }

    public override void LaunchWeapon()
    {
        base.LaunchWeapon();
        SoundManager.Ins.PlayThrowWeaponSound();
    }

    public override void GetTarget()
    {
        base.GetTarget();
        if (target == null)
        {
            return;
        }
        Enemy enemy = target.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.ShowTarget();
        }
    }

    public override void OnDeath()
    {
        base.OnDeath();
        OnGetReward?.Invoke(this, new OnGetRewardEventArgs()
        {
            reward = score,
            buff = goldBuf,
        });
        OnLose?.Invoke(this, EventArgs.Empty);
    }

    public void ChangeCurrentSkin()
    {
        rangeBuf = 0;
        speedBuf = 0;
        goldBuf = 0;

        ChangeWeapon(DataManager.Ins.GetCurrentWeaponId());

        ChangeHair(DataManager.Ins.GetCurrentHairId());

        ChangePant(DataManager.Ins.GetCurrentPantId());

        ChangeShield(DataManager.Ins.GetCurrentShieldId());

        ChangeSetFull(DataManager.Ins.GetCurrentSetFullId());
    }

    public void ChangeWeapon(int value)
    {
        WeaponSO weaponSO = SOManager.Ins.GetWeaponSO(value - 1);

        if (weaponHand != null)
        {
            Destroy(weaponHand.gameObject);
        }

        Vector3 localPos = weaponSO.weaponOnHand.transform.position;
        Quaternion localRot = weaponSO.weaponOnHand.transform.rotation;

        weaponHand = Instantiate(weaponSO.weaponOnHand, Vector3.zero, Quaternion.identity, leftHand);

        weaponHand.transform.localPosition = localPos;
        weaponHand.transform.localRotation = localRot;

        weaponPrefab = weaponSO.weapon;

        speedBuf += weaponSO.speedBuf;
        rangeBuf += weaponSO.rangeBuf;
    }

    public void ChangeHair(int value)
    {
        if (value == 0)
        {
            if (hairOb != null)
            {
                Destroy(hairOb.gameObject);
            }
            return;
        }

        HairSO hairSO = SOManager.Ins.GetHairSO(value - 1);

        if (hairOb != null)
        {
            Destroy(hairOb.gameObject);
        }

        Vector3 localPos = hairSO.prefab.transform.position;
        Quaternion localRot = hairSO.prefab.transform.rotation;

        hairOb = Instantiate(hairSO.prefab, Vector3.zero, Quaternion.identity, hair);

        hairOb.transform.localPosition = localPos;
        hairOb.transform.localRotation = localRot;

        rangeBuf += hairSO.rangeBuf;
    }

    public void ChangePant(int value)
    {
        if (value == 0)
        {
            pant.material = defaultMaterialPant;
            return;
        }

        PantSO pantSO = SOManager.Ins.GetPantSO(value - 1);

        pant.material = pantSO.material;

        speedBuf += pantSO.speedBuf;
    }

    public void ChangeShield(int value)
    {
        if (value == 0)
        {
            if (shieldOb != null)
            {
                Destroy(shieldOb.gameObject);
            }
            return;
        }

        ShieldSO shieldSO = SOManager.Ins.GetShielSO(value - 1);

        if (shieldOb != null)
        {
            Destroy(shieldOb.gameObject);
        }

        Vector3 localPos = shieldSO.prefab.transform.position;
        Quaternion localRot = shieldSO.prefab.transform.rotation;

        shieldOb = Instantiate(shieldSO.prefab, Vector3.zero, Quaternion.identity, rightHand);

        shieldOb.transform.localPosition = localPos;
        shieldOb.transform.localRotation = localRot;

        goldBuf += shieldSO.goldBuf;
    }

    public void ChangeSetFull(int value)
    {
        if (wingOb != null)
        {
            Destroy(wingOb.gameObject);
        }
        if (tailOb != null)
        {
            Destroy(tailOb.gameObject);
        }
        if (headOb != null)
        {
            Destroy(headOb.gameObject);
        }

        body.material = defaultMaterialBody;

        if (value == 0)
        {
            return;
        }

        SetFullSO setFullSO = SOManager.Ins.GetSetFullSO(value - 1);

        body.material = setFullSO.materialBody;

        if (setFullSO.wing != null)
        {
            ChangeWing(setFullSO.wing);
        }
        if (setFullSO.tail != null)
        {
            ChangeTail(setFullSO.tail);
        }
        if (setFullSO.specialHair != null)
        {
            ChangeHead(setFullSO.specialHair);
        }
    }

    private void ChangeWing(GameObject wingPb)
    {
        Vector3 localPos = wingPb.transform.position;
        Quaternion localRot = wingPb.transform.rotation;

        wingOb = Instantiate(wingPb, Vector3.zero, Quaternion.identity, wingPos);

        wingOb.transform.localPosition = localPos;
        wingOb.transform.localRotation = localRot;
    }
     
    private void ChangeTail(GameObject tailPb)
    {
        Vector3 localPos = tailPb.transform.position;
        Quaternion localRot = tailPb.transform.rotation;

        tailOb = Instantiate(tailPb, Vector3.zero, Quaternion.identity, tailPos);

        tailOb.transform.localPosition = localPos;
        tailOb.transform.localRotation = localRot;
    }

    private void ChangeHead(GameObject headPb)
    {
        Vector3 localPos = headPb.transform.position;
        Quaternion localRot = headPb.transform.rotation;

        headOb = Instantiate(headPb, Vector3.zero, Quaternion.identity, headPos);

        headOb.transform.localPosition = localPos;
        headOb.transform.localRotation = localRot;
    }
}