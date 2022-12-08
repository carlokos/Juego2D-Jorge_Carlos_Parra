using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformTransform : MonoBehaviour
{
    /*
     * Script que contiene las plataformas que se mueven en el juego
     * convierte al jugador en hijo de las plataformas temporalmente
     * para que este se mueva con las plataformas
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
