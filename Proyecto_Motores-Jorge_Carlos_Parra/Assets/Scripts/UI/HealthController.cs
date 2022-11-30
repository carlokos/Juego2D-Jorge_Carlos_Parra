using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
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
