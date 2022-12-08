using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_drop : MonoBehaviour
{
    //Script basico que controla los drops que puede recoger el jugador
    [SerializeField] private bool isArrow;
    [SerializeField] private bool isHeart;
    [SerializeField] private int stocks;
    [SerializeField] private AudioSource sound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isArrow && collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<CombatPlayer>().NumArrows += stocks;
            sound.Play();
            Destroy(gameObject, 0.2f);
        }

        if (isHeart && collision.gameObject.CompareTag("Player"))
        {    
            collision.GetComponent<CombatPlayer>().Vida += 1;
            sound.Play();
            Destroy(gameObject, 0.2f);
        }
    }

        
}
