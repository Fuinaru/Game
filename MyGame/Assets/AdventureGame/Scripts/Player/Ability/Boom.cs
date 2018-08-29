using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : myGameObject
{

    public float radius = 3.0F;
    public float power = 1.1F;
    public int atk = 2;
    public float aliveTime = 5f;
 
    public float flickPower=5;
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
        if (aliveTime <= 0)
        {
            gameObject.GetComponent<Collider>().isTrigger = true;
            Color color = gameObject.GetComponent<Renderer>().material.color;
            gameObject.GetComponent<Renderer>().material.color = new Color(color.r+0.1f, color.g-0.2f, color.b-0.5f, color.a );
          
            d = transform.localScale;
            transform.localScale = new Vector3(d.x * power, d.y * power, d.z * power);

            CameraShakeEffect.shakeScreen();
            if (d.x >= radius) Destroy(gameObject);
        }

}
    private void OnTriggerEnter(Collider hit)
    {
       
        if (hit.tag == "Player")
        {
            //Vector3 dir = hit.transform.position - transform.position;
            //dir = new Vector3(dir.x, 0, dir.z).normalized;
            //dir *= 2;
            //dir.y = 0.5f;
           
            //hit.GetComponent<Rigidbody>().velocity = dir * flickPower;
            hit.GetComponent<Player>().Damage(atk);   
        }


        if (hit.tag == "Monster")
        {
            //Vector3 dir = hit.transform.position - transform.position;
            //dir = new Vector3(dir.x, 0, dir.z).normalized;
            //dir *= 2;
            //dir.y = 0.5f;
            //hit.GetComponent<Rigidbody>().velocity = dir.normalized * flickPower;
            hit.GetComponent<MonsterFoolAi>().Damage(atk);
        }
    }
}

             
 
  



