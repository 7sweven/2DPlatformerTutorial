using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // SerializeField is a tag which allows you to edit the value from the editor without making it public
    [SerializeField] private float jumpForce = 14;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    private bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private float groundCheckRadius = 0.1f;

    /* Start is a method which runs at the very first frame just once
     * 
     * We can initialize our variables here so they dont initialize every
     * single frame
     */ 
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    /* Update will run every single frame
     * 
     * We want to handle things like movement here since we want to check
     * if we are pressing down a button all the time.
     */
    private void Update()
    {
        #region Movement
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
        #endregion

        // calls the "UpdateAnimationState" method, and passes in dirX
        UpdateAnimationState(dirX);
        

    }

    private void UpdateAnimationState(float dirX)
    {
        // if moving to the right, activate running animation, reset x rotation
        if (dirX > 0f)
        {
            anim.SetBool("isRunning", true);
            sprite.flipX = false;
        }
        // if moving to the left, activate running animation, flip x rotation to face the correct way
        else if (dirX < 0f)
        {
            anim.SetBool("isRunning", true);
            sprite.flipX = true;
        }
        // if not moving, go back to idle state
        else
        {
            anim.SetBool("isRunning", false);
        }
            
    }

}
