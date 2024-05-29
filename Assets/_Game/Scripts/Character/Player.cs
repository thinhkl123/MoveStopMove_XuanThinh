using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
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
        OnInit();
    }

    public override void OnInit()
    {
        base.OnInit();
        visual.rotation = Quaternion.identity;
    }

    private void Update()
    {
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
}