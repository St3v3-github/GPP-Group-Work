using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Transform cameraObject;
    Vector3 moveDirection;
    Rigidbody playerRigidbody;
    public float rotationSpeed = 15;

    public float jumpHeight = 3;
    public float gravityIntensity = -15;

    AnimatorManager animatorManager;
    Animator animator;

    CameraController cameraManger;

    private float moveAmount;
    private Vector2 _input;

    private Vector3 _direction;


    [SerializeField] public float speed;
    [SerializeField] public float speed2;

    [SerializeField] private float jumpPower;

    public bool isJumping;

    public bool isAttacking = false;

    public bool onGround;

    public bool isInteracting;

    public float inAirTimer;
    public float leapingVelocity;
    public float fallingSpeed;
    public LayerMask groundLayer;
    public float rayCastHeightOffset = 0.5f;

    public bool jumpActivated;
    private bool doubleJump;
    public bool canDoubleJump = false;
    DoubleJumpEnabled doubleJumpEnabled;

    public bool speedBoost;
    public bool jumpBoost;

    public float health;
    public Transform player;
    public Transform respawnPoint;
    public bool isDead;
    public bool isDeadAnimComplete;

    public GameObject transitionOBJ;
    public LevelLoader levelLoaderRef;

    bool switchCollide = false;
    public bool interact = false;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;

        cameraManger = FindObjectOfType<CameraController>();

        animatorManager = GetComponent<AnimatorManager>();
        animator = GetComponent<Animator>();

        levelLoaderRef = transitionOBJ.GetComponent<LevelLoader>();
    }

    public void DoubleJump()
    {
        jumpBoost = true;
    }
    public void BoostSpeed()
    {
        speedBoost = true;
    }

    private void Update()
    {
        ApplyAllMovement();
        JumpInput();
    }

    private void FixedUpdate()
    {

    }

    public void ApplyAllMovement()
    {
        FallingAndLanding();

        if (isInteracting)
            return;

        ApplyMovement();
        ApplyRotation();
        CheckMoveAmount();
    }

    private void ApplyRotation()
    {

        if (isJumping)
            return;

        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * _input.y;
        targetDirection = targetDirection + cameraObject.right * _input.x;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if(targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    private void ApplyMovement()
    {
        if (isJumping)
            return;

        moveDirection = cameraObject.forward * _input.y;
        moveDirection = moveDirection + cameraObject.right * _input.x;
        moveDirection.Normalize();
        moveDirection.y = 0;
        moveDirection = moveDirection * speed;

        if(moveAmount >= 0.5f)
        {
            moveDirection = moveDirection * speed2;
        }
        else
        {
            moveDirection = moveDirection * speed;
        }

        Vector3 movementVelocity = moveDirection;
        playerRigidbody.velocity = movementVelocity;

    }

    private void FallingAndLanding()
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        rayCastOrigin.y = rayCastOrigin.y + rayCastHeightOffset;

        if(!onGround && !isJumping)
        {
            if(!isInteracting)
            {
                animatorManager.PlayTargetAnimation("Fall", true);

            }

            inAirTimer = inAirTimer + Time.deltaTime;
            playerRigidbody.AddForce(transform.forward * leapingVelocity);
            playerRigidbody.AddForce(-Vector3.up * fallingSpeed * inAirTimer);
        }

        if(Physics.SphereCast(rayCastOrigin, 0.2f, -Vector3.up, out hit, groundLayer))
        {
            if(!onGround && !isInteracting)
            {
                animatorManager.PlayTargetAnimation("Land", true);
            }

            inAirTimer = 0;
            onGround = true;
        }

        else
        {
            onGround = false;
        }
    }

    private void JumpInput()
    {
        if(jumpActivated)
        {
            jumpActivated = false;
            Jumping();
        }
    }

    public void Jumping()
    {
        if (onGround && !jumpActivated)
        {
            doubleJump = false;
        }

        if (onGround || canDoubleJump && doubleJump)
        {

            animatorManager.animator.SetBool("isJumping", true);
            animatorManager.PlayTargetAnimation("Jump", false);

            float jumpVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
            Vector3 playerVelocity = moveDirection;
            playerVelocity.y = jumpVelocity;
            playerRigidbody.velocity = playerVelocity;

            doubleJump = !doubleJump;
        }

    }

    public void Move(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();

    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            interact = true;
            print("interact");
        }
    }

    private void CheckMoveAmount()
    {
        moveAmount = Mathf.Clamp01(Mathf.Abs(_input.x) + Mathf.Abs(_input.y));
        animatorManager.UpdateAnimatorValues(0, moveAmount);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            jumpActivated = true;
        }
    }

    private void LateUpdate()
    {
        cameraManger.HandleAllCameraMovement();

        isInteracting = animator.GetBool("isInteracting");
        isJumping = animator.GetBool("isJumping");
        animator.SetBool("isGrounded", onGround);
        isAttacking = animator.GetBool("isAttacking");
        isDeadAnimComplete = animator.GetBool("isDead");

    }

    public void Attack(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            isAttacking = true;

        }
        if(context.canceled)
        {

        }
        
        if(isAttacking)
        {
            animatorManager.PlayTargetAnimation("Attack", false);
            animator.SetBool("isAttacking", true);
        }
        if(!isAttacking)
        {

        }

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 3)
        {
            animatorManager.PlayTargetAnimation("Stumble", false);

            if(health <= 0)
            {
                Debug.Log("DEAD");
                Death();
            }
        }

        else
        {
            animatorManager.PlayTargetAnimation("GetHit", false);
        }
    }

    private void Death()
    {
        animatorManager.PlayTargetAnimation("Die", false);
        isDead = true;
        if(isDeadAnimComplete)
        {
            Respawn();
        }
        
    }

    private void Respawn()
    {
        player.transform.position = respawnPoint.transform.position;
        health = 20;
        isDead = false;
        animator.SetBool("isDead", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "transition")
        {
            levelLoaderRef.LoadNextLevel();
        }
    }

}
