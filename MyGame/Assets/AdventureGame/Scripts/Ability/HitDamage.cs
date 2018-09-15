using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class HitDamage : MonoBehaviour {

    public int atk=2;
    public int flick = 15;
    private void OnCollisionStay(Collision collision)
    {
        if (Player.playEnd!=0) return;
        Transform trans = collision.gameObject.transform;
        if (trans.tag == "Monster" || trans.tag == "Others")
        {
            StartCoroutine(PlayerAttack(trans));
        }
        Tools.PlayParticletAtPosByName("BoomEffect 1", transform);
       
        //   Destroy(gameObject);
    }


    IEnumerator PlayerAttack(Transform trans)
    {
        yield return new WaitForSeconds(0.05f);

    
        try
        {
            Vector3 dir = (GameManager.player.transform.position - trans.position).normalized;
            dir.y = 0;
            trans.gameObject.GetComponent<Rigidbody>().AddForce(-dir * flick, ForceMode.Impulse);
            trans.GetComponent<HPObject>().Damage(atk);
        }
        catch { }
    }



}
