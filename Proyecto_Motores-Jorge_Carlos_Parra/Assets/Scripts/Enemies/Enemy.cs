using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int vida;
    [SerializeField] private int damage;
    [SerializeField] private int points;
    private Animator animator;
    private SpriteRenderer sprite;
    private Enemy_mov mov;
    private GameObject UI;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        mov = GetComponent<Enemy_mov>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        yield return new WaitForSeconds(0.5f);
        sprite.color = Color.white;
    }

    private void Muerte()
    {
        UI = GameObject.Find("UI");
        UI.GetComponent<PointsManager>().addPoints(points);
        mov.Speed = 0;
        sprite.color = Color.white;
        animator.SetTrigger("Muerte");
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
}
