using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletItem : BagItem
{
    public float autoShootDis = 8;

    // Use this for initialization
    private void Awake()
    {
        coolTime = 1f;
    }
    protected void Start()
    {
        base.Start();
      
    }

    // Update is called once per frame
    protected void Update()
    {
        base.Update();
    }
    public override void useItem()
    {
        if (!ItemCoolEnd()) return;
        if (BaseMonster.nearestMonster != null)
        {
            Vector3 dir = BaseMonster.nearestMonster.position - GameManager.player.transform.position;
            Debug.Log(dir.magnitude);
            if (dir.magnitude <= autoShootDis) GameManager.player.transform.eulerAngles = new Vector3(0, Mathf.Atan2(dir.x, dir.z) * 180 / Mathf.PI, 0);
        }
        GameObject go = Instantiate(GetItemObject(), PlayerLaunch.trans.position, PlayerLaunch.trans.rotation) as GameObject;
        go.GetComponent<Rigidbody>().AddRelativeForce(0, 0, 1000);
        useUpdate();

       
    }


}