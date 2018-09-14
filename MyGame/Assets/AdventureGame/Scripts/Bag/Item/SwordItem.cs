using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordItem : BagItem
{

    protected void Awake()
    {
        coolTime = 0.8f;
        consumAble = false;
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
        if (!(Player.playEnd==1)) return;
        GameManager.player.sword.SetActive(true);
        Player.playEnd = -1;
        if (BaseMonster.nearestMonster != null)
        {
            Vector3 dir = BaseMonster.nearestMonster.position - GameManager.player.transform.position;
            Debug.Log(dir.magnitude);
            if (dir.magnitude <= 4) GameManager.player.transform.eulerAngles = new Vector3(0, Mathf.Atan2(dir.x, dir.z) * 180 / Mathf.PI, 0);
        }
        PlayerControl.m_speed = 0;
        GameManager.playerAni.SetTrigger("SwordSlash");
        useUpdate();
    }
 }
