using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
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
