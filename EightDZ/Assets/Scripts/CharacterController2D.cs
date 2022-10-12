using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayerMask;
    private AnimationClip animationClipAttack;
    private float groundOffset= 0.1f;//doublejump 1.3
    private Rigidbody2D body2D;
    private BoxCollider2D boxColl2D;
    private Vector2 direction;
    private Animator characterAnimator;
    private SpriteRenderer spriteRenderer;
    private const float moveSpeed = 5.0f;
    private const float jumpSpeed = 5.0f;
    private int speedY;
    private bool isDeath = false;
    private bool isJumping = false;
    private bool isAttacking = false;

    private void Awake()
    {
        body2D = GetComponent<Rigidbody2D>();
        characterAnimator = GetComponent<Animator>();
        spriteRenderer= GetComponent<SpriteRenderer>();
        boxColl2D = GetComponent<BoxCollider2D>();
        animationClipAttack=FindAnimation(characterAnimator, "NinjaAttack");
        //animationClipAttack=characterAnimator.runtimeAnimatorController.animationClips[3];
        animationClipAttack.AddEvent(new AnimationEvent(){ time = animationClipAttack.length, functionName = "OnCompletedAttackedAnimation" });
    }
    private void Update()
    {
        if (isDeath==false)
        {
            Jumping();
            Moving();
            Attacking();
        }
    }
    private void FixedUpdate()
    {
        body2D.velocity = new Vector2(direction.x *moveSpeed, body2D.velocity.y);
    }
    private void Jumping()
    {
        if (Input.GetButtonDown("Jump") && isJumping == false)
        {
            isJumping = true;
            characterAnimator.SetTrigger("Jumping");
            body2D.velocity = Vector2.up * jumpSpeed;
            characterAnimator.SetBool("IsGrounded", false);
        }
        speedY = (int)Mathf.Abs(body2D.velocity.normalized.y);

        if (isJumping == true && speedY == 0)
        {
            if (Physics2D.BoxCast(boxColl2D.bounds.center, boxColl2D.bounds.size, 0f, Vector2.down, groundOffset, groundLayerMask))
            {
                isJumping = false;
                characterAnimator.SetBool("IsGrounded", true);
            }
        }
    }
    private void Moving()
    {
        float moveX = Input.GetAxis("Horizontal");
        direction = new Vector2(moveX, 0).normalized;
        characterAnimator.SetInteger("Speed", (int)Mathf.Abs(direction.x));
        if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }
    private void Attacking()
    {
        if (Input.GetKey(KeyCode.F) && isAttacking == false && isJumping == false)
        {
            isAttacking = true;
            characterAnimator.SetTrigger("Attack");
        }
    }
    private void OnCompletedAttackedAnimation()
    {
        isAttacking = false;
    }
    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxColl2D.bounds.center, boxColl2D.bounds.size, 0f, Vector2.down , groundOffset, groundLayerMask);
        return raycastHit2D.collider!=null;
    }
    private AnimationClip FindAnimation(Animator animator, string name)
    {
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == name)
            {
                return clip;
            }
        }
        return null;
    }
}
