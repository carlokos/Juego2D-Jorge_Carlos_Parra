using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_lifeSpan : MonoBehaviour
{
    [SerializeField] private float lifeSpan;
    private float lifetime;
    [SerializeField] private int damage;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        lifetime = lifeSpan;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        lifeSpan -= Time.deltaTime;
        if (lifeSpan <= 0)
            StartCoroutine(endProjectile());
    }

    private IEnumerator endProjectile()
    {
        anim.SetTrigger("Finish");
        yield return new WaitForSeconds(0.3f);
        lifeSpan = lifetime;
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(endProjectile());
            collision.gameObject.GetComponent<CombatPlayer>().TomarDaño(damage,
                collision.GetContact(0).normal);
        }
    }
}
