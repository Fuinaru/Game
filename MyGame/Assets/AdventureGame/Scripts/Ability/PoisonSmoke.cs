using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonSmoke : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnParticleCollision(GameObject other)
    {
        try
        {
            other.GetComponent<HPObject>().GetPosioned(1, 10);
        }
        catch { }
    }




}
