using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    /*
     * Script que usamos para mover objetos, le pasamos un array de posiciones y con el index desde cero vamos
     * sumando para avanzar al siguiente, al llegar al maximo le restamos para que el objeto recorra los mismos puntos
     * pero al contrario
     */
    [SerializeField] private Transform[] positions;
    [SerializeField] private float speed;
    private int index;
    private int suma;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = positions[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position == positions[index].position)
        {
            index += suma;
        }

        if(index == positions.Length - 1)
        {
            suma = -1;
        }

        if(index == 0)
        {
            suma = 1;
        }

        transform.position = Vector3.MoveTowards(transform.position, positions[index].position, speed * Time.deltaTime);
    }
}
