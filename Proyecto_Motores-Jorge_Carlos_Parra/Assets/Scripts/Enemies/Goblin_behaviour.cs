using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_behaviour : MonoBehaviour
{
    [SerializeField] private Transform hitbox;
    [SerializeField] private int damage;
    private Enemy_mov mov;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        mov = GetComponent<Enemy_mov>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mov.IsFollowing)
        {
            anim.SetBool("Running", true);
        }
    }
}
