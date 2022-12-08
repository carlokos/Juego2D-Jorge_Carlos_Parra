using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magicHand : MonoBehaviour
{
    /*
     * Script de un movimiento del boss, una mano que persigue al jugador
     * las funciones son publicas ya que la llamamos desde el script del Boss
     */
    [SerializeField] private float speed;
    [SerializeField] private Player pj;
    private Animator anim;

    public float Speed { get => speed; set => speed = value; }

    void Start()
    {
        anim = GetComponent<Animator>();
        transform.position = new Vector2(pj.transform.position.x, pj.transform.position.y);
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(pj.transform.position.x, pj.transform.position.y + 2),
                speed * Time.deltaTime);
    }
    public IEnumerator endProjectile()
    {
        speed = 0;
        anim.SetTrigger("Finish");
        yield return new WaitForSeconds(0.7f);
        desactive();
    }

    public void activate()
    {
        gameObject.SetActive(true);
    }

    public void desactive()
    {
        gameObject.SetActive(false);
    }
}
