using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float acceleration = 0.1f;
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float jumpImpulse = 250.0f;
    [SerializeField] private float jumpPositionImpulse = 1.0f;
    [SerializeField] private float gravityFactor = 10.0f;
    [Space]
    [SerializeField] private float xMoveToRotateFactor = 2.0f;
    [SerializeField] private float jumpSquash = 1.2f;
    [SerializeField] private float landingSquashFactor = 2.0f;

    private bool isJumping = false;
    private Vector2 prevVelocity = Vector2.zero;

    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isJumping && rb.velocity.y < 5.0f && rb.velocity.y > -5.0f)
        {
            isJumping = Input.GetButtonDown("Jump");
            if(isJumping) transform.localScale = new Vector2(jumpSquash - 1.0f, jumpSquash);
        }

        float ySc = prevVelocity.y < rb.velocity.y
            ? 1.0f - ((rb.velocity.y - prevVelocity.y) / landingSquashFactor)
            : 1.0f;
        transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(0.4f + ySc, ySc), 0.05f);

        transform.GetChild(0).rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x*xMoveToRotateFactor);

        if (transform.position.y < -50.0f) SceneManager.LoadScene("SideScroller");
    }
    
    void FixedUpdate()
    {
        Vector2 vel = rb.velocity;
        
        int xMove = Mathf.FloorToInt(Input.GetAxisRaw("Horizontal"));
        int yMove = Mathf.FloorToInt(Input.GetAxisRaw("Vertical"));

        vel.x += xMove * acceleration * Time.fixedDeltaTime;
        vel.y += yMove * acceleration * Time.fixedDeltaTime;

        if (vel.x > speed) vel.x = speed;
        else if (xMove == 0) vel.x /= 1.2f;

        if (isJumping)
        {
            vel.y = jumpImpulse;
            rb.position += new Vector2(0.0f, jumpPositionImpulse);
            isJumping = false;
        }
        else
        {
            vel.y -= gravityFactor;
        }

        rb.velocity = vel;
        prevVelocity = vel;
    }
}
