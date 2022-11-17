using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraManager : MonoBehaviour
{
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
      TargetPos = new Vector3(target.transform.position.x, target.transform.position.y - 2, transform.position.z);

      if(!target.flipX){
            TargetPos = new Vector3(TargetPos.x + HaciaAdelante, TargetPos.y, transform.position.z);
      }

      if (target.flipX)
      {
            TargetPos = new Vector3(TargetPos.x - HaciaAdelante, TargetPos.y, transform.position.z);
      }

      transform.position = Vector3.Lerp(transform.position, TargetPos, smoothing * Time.deltaTime);
    }
}