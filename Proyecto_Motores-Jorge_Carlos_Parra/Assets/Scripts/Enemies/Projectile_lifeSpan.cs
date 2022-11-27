using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_lifeSpan : MonoBehaviour
{
    [SerializeField] private float lifeSpan;
    [SerializeField] private float damage;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        lifeSpan -= Time.deltaTime;
        if (lifeSpan <= 0)
            endProjectile();
    }

    public void endProjectile()
    {
        anim.SetTrigger("Finish");
        Destroy(gameObject, .3f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            endProjectile();
            collision.gameObject.GetComponent<CombatPlayer>().TomarDaño(damage,
                collision.GetContact(0).normal);
        }
    }
}
