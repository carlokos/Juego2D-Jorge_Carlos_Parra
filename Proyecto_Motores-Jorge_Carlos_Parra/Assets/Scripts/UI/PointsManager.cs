using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    //Script que se encarga de mostrar los puntos del jugador
    [SerializeField] TextMeshProUGUI lblPoints;
    private int points;
    // Start is called before the first frame update
    void Start()
    {
        points = 0;
    }

    // Update is called once per frame
    void Update()
    {
        lblPoints.text = points.ToString();
    }

    public void addPoints(int getPoints)
    {
        points += getPoints;
    }
}
