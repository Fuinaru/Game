using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBall : Bullet
{
    // Use this for initialization
    void Start()
    {
      
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Transform trans = collision.gameObject.transform;
        if (trans.tag == "Monster" || trans.tag == "Others" || trans.tag == "Player") trans.GetComponent<HPObject>().Damage(atk);
        Tools.PlayParticletAtPosByName("PoisonSmokeEffect", transform);
        Tools.PlaySoundByName("boom", transform);
        Destroy(gameObject);



    }

}
