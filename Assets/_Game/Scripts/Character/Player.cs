using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public static event EventHandler OnLose;
    public static event EventHandler<OnGetRewardEventArgs> OnGetReward;

    public class OnGetRewardEventArgs: EventArgs
    {
        public int reward;
    }

    [SerializeField] private Joystick joystick;
    [SerializeField] private float speed = 5f;

    private Rigidbody rb;
    private float horizontal;
    private float vertical;
    private Vector3 movement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        LevelManager.Ins.OnLoadLevel += LevelManager_OnLoadLevel;
        GameManager.Ins.OnWin += GameManager_OnWin;

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

    public override void OnDeath()
    {
        base.OnDeath();
        OnGetReward?.Invoke(this, new OnGetRewardEventArgs()
        {
            reward = score
        });
        OnLose?.Invoke(this, EventArgs.Empty);
    }
}