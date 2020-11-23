using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float acceleration;
    [SerializeField] private Transform[] patrolPoints;
    private int patrolIndex = 0;
    private Camera cam;

    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }
    
    void Update()
    {
        
    }
    
    void FixedUpdate()
    {
        int xMove = 0, yMove = 0;
        Vector2 vel = rb.velocity;
        if (Mathf.Abs(patrolPoints[patrolIndex].position.x - transform.position.x) > 2.0f)
        {
            xMove = patrolPoints[patrolIndex].position.x > transform.position.x ? 1 : -1;
        }
        else
        {
            patrolIndex++;
            if (patrolIndex >= patrolPoints.Length) patrolIndex = 0;
        }

        vel.x += xMove * acceleration * Time.fixedDeltaTime;
        vel.y += yMove * acceleration * Time.fixedDeltaTime;

        if (vel.x > speed) vel.x = speed;
        else if (xMove == 0) vel.x /= 1.2f;

        rb.velocity = vel;
    }
}
