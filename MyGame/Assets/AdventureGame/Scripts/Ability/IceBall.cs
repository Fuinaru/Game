using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBall : Bullet
{
    public float iceTime = 5f;
    protected float _iceTime = 0;
    private Transform trans;
    // Use this for initialization
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if (_iceTime > 0) {
            _iceTime -= Time.deltaTime;
        
            if (trans==null||trans.GetComponent<Rigidbody>() == null)
            { _iceTime = 0; Destroy(gameObject); return; }
            trans.GetComponent<Rigidbody>().velocity /= 3;
            trans.GetComponent<HPObject>().FlashOther(new Color(0,0,1,0.7f),5);
    }
    }
    private void OnDestroy()
    {
        try
        {
            trans.GetComponent<HPObject>().FlashOtherEnd();
        }
        catch { }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (_iceTime > 0) return;
        Transform _trans = collision.gameObject.transform;
        Tools.PlayParticletAtPosByName("IceEffect", transform);
        if (_trans.tag == "Monster" || _trans.tag == "Others" || _trans.tag == "Player")
        {
            _trans.GetComponent<HPObject>().Damage(atk);
            trans = _trans;
            _iceTime = iceTime;
            try
            {
                Destroy(transform.GetChild(0).gameObject);
            }
            catch { }

        }
        else
        {
           
            Destroy(gameObject);
        }

    }
    protected override void GoDie()
    {
        if (aliveTime <= 0&& _iceTime<=0) Destroy(gameObject);
    }
}
