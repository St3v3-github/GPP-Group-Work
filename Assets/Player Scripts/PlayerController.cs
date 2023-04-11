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
   // private CharacterController _characterController;
    private Vector3 _direction;

   // [SerializeField] private float smoothTime = 0.05f;
   // private float _currentVelocity;

    [SerializeField] public float speed;
    [SerializeField] public float speed2;


    // private float _gravity = -9.81f;
    // [SerializeField] private float gravityMultiplyier = 3.0f;
    // private float _velocity;

    // private bool IsGrounded() => _characterController.isGrounded;

    [SerializeField] private float jumpPower;

    public bool isJumping;

    public bool isAttacking = false;

    public bool onGround;

  //  private bool started = false;

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

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
       // _characterController = GetComponent<CharacterController>();
        cameraManger = FindObjectOfType<CameraController>();

        animatorManager = GetComponent<AnimatorManager>();
        animator = GetComponent<Animator>();

        //doubleJumpEnabled = GameObject.FindGameObjectWithTag("JumpCollectable").GetComponent<DoubleJumpEnabled>();
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
        //ApplyGravity();
        //ApplyRotation();
        //ApplyMovement();
        ApplyAllMovement();
        JumpInput();
       //CheckJump();

        //if(started)
        //{
        //    if(_characterController.isGrounded)
        //    {
        //        onGround = true;
        //        Debug.Log("on ground");
        //        if(onGround)
        //        {
        //            animatorManager.UpdateJump(false, true);
        //            started = false;
        //        }

        //    }

        //    if(!_characterController.isGrounded)
         //   {
                 //Debug.Log("in air");
         //        onGround = false;
          //  }
       // }
    }

    private void FixedUpdate()
    {
        //ApplyRotation();
        //ApplyMovement();
    }

   // private void ApplyGravity()
   // {
   //     if (IsGrounded() && _velocity < 0.0f)
   //     {
   //         _velocity = -1.0f;
   //     }
   //     else
   //     {
   //         _velocity += _gravity * gravityMultiplyier * Time.deltaTime;
   //     }

    //    moveDirection.y = _velocity;

   // }

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
        //if (_input.sqrMagnitude == 0) return;

        //var targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
        //var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelocity, smoothTime);
        //transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);

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
        //_characterController.Move(movementVelocity);

        //_characterController.Move( _direction * speed * Time.deltaTime);

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
       // if(doubleJumpEnabled.jumpCollected)
       // {
       //     canDoubleJump = true;
       // }

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
        //_direction = new Vector3(_input.x, 0.0f, _input.y);

        //moveAmount = Mathf.Clamp01(Mathf.Abs(_direction.x) + Mathf.Abs(_direction.y));

    }

    private void CheckMoveAmount()
    {
        moveAmount = Mathf.Clamp01(Mathf.Abs(_input.x) + Mathf.Abs(_input.y));
        animatorManager.UpdateAnimatorValues(0, moveAmount);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        //if (!context.started) return;
        //if (!IsGrounded()) return;
        if(context.started)
        {
            jumpActivated = true;
            //started = true;
         //   isJumping = true;
        }
       // if(context.canceled)
       // {
       //     isJumping = false;
       //     Debug.Log("Ok");
       // }

      // if(isJumping)
      //  {
       //     Debug.Log("Jumping");
       //     //_velocity += jumpPower;
       //     float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
       //     Vector3 playerVelocity = moveDirection;
       //     playerVelocity.y = jumpingVelocity;
       //     playerRigidbody.velocity = playerVelocity;
       //     animatorManager.UpdateJump(true, false);
        //    isJumping = false;
       // }

       // if(!isJumping && !onGround)
       // {
        //    animatorManager.UpdateJump(false, false);
        //    Debug.Log("falling");
       // }
    }

   // public void CheckJump()
   // {
   //     if(started == true)
    //    {
    //        return;
    //    }
   // }

    private void LateUpdate()
    {
        cameraManger.HandleAllCameraMovement();

        isInteracting = animator.GetBool("isInteracting");
        isJumping = animator.GetBool("isJumping");
        animator.SetBool("isGrounded", onGround);
        isAttacking = animator.GetBool("isAttacking");
        isDeadAnimComplete = animator.GetBool("isDead");

        //Debug.Log(isAttacking);
    }

    public void Attack(InputAction.CallbackContext context)
    {
        //if (!context.started) return;
        if(context.started)
        {
            isAttacking = true;

        }
        if(context.canceled)
        {
            //isAttacking = false;
        }
        
        if(isAttacking)
        {
            animatorManager.PlayTargetAnimation("Attack", false);
            animator.SetBool("isAttacking", true);
            //isAttacking = false;
        }
        if(!isAttacking)
        {
            //Debug.Log("not attacking");
           // animator.SetBool("isAttacking", false);
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
        //player.SetActive(false);
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

}
