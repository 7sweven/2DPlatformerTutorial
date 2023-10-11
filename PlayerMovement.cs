using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private float jumpForce = 14;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    private bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private float groundCheckRadius = 0.1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // get float with where joystick/button is (- for left, + for right)
        // raw gets it straight, not raw will gradually go to 0 when released
        float dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2 (dirX * 7f, rb.velocity.y);
        
        // draw circle at position with radius and sees if it overlaps with layer
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // can use vector2, adds jumpforce velocity to rigidbody
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0);
        }

        UpdateAnimationState(dirX);
        

    }

    private void UpdateAnimationState(float dirX)
    {
        if (dirX > 0f)
        {
            anim.SetBool("isRunning", true);
            sprite.flipX = false;
        } 
        else if (dirX < 0f)
        {
            anim.SetBool("isRunning", true);
            sprite.flipX = true;
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
            
    }

}
