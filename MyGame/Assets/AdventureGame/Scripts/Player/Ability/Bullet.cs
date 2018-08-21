using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
public float aliveTime=5f;
    // Use this for initialization
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update () {
        aliveTime -= Time.deltaTime;
        if (aliveTime >= 0)
        {
            Color color = gameObject.GetComponent<Renderer>().material.color;
            gameObject.GetComponent<Renderer>().material.color = new Color(color.r, color.g, color.b, aliveTime);

        }
        else Destroy(gameObject);
        //else gameObject.GetComponent<Rigidbody>().AddExplosionForce(100, transform.position,100);

    }
}
