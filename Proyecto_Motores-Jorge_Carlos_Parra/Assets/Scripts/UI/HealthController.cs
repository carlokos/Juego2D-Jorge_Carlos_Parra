using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    /*
     * Script que controla la vida del jugador en la UI, es un sistema de corazones
     * asi que le pasamos las imagenes y una array de los corazones, vamos rellenando
     * esta array con las imagenes dependiendo de la vida del jugador
     */
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    [SerializeField] private Image[] hearts;
    private CombatPlayer player;

    private void Start()
    {
        player = GetComponent<CombatPlayer>();
    }

    private void Update()
    {
        if(player.Vida > player.MaxVida)
        {
            player.Vida = player.MaxVida;
        }

        for(int i = 0; i < hearts.Length; i++)
        {
            if(i < player.Vida)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if(i < player.MaxVida)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
