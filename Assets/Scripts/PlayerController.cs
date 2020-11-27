using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool isTopDown = false;
    [Space]
    [SerializeField] private float acceleration = 0.1f;
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float jumpImpulse = 250.0f;
    [SerializeField] private float jumpPositionImpulse = 1.0f;
    [SerializeField] private float gravityFactor = 10.0f;
    [SerializeField] private LayerMask groundLayer;
    [Space]
    [SerializeField] private float xMoveToRotateFactor = 2.0f;
    [SerializeField] private float jumpSquash = 1.2f;
    [SerializeField] private float landingSquashFactor = 2.0f;
    [Space]
    public bool isGun = false;
    [SerializeField] private float shootingDelay = 0.01f;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float inaccuracyFactor = 4.0f;

    private bool isJumping = false;
    private bool isShooting = false;
    private float shootingDelayTimer = 0.0f;
    private Vector2 prevVelocity = Vector2.zero;
    private Camera cam;

    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }
    
    private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
        Vector2 diference = vec2 - vec1;
        float sign = (vec2.y < vec1.y)? -1.0f : 1.0f;
        return Vector2.Angle(Vector2.right, diference) * sign;
    }

    private bool isOnGround()
    {
        RaycastHit2D rHit = Physics2D.BoxCast(GetComponent<CapsuleCollider2D>().bounds.center,
            GetComponent<CapsuleCollider2D>().bounds.size, 0f, Vector2.down, 0.25f, groundLayer);
        return rHit.collider != null;
    }

    void Update()
    {
        if (!isTopDown)
        {
            if (isOnGround() && !isJumping && rb.velocity.y < 5.0f && rb.velocity.y > -5.0f)
            {
                isJumping = Input.GetButtonDown("Jump");
                if (isJumping) transform.localScale = new Vector2(jumpSquash - 1.0f, jumpSquash);
            }

            float ySc = prevVelocity.y < rb.velocity.y
                ? 1.0f - ((rb.velocity.y - prevVelocity.y) / landingSquashFactor)
                : 1.0f;
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(ySc, ySc), 0.05f);

            //transform.GetChild(0).rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * xMoveToRotateFactor);
        }

        if (transform.position.y < -50.0f) SceneManager.LoadScene("SideScroller");

        if (Input.GetMouseButton(0) && isGun)
        {
            if (shootingDelayTimer <= 0.0f)
            {
                isShooting = true;
                Instantiate(bullet, transform.GetChild(1).GetChild(1).position,
                    Quaternion.Euler(0.0f, 0.0f,
                        transform.GetChild(1).rotation.eulerAngles.z + UnityEngine.Random.Range(-inaccuracyFactor, inaccuracyFactor)))
                        .SetActive(true);
                shootingDelayTimer = shootingDelay;
            }
        }
        
        if (shootingDelayTimer <= shootingDelay / 2)
        {
            isShooting = false;
        }
        
        shootingDelayTimer -= Time.deltaTime;

        transform.GetChild(1).gameObject.SetActive(isGun);
        transform.GetChild(1).rotation = Quaternion.Euler(0.0f, 0.0f, AngleBetweenVector2(transform.position, cam.ScreenToWorldPoint(Input.mousePosition)));
        transform.GetChild(1).GetChild(1).gameObject.SetActive(isShooting);
    }
    
    void FixedUpdate()
    {
        Vector2 vel = rb.velocity;
        
        int xMove = Mathf.FloorToInt(Input.GetAxisRaw("Horizontal"));
        int yMove = 0;
        
        if(isTopDown) yMove = Mathf.FloorToInt(Input.GetAxisRaw("Vertical"));

        vel.x += xMove * acceleration * Time.fixedDeltaTime;
        vel.y += yMove * acceleration * Time.fixedDeltaTime;

        if (vel.x > speed) vel.x = speed;
        else if (xMove == 0) vel.x /= 1.2f;

        if (!isTopDown)
        {
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
        }
        else
        {
            if (vel.y > speed) vel.y = speed;
            else if (yMove == 0) vel.y /= 1.2f;
        }

        rb.velocity = vel;
        prevVelocity = vel;
    }
}
