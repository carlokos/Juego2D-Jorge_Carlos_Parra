using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_mov : MonoBehaviour
{
    /*
     * Script con la IA de movimiento de los enemigos, tiene informacion como su velocidad 
     * y elementos que los ayuda a no quedarse estancados en un mismo lugar o caerse al vacio
     * tiene varias opciones de movimiento que se distingue con bools
     */
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private bool isWalker;
    
    [SerializeField] private bool isFollowing;
    [SerializeField] private bool walkRight;
    private bool walldetected, pitdetected, isGrounded;

    [SerializeField] private Transform wallCheck, pitCheck, groundCheck;
    [SerializeField] private float detectionRadius;
    [SerializeField] private LayerMask Floor;

    [SerializeField] private Transform player;
    [SerializeField] private bool canFollow;

    [SerializeField] private bool isGhost;

    public float Speed { get => speed; set => speed = value; }
    public bool CanFollow { get => canFollow; set => canFollow = value; }
    public bool IsFollowing { get => isFollowing; set => isFollowing = value; }
    public Rigidbody2D Rb { get => rb; set => rb = value; }

    private void Start()
    {
        //los fantasmas pueden atravesar muros y el suelo
        if (isGhost)
        {
            Physics2D.IgnoreLayerCollision(3, 10, true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        /*
         * Hacemos que este atento a encontrar estas cosas:
         * -un precipicio
         * -un muro
         * y comprobamos en todo momento si esta en el suelo
         * Si encuentra algun precipicio o muro y esta en el suelo, ademas de no estar
         * siguiendo al jugador y no ser un fantasma, se dara la vuelta
         */
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
        //comprobamos en que direccion esta andando y modificamos su velocidad
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

        //movemos el objeto a una velocidad hacia el jugador, modificando su rotacion cuando sea necesario
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
        isFollowing = true;
    }
}
