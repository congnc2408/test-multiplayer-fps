using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float walkSpeed = 4f;
    public float sprintSpeed = 14f;
    public float maxVelocityChange = 10f;
    [Space]
    public float airControl = 0.5f;
    [Space]
    public float jumpHeight = 7f;

    private Vector2 input;
    private Rigidbody rigidbody;
    private bool sprinting;
    private bool jumping;
    private bool grounded = false;

    [Header("Animation")]
    public Animation handAnimation;
    public AnimationClip handWalkAnimation;
    public AnimationClip idleAnimation;
    private PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        photonView = GetComponent<PhotonView>();

    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            input.Normalize();
            sprinting = Input.GetButton("Sprint");
            jumping = Input.GetButton("Jump");
        }
    }

    void OnTriggerStay(Collider other)
    {
        grounded = true;
    }

    void FixedUpdate()
    {
        if (grounded)
        {
            if (jumping)
            {
                rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpHeight, rigidbody.velocity.z);
            }
            else if (input.magnitude > 0.5f)
            {
                handAnimation.clip = handWalkAnimation;
                handAnimation.Play();
                rigidbody.AddForce(CalculateMovement(sprinting ? sprintSpeed : walkSpeed), ForceMode.VelocityChange);
            }
            else
            {
                handAnimation.clip = idleAnimation;
                handAnimation.Play();
                var velocity1 = rigidbody.velocity;
                velocity1 = new Vector3(velocity1.x * 0.2f * Time.fixedDeltaTime, velocity1.y, velocity1.z * 0.2f * Time.fixedDeltaTime);
                rigidbody.velocity = velocity1;
            }
        }
        else
        {
            if (input.magnitude > 0.5f)
            {
                rigidbody.AddForce(CalculateMovement(sprinting ? sprintSpeed * airControl : walkSpeed * airControl), ForceMode.VelocityChange);
            }
            else
            {
                var velocity1 = rigidbody.velocity;
                velocity1 = new Vector3(velocity1.x * 0.2f * Time.fixedDeltaTime, velocity1.y, velocity1.z * 0.2f * Time.fixedDeltaTime);
                rigidbody.velocity = velocity1;
            }
        }
        grounded = false;

    }

    Vector3 CalculateMovement(float _speed)
    {
        Vector3 targetVelocity = new Vector3(input.x, 0, input.y);
        targetVelocity = transform.TransformDirection(targetVelocity);
        targetVelocity *= _speed;
        Vector3 velocity = rigidbody.velocity;

        if (input.magnitude > 0.5f)
        {
            Vector3 velocityChange = targetVelocity - velocity;
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;

            return (velocityChange);
        }
        else
        {
            return new Vector3();
        }

    }
}
