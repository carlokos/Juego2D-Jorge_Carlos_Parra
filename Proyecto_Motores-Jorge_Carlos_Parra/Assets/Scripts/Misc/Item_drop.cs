using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_drop : MonoBehaviour
{
    [SerializeField] private bool isArrow;
    [SerializeField] private bool isHeart;
    [SerializeField] private int stocks;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isArrow && collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<CombatPlayer>().NumArrows += stocks;
            Destroy(gameObject);
        }

        if (isHeart && collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<CombatPlayer>().Vida += 1;
            Destroy(gameObject);
        }
    }
}
