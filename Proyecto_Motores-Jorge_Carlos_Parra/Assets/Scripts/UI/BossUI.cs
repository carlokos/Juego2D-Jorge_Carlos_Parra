using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    /*
     * Script del boss que contiene su barra de vida, ademas
     * se actualiza con su vida actual mediante una funcion en el update
     */
    [SerializeField] private Image bossHealthBar;
    private int bossmaxHealth;
    private Enemy boss;

    private void Start()
    {
        boss = GetComponentInParent<Enemy>();
        bossmaxHealth = boss.Vida;
    }

    private void Update()
    {
        bossHealthBar.fillAmount = (float) boss.Vida / bossmaxHealth;
    }
}
