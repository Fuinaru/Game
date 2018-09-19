using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MyGameObject
{

    public float radius = 3.0F;
    public float boomSpeed = 1.1F;
    public int atk = 2;
    public float aliveTime = 5f;
    private bool isHitPlayer = false;
    private Vector3 originScale;
    private ColorChange changeColor=new ColorChange();
    public bool isStart = false;
    public bool freezePos = false;
    // Use this for initialization
    //Vector3 explosionPos;
    Collider[] colliders;

    Vector3 d;
    void Start()
    {

        base.Start();
        originScale = transform.localScale;
        if (freezePos) m_rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.IsTimePause()) { m_rigidbody.velocity = Vector3.zero; return; }
        //explosionPos = transform.position;
        //colliders = Physics.OverlapSphere(explosionPos, radius);
        if (!isStart) return;
       aliveTime -= Time.deltaTime;
        if (aliveTime <= 0)
        {
            if (!gameObject.GetComponent<Collider>().isTrigger)
            {
                Tools.PlayFollowingParticletByName("BoomEffect", transform);
                Tools.PlaySoundByName("boom", transform);
                gameObject.GetComponent<Collider>().isTrigger = true;
                gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            }



            d = transform.localScale;
            transform.localScale = new Vector3(d.x * boomSpeed, d.y * boomSpeed, d.z * boomSpeed);

            CameraShakeEffect.shakeScreen(0.5f,0.5f);
            if (d.x >= radius) Destroy(gameObject);
        }
        else if(aliveTime<1.5f)
        {
            d = Random.Range(0.8f, 1.2f) * originScale;
            transform.localScale = new Vector3(d.x , d.y , d.z);
            changeColor.ColorA2BCir(gameObject.GetComponent<Renderer>().material,new Color(0.8f,0,0,0.7f) , Color.black, 10);
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
            hit.GetComponent<HPObject>().Damage(atk,"Boom");
        }
    }
}

             
 
  



