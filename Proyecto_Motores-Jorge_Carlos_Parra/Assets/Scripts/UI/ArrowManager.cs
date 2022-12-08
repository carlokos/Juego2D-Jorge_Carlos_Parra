using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArrowManager : MonoBehaviour
{
    //Script de la UI que indica cuantas flechas tiene el jugador
    [SerializeField] Image arrowImg;
    [SerializeField] TextMeshProUGUI numArrows;
    private CombatPlayer player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CombatPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.NumArrows <= 0)
        {
            arrowImg.enabled = false;
            numArrows.enabled = false;
        }
        else
        {
            arrowImg.enabled = true;
            numArrows.enabled = true;
            numArrows.text = "x " + player.NumArrows;
        }
    }
}
