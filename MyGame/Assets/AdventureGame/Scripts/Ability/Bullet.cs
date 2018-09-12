using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MyGameObject
{
public float aliveTime=5f;
    public int atk = 1;
    private Vector3 scale;
    private Vector3 pos;
    private bool isChild = false;
    private Vector3 s = new Vector3();

    // Use this for initialization
    private void Start()
    {
        base.Start();
        s = Vector3.zero;
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update () {
        if (GameManager.IsTimePause()) { s = m_rigidbody.velocity; m_rigidbody.velocity = Vector3.zero; return; }
        else if (s != Vector3.zero) { m_rigidbody.velocity = s;s = Vector3.zero; }
        aliveTime -= Time.deltaTime;
        if (aliveTime <= 0) Destroy(gameObject);
        //else gameObject.GetComponent<Rigidbody>().AddExplosionForce(100, transform.position,100);

    }
    private void OnCollisionEnter(Collision collision)
    {
        Transform trans = collision.gameObject.transform;
        if (trans.tag == "Monster" || trans.tag == "Others" || trans.tag == "Player")
            trans.GetComponent<HPObject>().Damage(atk);
        Destroy(gameObject);
      
        //Destroy(gameObject.GetComponent<Collider>());
        //Destroy(m_rigidbody);
        //if (!isChild) { transform.SetParent(trans); isChild = true; }
      
      //  scale = transform.lossyScale;
        //   transform.localScale = new Vector3(scale.x / transform.parent.localScale.x, scale.y / transform.parent.localScale.y, scale.z / transform.parent.localScale.z);
       

    
       // transform.localScale = scale;

        //  Destroy(gameObject);


    }


}
