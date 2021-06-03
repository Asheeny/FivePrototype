using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 0f;
    private Vector3 moveInput = new Vector3();

    [SerializeField]
    private CameraShake cShake = null;
    [SerializeField]
    private CinemachineVirtualCamera cam = null;

    private Rigidbody rB;
    private Animator anim;

    private bool isGrounded = true;
    [SerializeField]
    private LayerMask whatIsGround = new LayerMask();
    [SerializeField]
    private float fallMultiplier = 2.5f;
    [SerializeField]
    private float lowJumpMultiplier = 2f;
    [SerializeField]
    private float jForce = 0f;

    [SerializeField]
    Transform startPos = null;
    [SerializeField]
    Transform groundCheck = null;

    private bool stunned = false;
    private bool jumping= false;
    private bool holdingJump= false;

    void Awake()
    {
        rB = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        transform.position = startPos.position;
    }

    private void Update()
    {
        if (stunned)
        {
            anim.SetBool("isStunned", true);
            anim.SetTrigger("stun");
            return;
        }
        else
        {
            anim.SetBool("isStunned", false);
        }

        if (Physics.CheckSphere(groundCheck.position, 0.5f, whatIsGround))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        jumping = Input.GetButtonDown("Jump");
        holdingJump = Input.GetButton("Jump");

        if (rB.velocity.y < 0)
        {
            // - 1 to fall multiplier accounts for existing unity physics
            rB.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rB.velocity.y < 0 && !holdingJump)
        {
            rB.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        Movement();
        Jumping(); 
    }

    private void Jumping()
    {
        if (jumping && isGrounded)
        {
            anim.SetBool("isJumping", true);
            anim.SetTrigger("startJump");
            FindObjectOfType<AudioController>().Play("Jump", Random.Range(0.9f, 1.1f));

            rB.velocity = jForce * Vector3.up;
        }
        else
        {
            anim.SetBool("isJumping", false);
        }
    }

    private void Movement()
    {
        rB.MoveRotation(Quaternion.Euler(0, cam.transform.rotation.y * 180, 0));
        rB.MovePosition(transform.position + (transform.TransformDirection(moveInput) * speed * Time.deltaTime));

        if (moveInput.magnitude == 0)
        {
            anim.SetBool("isMoving", false);
        }
        else
        {
            anim.SetBool("isMoving", true);
        }
    }

    public IEnumerator Stun(Vector3 knockBackDir, float force)
    {
        stunned = true;
        StartCoroutine(cShake.Shake(0.15f, 0.16f));

        rB.AddRelativeForce(new Vector3(knockBackDir.x * -force, knockBackDir.y * 50, knockBackDir.z * -force));

        yield return new WaitForSeconds(1f);
        stunned = false;
    }

    public bool IsStunned()
    {
        return stunned;
    }

    public void SetStun(bool value)
    {
        stunned = value;
    }

    public bool DetectMovement()
    {
        if (Input.GetButton("Jump") || moveInput.Equals(0))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
