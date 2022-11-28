using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLifeSpan : MonoBehaviour
{
    [SerializeField] private float lifeSpan;
    private float lifetime;
    [SerializeField] private float damage;

    private void Start()
    {
        lifetime = lifeSpan;
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
        lifeSpan = lifetime;
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            endProjectile();
        }
    }
}
