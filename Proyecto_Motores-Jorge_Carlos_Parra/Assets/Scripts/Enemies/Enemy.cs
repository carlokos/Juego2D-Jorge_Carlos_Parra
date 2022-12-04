using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int vida;
    [SerializeField] private int damage;
    [SerializeField] private int points;
    [SerializeField] private Transform hitbox;
    [SerializeField] private Transform range;
    [SerializeField] private GameObject drop;
    [SerializeField] private bool canDrop;
    [SerializeField] private AudioSource enemyDead;
    private Animator animator;
    private bool isAttacking;
    private SpriteRenderer sprite;
    private Enemy_mov mov;
    private GameObject UI;

    public bool IsAttacking { get => isAttacking; set => isAttacking = value; }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        mov = GetComponent<Enemy_mov>();
    }

    public void TakeDamage(int damage)
    {
        vida -= damage;
        sprite.color = Color.red;
        StartCoroutine(Recovering());
        if(vida <= 0)
        {
            Muerte();
        }
    }

    private IEnumerator Recovering()
    {
        yield return new WaitForSeconds(0.2f);
        sprite.color = Color.white;
    }

    private void Muerte()
    {
        enemyDead.Play();
        gameObject.GetComponent<Collider2D>().enabled = false;
        UI = GameObject.Find("UI");
        UI.GetComponent<PointsManager>().addPoints(points);
        mov.Speed = 0;
        sprite.color = Color.white;
        animator.SetTrigger("Muerte");
        if (canDrop)
        {
            GameObject item = drop;
            item.transform.position = gameObject.transform.position;
            Instantiate(item);
        }
        Destroy(gameObject, .5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<CombatPlayer>().TomarDaño(damage, 
                collision.GetContact(0).normal);
        }
    }

    public void final_Ani()
    {
        animator.SetBool("Attaking", false);
        isAttacking = false;
        range.GetComponent<Collider2D>().enabled = true;
        mov.Rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void ColliderWeaponTrue()
    {
        hitbox.GetComponent<Collider2D>().enabled = true;
    }

    public void ColliderWeaponFalse()
    {
        hitbox.GetComponent<Collider2D>().enabled = false;
    }
}
