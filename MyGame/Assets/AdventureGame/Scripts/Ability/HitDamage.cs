using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class HitDamage : MonoBehaviour {

    public int atk=1;
    public int flick = 30;
    private void OnCollisionStay(Collision collision)
    {
        if (Player.playEnd!=0) return;
        Transform trans = collision.gameObject.transform;
        if (trans.tag == "Monster" || trans.tag == "Others")
        {
            trans.GetComponent<HPObject>().Damage(atk);
            Vector3 dir = (GameManager.player.transform.position - collision.transform.position).normalized;
            dir.y = 0;
            try
            {
                collision.gameObject.GetComponent<Rigidbody>().AddForce(-dir * flick, ForceMode.Impulse);
            }
            catch { }

        }
       // Tools.PlayParticletAtPosByName("BoomEffectTwo", transform);
     //   Destroy(gameObject);
    }

 
    


}
