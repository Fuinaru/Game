using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : myGameObject
{
public float aliveTime=5f;
    public int atk = 1;
    private Vector3 scale;
    private Vector3 pos;
    private bool isChild = false;
    // Use this for initialization
    private void Start()
    {

    }
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
    private void OnCollisionEnter(Collision collision)
    {
        Transform trans = collision.transform;
     


        if (trans.tag == "Monster" || trans.tag == "Others")
            trans.GetComponent<MonsterFoolAi>().Damage(atk);
    


    pos = transform.position;
        Destroy(gameObject.GetComponent<Collider>());
        Destroy(gameObject.GetComponent<Rigidbody>());
        if (!isChild) { transform.SetParent(trans); isChild = true; }
      
      //  scale = transform.lossyScale;
        //   transform.localScale = new Vector3(scale.x / transform.parent.localScale.x, scale.y / transform.parent.localScale.y, scale.z / transform.parent.localScale.z);
       
        transform.position = pos;
    
       // transform.localScale = scale;

        //  Destroy(gameObject);


    }


}
