using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float vida;
    private Animator animator;
    private SpriteRenderer sprite;
    private Collider2D enemy;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        enemy = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        vida -= damage;
        sprite.color = Color.red;
        StartCoroutine(Recovering());
        if(vida <= 0)
        {
            Muerte();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().Animator.SetTrigger("Hurt");
            collision.GetComponent<Player>().Damage1 = true;

            if(transform.position.x > collision.transform.position.x)
            {
                collision.GetComponent<Player>().Knockback = -3;
                collision.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    private IEnumerator Recovering()
    {
        enemy.enabled = false;
        yield return new WaitForSeconds(0.7f);
        Debug.Log("Ha entrado");
    }

    private void Muerte()
    {
        animator.SetTrigger("Muerte");
        Destroy(gameObject, .5f);
    }
}
