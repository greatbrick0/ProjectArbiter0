using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    Animator enemy;
    private float a;
    public bool walking;
    void Start()
    {
        enemy = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (walking)
        {
            enemy.SetBool("walking", true);

            //Gradual weight increase
            //if (a <= 1)
            //  a = a + 0.05f;
        }
        else
        {
            enemy.SetBool("walking", false);
            //a = 0f;
        }
    }
}
