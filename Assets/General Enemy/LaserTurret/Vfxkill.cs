using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vfxkill : MonoBehaviour
{

    [SerializeField]
    private float lifespan; //will die after this much
    void Start()
    {
        Invoke("Expire",lifespan);
    }

    void Expire()
    {
        Destroy(this.gameObject);
    }

}
