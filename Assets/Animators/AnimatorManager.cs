using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator animator;
    int horizontal;
    int vertical;
    //int jumpBool;
    //int groundedBool;
    int attackBool;
    //int fallBool;
    public void Awake()
    {
        animator = GetComponent<Animator>();
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
        //jumpBool = Animator.StringToHash("isJumping");
        //groundedBool = Animator.StringToHash("isGrounded");
       // attackBool = Animator.StringToHash("isAttacking");
        //fallBool = Animator.StringToHash("isFalling");
    }

    public void PlayTargetAnimation(string targetAnimation, bool isInteracting)
    {
        animator.SetBool("isInteracting", isInteracting);
        animator.CrossFade(targetAnimation, 0.2f);
    }

    public void UpdateAnimatorValues(float hoizontalMovement, float verticalMovement)
    {
        float horizontalSnap;
        float verticalSnap;
        //Horizontal
        if(hoizontalMovement > 0 && hoizontalMovement < 0.55f)
        {
            horizontalSnap = 0.5f;
        }
        else if(hoizontalMovement > 0.55f)
        {
            horizontalSnap = 1;
        }
        else if(hoizontalMovement < 0 && hoizontalMovement > -0.55f)
        {
            horizontalSnap = -0.5f;
        }
        else if(hoizontalMovement < -0.55f)
        {
            horizontalSnap = -1;
        }
        else
        {
            horizontalSnap = 0;
        }
        //Vertical
        if (verticalMovement > 0 && verticalMovement < 0.55f)
        {
            verticalSnap = 0.5f;
        }
        else if (verticalMovement > 0.55f)
        {
            verticalSnap = 1;
        }
        else if (verticalMovement < 0 && verticalMovement > -0.55f)
        {
            verticalSnap = -0.5f;
        }
        else if (verticalMovement < -0.55f)
        {
            verticalSnap = -1;
        }
        else
        {
            verticalSnap = 0;
        }
        animator.SetFloat(horizontal, horizontalSnap, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical, verticalSnap, 0.1f, Time.deltaTime);


    }

   // public void UpdateJump(bool isJumping = false, bool isGrounded = true)
   // {
   //     if (isJumping)
   //     {
   //         animator.SetBool(jumpBool, true);
   //         animator.SetBool(groundedBool, false);
   //     }

   //     if(!isJumping && !isGrounded)
   //     {
   //         animator.SetBool(fallBool, true);
   //     }

   //     if(!isJumping && isGrounded)
   //     {
   //         animator.SetBool(groundedBool, true);
   //         animator.SetBool(jumpBool, false);
   //     }
   // }
  //  public void UpdateAttack(bool attacking = false)
  //  {
  //      if(attacking)
  //      {
  //          animator.SetBool(attackBool, true);
  //      }

   // }
}
