using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D collide;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0;
    [SerializeField] private float moveSpeed = 7;
    [SerializeField] private float jump = 14;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collide= GetComponent<BoxCollider2D>();
        sprite= GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed,rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
        }

        AnimationUpdate();
    }
    private void AnimationUpdate()
    {
        if (dirX > 0)
        {
            anim.SetBool("running", true);
            sprite.flipX = false;
        }
        else if (dirX < 0)
        {
            anim.SetBool("running", true);
            sprite.flipX= true;
        }
        else
        {
            anim.SetBool("running", false);
        }
    }
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(collide.bounds.center, collide.bounds.size, 0, Vector2.down, .1f, jumpableGround);
    }
}
