using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using Color = UnityEngine.Color;

public class Player : MonoBehaviour
{
    private Rigidbody2D player;
    private Animator animator;
    private float speed;

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
    private bool botonSaltoPulsado = true;

    [Header("Movimiento")]
    private bool sePuedeMover = true;
    [SerializeField] private Vector2 knockback;

    public float Speed { get => speed; set => speed = value; }
    public bool Jump1 { get => jump; set => jump = value; }
    public Animator Animator { get => animator; set => animator = value; }
    public bool SePuedeMover { get => sePuedeMover; set => sePuedeMover = value; }

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        escalaGravedad = player.gravityScale;
        speed = 10;
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
            BotonSaltoPulsado();
        }
        
        animator.SetFloat("Speed", Mathf.Abs(player.velocity.x));
        onFloor = Physics2D.OverlapBox(controladorSuelo.position, boxSize, 0, isFloor);
    }

    private void FixedUpdate()
    {
        if (sePuedeMover)
        {
            float InputX = Input.GetAxis("Horizontal");
            player.velocity = new Vector2(InputX * speed, player.velocity.y);
            if (InputX > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (InputX < 0)
            {
                transform.localScale = new Vector3(-1f, 1, 1);
            }
        }

        if (jump && onFloor && botonSaltoPulsado)
             Jump();

        if (player.velocity.y < 0 && !onFloor)
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
        botonSaltoPulsado = false;
    }

    public void Knockback(Vector2 puntoGolpe)
    {
        player.velocity = new Vector2(-knockback.x * puntoGolpe.x, knockback.y);
    }


    private void BotonSaltoPulsado()
    {
        if(player.velocity.y > 0)
        {
            player.AddForce(Vector2.down * player.velocity.y * (1 - multiplicadorCancelarSalto), ForceMode2D.Impulse);
            animator.SetBool("Jumping", true);
        }
        botonSaltoPulsado = true;
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
