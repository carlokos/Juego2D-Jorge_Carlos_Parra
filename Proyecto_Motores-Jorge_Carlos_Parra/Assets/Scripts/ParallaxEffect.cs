using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private Transform camara;
    public float efectoParallax;
    private Vector3 camaraUltPos;
    // Start is called before the first frame update
    void Start()
    {
        camara = Camera.main.transform;
        camaraUltPos = camara.position;
    }

    //Función se carga cuando la demas funciones ya han terminado, se usa mucho con la camara
    private void LateUpdate()
    {
        Vector3 movimientoFondo = camara.position - camaraUltPos;
        transform.position += new Vector3(movimientoFondo.x * efectoParallax, movimientoFondo.y, 0);
        camaraUltPos = camara.position;
    }
}
