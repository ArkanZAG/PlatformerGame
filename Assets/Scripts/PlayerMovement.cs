    using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Jump()
    {
        if (GetIsGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("Jump");
        }else if (GetOnWall() && !GetIsGrounded())
        {
            if (horizontalInput == 0)
            {
                body.velocity = new Vector2(-transform.localScale.x * 10, 0);
                transform.localScale =
                    new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                body.velocity = new Vector2(-transform.localScale.x * 3, 6);
            }
            wallJumpCooldown = 0;
        }
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        
        if (horizontalInput>0.01f)
        {
            transform.localScale = Vector3.one;
        }else if (horizontalInput<-0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (wallJumpCooldown > 0.2f)
        {
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (GetOnWall() && !GetIsGrounded())
            {
                body.gravityScale = 1;
                body.velocity = Vector2.zero;
            }
            else
            {
                body.gravityScale = 7;
            }
            
            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {
            wallJumpCooldown += Time.deltaTime;
        }

        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("Grounded", GetIsGrounded());
    }

    private bool GetIsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0,
            new Vector2(0, -1), 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    
    private bool GetOnWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0,
            new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
    
    
    
    
    
    
        
    // private void Jump()
    // {
    //     if (GetIsGrounded())
    //     {
    //         body.velocity = new Vector2(body.velocity.x, jumpPower);
    //         anim.SetTrigger("Jump");
    //         return;
    //     }
    //
    //     if (!GetOnWall() || GetIsGrounded()) return;
    //
    //     bool hasNoInput = horizontalInput == 0;
    //     var power = hasNoInput ? 10 : 3;
    //     body.velocity = new Vector2(-transform.localScale.x * power, 6);
    //         
    //     if (hasNoInput)
    //         transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    //         
    //     wallJumpCooldown = 0;
    // }
    
    
}
