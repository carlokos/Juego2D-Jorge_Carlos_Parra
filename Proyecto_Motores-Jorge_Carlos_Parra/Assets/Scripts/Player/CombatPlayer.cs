using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatPlayer : MonoBehaviour
{
    //script de combate del jugador, tambien controla los sonidos y el gameover
    private Player player;
    private float cd;
    private bool isTalking;
    private bool dead;
    private Animator animator;

    //variables del ataque, como su tamaño, tiempo y a quienes afecta
    [Header ("Ataque")]
    [SerializeField] private Transform location;
    private bool canAttack;
    [SerializeField] private Vector2 boxsize;
    [SerializeField] private int damage;
    [SerializeField] private float cdtime;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private LayerMask ghostLayers;

    //variables del proyectil del jugador, como cuantos tiene,  un sistema de pool y el tiempo de espera
    [Header("Proyectil")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private int numArrows;
    private List<GameObject> pool = new List<GameObject>(); 
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectilecdtime;
    private float projectilecd;

    //estadisticas del jugador
    [Header("Estadisticas_jugador")]
    [SerializeField] private int maxVida;
    private int vida;

    [Header("GameOver")]
    [SerializeField] private GameObject gameOverImg;
    [SerializeField] private GameObject[] cancelEnable;

    //los sonidos del jugador
    [Header("Sounds")]
    [SerializeField] private AudioSource arrowSound;
    [SerializeField] private AudioSource swordSound;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private AudioSource hitSound;
    [SerializeField] private BGMusic_manager AudioManager;
 
    public int Vida { get => vida; set => vida = value; }
    public int MaxVida { get => maxVida; set => maxVida = value; }
    public int NumArrows { get => numArrows; set => numArrows = value; }
    public bool CanAttack { get => canAttack; set => canAttack = value; }
    public bool IsTalking { get => isTalking; set => isTalking = value; }
    public bool Dead { get => dead; set => dead = value; }

    private void Start()
    {
        isTalking = false;
        gameOverImg.SetActive(false);
        gameOverImg.GetComponent<CanvasGroup>().alpha = 1;
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
        vida = maxVida;
        canAttack = true;
        dead = false;
        //es importante que nos aseguremos que este las capas activas
        Physics2D.IgnoreLayerCollision(7, 8, false);
        Physics2D.IgnoreLayerCollision(7, 9, false);
    }

    // Update is called once per frame
    void Update()
    {
        if(cd > 0)
        {
            cd -= Time.deltaTime;
        }
        if(projectilecd > 0)
        {
            projectilecd -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Fire1") && cd <= 0 && canAttack) 
        {
            StartCoroutine(Attacking());
            Attack();
            cd = cdtime;
        }
        if (Input.GetButtonDown("Fire2") && projectilecd <= 0 && numArrows > 0 && canAttack)
        {
            ShootArrow();
            projectilecd = projectilecdtime;
        }
    }

    //miramos si lo que hay en la caja es enemigo y llamamos a su funcion de daño,
    //lo comprobamos mediante un layer
    private void Attack()
    {
        Collider2D[] foes = Physics2D.OverlapBoxAll(location.position, boxsize, 0, enemyLayers);
        Collider2D[] ghost = Physics2D.OverlapBoxAll(location.position, boxsize, 0, ghostLayers);
        animator.SetTrigger("Attack");
        foreach (Collider2D colisionador in foes)
        {
            colisionador.transform.GetComponent<Enemy>().TakeDamage(damage);
        }

        foreach (Collider2D colisionador in ghost)
        {
            colisionador.transform.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(location.position, boxsize);
    }

    //empieza en la posicion del jugador y va avanzando segun la direccion donde el jugador este mirando
    private void ShootArrow()
    {
        GameObject arrow = getProjectile();
        numArrows--;
        arrow.transform.position = location.position;
        arrow.transform.rotation = Quaternion.Euler(location.rotation.x, location.rotation.y, -45);
        
        if (transform.localScale.x < 0)
        {
            arrow.transform.localScale = -arrow.transform.localScale;
            arrow.GetComponent<Rigidbody2D>().AddForce(new Vector2(-projectileSpeed, 0f), ForceMode2D.Force);
        }
        else
        {
            arrow.GetComponent<Rigidbody2D>().AddForce(new Vector2(projectileSpeed, 0f), ForceMode2D.Force);
        }
    }

    /*
     * Sistema de reciclaje, comprueba si ya existe el objeto en la herarquia, si no existe crea uno
     * si existe lo vuelve activo
     */
    public GameObject getProjectile()
    {
        arrowSound.Play();
        for(int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);
                return pool[i];
            }
        }
        GameObject arrow = Instantiate(projectile, location.position, Quaternion.identity) as GameObject;
        pool.Add(arrow);
        return arrow;
    }

    private IEnumerator Attacking()
    {
        swordSound.Play();
        canAttack = false;
        player.Speed = 0;
        player.Jump1 = false;
        yield return new WaitForSeconds(0.4f);
        player.Speed = 10f;
        canAttack = true;
    }

    public void TomarDaño(int damage, Vector2 posicion)
    {
        hitSound.Play();
        StartCoroutine(Invulnerabilidad());
        StartCoroutine(PerderelControl());
        vida -= damage;
        animator.SetTrigger("Hurt");
        player.Knockback(posicion);
        if(vida <= 0)
        {
            isDead();
        }
    }

    private IEnumerator PerderelControl()
    {
        player.SePuedeMover = false;
        canAttack = false;
        yield return new WaitForSeconds(0.8f);
        player.SePuedeMover = true;
        canAttack = true;
    }

    private IEnumerator Invulnerabilidad()
    {
        Physics2D.IgnoreLayerCollision(7, 8, true);
        Physics2D.IgnoreLayerCollision(7, 9, true);
        Physics2D.IgnoreLayerCollision(7, 10, true);
        yield return new WaitForSeconds(1.5f);
        Physics2D.IgnoreLayerCollision(7, 8, false);
        Physics2D.IgnoreLayerCollision(7, 9, false);
        Physics2D.IgnoreLayerCollision(7, 10, false);
    }

    //llamamos al menu de gameover
    private void isDead()
    {
        deathSound.Play();
        Time.timeScale = 0;
        dead = true;
        if(cancelEnable.Length != null)
        {
            for(int i = 0; i < cancelEnable.Length; i++)
            {
                cancelEnable[i].SetActive(false);
            }
        }
        gameOverImg.SetActive(true);
        AudioManager.GameOver();
    }
}
