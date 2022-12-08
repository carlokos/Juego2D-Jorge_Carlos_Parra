using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraManager : MonoBehaviour
{
    /*
     * Script para el seguimiento de la camara, seguira al jugador y se adelantara un poco para que pueda ver
     * mejor lo que tiene delante, al cambiar de direccion se adaptara con un poquito de retraso para que 
     * quede mejor
     */
    public SpriteRenderer target;
    private Vector3 TargetPos;

    public float HaciaAdelante;
    public float smoothing;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      TargetPos = new Vector3(target.transform.position.x, target.transform.position.y -2, transform.position.z);

      if(target.transform.localScale.x == 1){
            TargetPos = new Vector3(TargetPos.x + HaciaAdelante, TargetPos.y, transform.position.z);
      }

      if (target.transform.localScale.x == -1)
      {
            TargetPos = new Vector3(TargetPos.x - HaciaAdelante, TargetPos.y, transform.position.z);
      }

      transform.position = Vector3.Lerp(transform.position, TargetPos, smoothing * Time.deltaTime);
    }
}
