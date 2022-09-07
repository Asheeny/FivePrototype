using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float maxSpeed = 0f;
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

    private Rigidbody rB;
    private Animator anim;
    private AudioController audioController;

    private Vector3 moveInput = new Vector3();
    private float playerRot = 0f;
    private bool isGrounded = true;
    private bool stunned = false;
    private bool tryToJump = false;
    private bool holdingJump = false;

    void Awake()
    {
        rB = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        audioController = FindObjectOfType<AudioController>();
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, whatIsGround);
        Vector3 tempPlayerRot = moveInput;
        if(!moveInput.magnitude.Equals(0))
            playerRot = Mathf.Atan2(tempPlayerRot.x, tempPlayerRot.z) * Mathf.Rad2Deg;

        if (stunned)
        {
            anim.SetBool("isStunned", true);
            anim.SetTrigger("stun");
            moveInput = Vector3.zero;
            tryToJump = false;
            holdingJump = false;
            return;
        }
        else
        {
            anim.SetBool("isStunned", false);
        }
        
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        tryToJump = Input.GetButtonDown("Jump");
        holdingJump = Input.GetButton("Jump");
    }
    
    private void FixedUpdate()
    {
        Jumping();
        Movement();
    }

    private void InAirAdjustment()
    {
        if (rB.velocity.y < 0)
        {
            // - 1 to fall multiplier accounts for existing unity physics
            rB.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rB.velocity.y > 0 && !holdingJump)
        {
            rB.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    private void Jumping()
    {
        if (tryToJump && isGrounded)
        {          
            rB.AddForce(Vector3.up * jForce, ForceMode.Impulse);

            isGrounded = false;
            tryToJump = false;

            anim.SetBool("isJumping", true);
            anim.SetTrigger("startJump");

            audioController.Play("Jump", Random.Range(0.9f, 1.1f));
        }
        else
        {
            anim.SetBool("isJumping", false);
        }

        InAirAdjustment();
    }

    private void Movement()
    {
        rB.rotation = Quaternion.Slerp(rB.rotation, Quaternion.Euler(0, playerRot, 0), 0.45f);

        rB.MovePosition(transform.position + moveInput * maxSpeed * Time.deltaTime);

        if (moveInput.magnitude.Equals(0))
        {
            anim.SetBool("isMoving", false);
        }
        else
        {
            anim.SetBool("isMoving", true);
        }
    }

    public IEnumerator Stun(float force)
    {
        stunned = true;
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
        if (Input.GetButton("Jump") || !moveInput.magnitude.Equals(0))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetPlayer()
    {
        moveInput = Vector3.zero;
        gameObject.SetActive(true);
        SetStun(false);
        transform.position = startPos.position;
    }
}
