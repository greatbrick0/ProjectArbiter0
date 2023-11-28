using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator player;
    private float a;
    public bool walking;
    void Start()
    {
        player = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (walking)
        {
            player.SetBool("walking", true);
            
            //Gradual weight increase
            //if (a <= 1)
              //  a = a + 0.05f;

            player.SetLayerWeight(player.GetLayerIndex("IdleWeight"), 0f);
        }
        else
        {
            player.SetBool("walking", false);
            player.SetLayerWeight(player.GetLayerIndex("IdleWeight"), 1f);
            //a = 0f;
        }
    }
}
