using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatPlayer : MonoBehaviour
{
    private Player player;
    private float cd;
    private Animator animator;

    [Header ("Ataque")]
    [SerializeField] private Transform location;
    private bool canAttack;
    [SerializeField] private Vector2 boxsize;
    [SerializeField] private int damage;
    [SerializeField] private float cdtime;
    [SerializeField] private LayerMask enemyLayers;

    [Header("Proyectil")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private int numArrows;
    private List<GameObject> pool = new List<GameObject>(); 
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectilecdtime;
    private float projectilecd;

    [Header("Estadisticas_jugador")]
    [SerializeField] private int maxVida;
    private int vida;

    [Header("GameOver")]
    [SerializeField] private GameObject gameOverImg;



    public int Vida { get => vida; set => vida = value; }
    public int MaxVida { get => maxVida; set => maxVida = value; }
    public int NumArrows { get => numArrows; set => numArrows = value; }

    private void Start()
    {
        gameOverImg.SetActive(false);
        gameOverImg.GetComponent<CanvasGroup>().alpha = 0;
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
        vida = maxVida;
        canAttack = true; 
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

    private void Attack()
    {
        Collider2D[] foes = Physics2D.OverlapBoxAll(location.position, boxsize, 0, enemyLayers);
        animator.SetTrigger("Attack");
        foreach (Collider2D colisionador in foes)
        {
            colisionador.transform.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(location.position, boxsize);
    }

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

    public GameObject getProjectile()
    {
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
        canAttack = false;
        player.Speed = 0;
        player.Jump1 = false;
        yield return new WaitForSeconds(0.5f);
        player.Speed = 10f;
        canAttack = true;
    }

    public void TomarDaño(int damage, Vector2 posicion)
    {
        StartCoroutine(Invulnerabilidad());
        vida -= damage;
        animator.SetTrigger("Hurt");
        StartCoroutine(PerderelControl());
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
        yield return new WaitForSeconds(2f);
        Physics2D.IgnoreLayerCollision(7, 8, false);
        Physics2D.IgnoreLayerCollision(7, 9, false);
    }

    private void isDead()
    {
        Time.timeScale = 0;
        gameOverImg.SetActive(true);
        while (gameOverImg.GetComponent<CanvasGroup>().alpha < 1)
        {
            gameOverImg.GetComponent<CanvasGroup>().alpha += 0.005f;
        }
        //paramos música
    }
}
