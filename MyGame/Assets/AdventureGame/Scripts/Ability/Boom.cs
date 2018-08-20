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
    Collider[] colliders;

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
            gameObject.GetComponent<Collider>().isTrigger = true;
            Color color = gameObject.GetComponent<Renderer>().material.color;
            gameObject.GetComponent<Renderer>().material.color = new Color(color.r+0.1f, color.g-0.2f, color.b-0.5f, color.a );
          
            d = transform.localScale;
            transform.localScale = new Vector3(d.x * power, d.y * power, d.z * power);

            if (d.x >= radius) Destroy(gameObject);
        }

}
    private void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "Player")
        { hit.GetComponent<Player>().Damage(2);
            hit.GetComponent<Player>().getHurted();
        }

      
        if (hit.tag == "Monster" || hit.tag == "Others")
            hit.GetComponent<OtherHp>().HpChange(-2);
    }
}

             
 
  



