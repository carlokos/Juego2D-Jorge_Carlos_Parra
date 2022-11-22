using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D player;
    private SpriteRenderer sprite;
    private Animator animator;
    public float speed;

    [Header("Salto")]
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private LayerMask isFloor;
    private bool onFloor;
    private bool jump;

    [Header("SaltoRegulable")]
    [Range(0, 1)] [SerializeField] private float multiplicadorCancelarSalto;
    [SerializeField] private float multiplicadorGravedad;
    private float escalaGravedad;
    private bool botonSaltoArriba = true;


    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        escalaGravedad = player.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Jump"))
        {
            jump = true;
        }

        if (onFloor)
        {
            animator.SetBool("Falling", false);
        }

        if (Input.GetButtonUp("Jump"))
        {
            BotonSaltoArriba();
            
        }

        if(player.velocity.x > 0)
        {
            sprite.flipX = false;
        }

        if(player.velocity.x < 0)
        {
            sprite.flipX = true;
        }
        animator.SetFloat("Speed", Mathf.Abs(player.velocity.x));
        onFloor = Physics2D.OverlapBox(controladorSuelo.position, boxSize, 0, isFloor);
        
    }

    private void FixedUpdate()
    {
        float InputX = Input.GetAxis("Horizontal");
        player.velocity = new Vector2(InputX * speed, player.velocity.y);

        
        if (jump && onFloor && botonSaltoArriba)
            Jump();

        if(player.velocity.y < 0 && !onFloor)
        {
            player.gravityScale = escalaGravedad * multiplicadorGravedad;
            fall();
        }
        else
            player.gravityScale = escalaGravedad;

        jump = false;
    }

    private void Jump()
    {
        player.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        animator.SetBool("Jumping", true);
        onFloor = false;
        jump = false;
        botonSaltoArriba = false;
    }

    private void BotonSaltoArriba()
    {
        if(player.velocity.y > 0)
        {
            player.AddForce(Vector2.down * player.velocity.y * (1 - multiplicadorCancelarSalto), ForceMode2D.Impulse);
            animator.SetBool("Jumping", true);
        }
        botonSaltoArriba = true;
        jump = false;
    }

    private void fall()
    {
        if(player.velocity.y < 0)
        {
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", true);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(controladorSuelo.position, boxSize);
    }

}
