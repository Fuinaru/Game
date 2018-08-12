using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public float radius = 3.0F;
    public float power = 1.1F;
    public float aliveTime = 5f;
    public Vector3 pos;
    // Use this for initialization
    //Vector3 explosionPos;
    //Collider[] colliders;

    Vector3 d;
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        //explosionPos = transform.position;
        //colliders = Physics.OverlapSphere(explosionPos, radius);
        aliveTime -= Time.deltaTime;
        pos = transform.position;
        if (aliveTime <= 0)
        {
            transform.position = pos;
            Color color = gameObject.GetComponent<Renderer>().material.color;
            gameObject.GetComponent<Renderer>().material.color = new Color(color.r, color.g, color.b, color.a * 0.5f);
            //foreach (Collider hit in colliders)
            //{
            //    Rigidbody rb = hit.GetComponent<Rigidbody>();

            //    if (rb != null)
            //    {
            //        //rb.AddExplosionForce(power, explosionPos, radius, power,ForceMode.Impulse);

            //        d = rb.transform.position - explosionPos;

            //        rb.AddForce(new Vector3(d.x,d.y,d.z).normalized* power*Mathf.Max(0,radius-d.magnitude), ForceMode.Impulse);
            //    }
            //        }

            d = transform.localScale;
            transform.localScale = new Vector3(d.x * power, d.y * power, d.z * power);

            if (d.x >= radius) Destroy(gameObject);
        }

    }
}   
  



