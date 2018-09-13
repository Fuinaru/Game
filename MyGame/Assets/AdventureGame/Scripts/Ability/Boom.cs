using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MyGameObject
{

    public float radius = 3.0F;
    public float power = 1.1F;
    public int atk = 2;
    public float aliveTime = 5f;
    private bool isHitPlayer = false;
    public float flickPower=5;
    // Use this for initialization
    //Vector3 explosionPos;
    Collider[] colliders;

    Vector3 d;
    void Start()
    {

        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.IsTimePause()) { m_rigidbody.velocity = Vector3.zero; return; }
        //explosionPos = transform.position;
        //colliders = Physics.OverlapSphere(explosionPos, radius);
        aliveTime -= Time.deltaTime;
        if (aliveTime <= 0)
        {
            if (!gameObject.GetComponent<Collider>().isTrigger) {
                Tools.PlayFollowingParticletByName("BoomEffect", transform);
                gameObject.GetComponent<Collider>().isTrigger = true;
                gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            }
         
          
          
            d = transform.localScale;
            transform.localScale = new Vector3(d.x * power, d.y * power, d.z * power);

            CameraShakeEffect.shakeScreen();
            if (d.x >= radius) Destroy(gameObject);
        }

}
    private void OnTriggerEnter(Collider hit)
    {
       
        if (hit.tag == "Player"&&!isHitPlayer)
        {
            //Vector3 dir = hit.transform.position - transform.position;
            //dir = new Vector3(dir.x, 0, dir.z).normalized;
            //dir *= 2;
            //dir.y = 0.5f;

            //hit.GetComponent<Rigidbody>().velocity = dir * flickPower;
            //   GameManager.player.transform.LookAt(gameObject.transform)
            GameManager.player.DamageWithAni(atk,transform);
          hit.GetComponent<HPObject>().Damage(atk);
            isHitPlayer = true;
           
        }


        if (hit.tag == "Monster")
        {
            //Vector3 dir = hit.transform.position - transform.position;
            //dir = new Vector3(dir.x, 0, dir.z).normalized;
            //dir *= 2;
            //dir.y = 0.5f;
            //hit.GetComponent<Rigidbody>().velocity = dir.normalized * flickPower;
            hit.GetComponent<HPObject>().Damage(atk);
        }
    }
}

             
 
  



