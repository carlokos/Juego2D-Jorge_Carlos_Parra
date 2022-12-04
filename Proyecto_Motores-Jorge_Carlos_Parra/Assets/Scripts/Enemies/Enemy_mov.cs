using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_mov : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private bool isWalker;
    [SerializeField] private bool isPatrol;
    [SerializeField] private bool isFollowing;
    [SerializeField] private bool walkRight;
    private bool walldetected, pitdetected, isGrounded;

    [SerializeField] private Transform wallCheck, pitCheck, groundCheck;
    [SerializeField] private float detectionRadius;
    [SerializeField] private LayerMask Floor;

    [SerializeField] private Transform pointA, pointB;
    [SerializeField] private bool goToA, goToB;

    [SerializeField] private Transform player;
    [SerializeField] private bool canFollow;

    [SerializeField] private bool isGhost;



    public float Speed { get => speed; set => speed = value; }
    public bool CanFollow { get => canFollow; set => canFollow = value; }
    public bool IsFollowing { get => isFollowing; set => isFollowing = value; }
    public Rigidbody2D Rb { get => rb; set => rb = value; }

    private void Start()
    {
        if (isGhost)
        {
            Physics2D.IgnoreLayerCollision(3, 10, true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        pitdetected = !Physics2D.OverlapCircle(pitCheck.position, detectionRadius, Floor);
        walldetected = Physics2D.OverlapCircle(wallCheck.position, detectionRadius, Floor);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, detectionRadius, Floor);

        if ((pitdetected || walldetected) && isGrounded && !isFollowing && !isGhost)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        if (isWalker)
        {
            //animacion de moverse
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            if (!walkRight)
            {
                rb.velocity = new Vector2(-speed * Time.deltaTime, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
            }
        }

        if (isPatrol)
        {
            //animacion de moverse
            if (goToA)
            {
                rb.velocity = new Vector2(-speed * Time.deltaTime, rb.velocity.y);

                if(Vector2.Distance(transform.position, pointA.position) < 0.2f)
                {
                    Flip();
                    goToA = false;
                    goToB = true;
                }
            }

            if (goToB)
            {
                rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);

                if (Vector2.Distance(transform.position, pointA.position) < 0.2f)
                {
                    Flip();
                    goToA = true;
                    goToB = false;
                }
            }
        }

        if (isFollowing)
        {
            rb.transform.position = Vector2.MoveTowards(rb.transform.position, player.transform.position, speed * Time.deltaTime);
            if(rb.transform.position.x < player.transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    private void Flip()
    {
        walkRight = !walkRight;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }

    public void StartFollowing()
    {
        isWalker = false;
        isPatrol = false;
        isFollowing = true;
    }
}
