using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportItem : BagItem
{
    public int costMp = 1;
    public int teleportDis = 8;
    private void Awake()
    {
        consumAble = false;
        coolTime = 2f;
    }
    // Use this for initialization
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
        if (!(Player.playEnd == 1)) return;
        if (!GameManager.player.CostMp(costMp)) return;
        Player.playEnd = -1;

        PlayerControl.m_speed = 0;
        GameManager.playerAni.SetTrigger("MagicAttack");
        Tools.PlayParticletAtPosByName("SmokeEffect", GameManager.player.transform);
        useUpdate();
        StartCoroutine(StartAfter());

    }

    IEnumerator StartAfter()
    {
        yield return new WaitUntil(() => Player.playEnd == 0);

        Ray ray = new Ray(GameManager.player.transform.position+new Vector3(0,0.4f,0), GameManager.player.transform.forward);

        RaycastHit HitInfo;
        bool result = Physics.Raycast(ray, out HitInfo, teleportDis);
        if (result)
        {
             Vector3 pos = HitInfo.point;
            pos.y = 0;
            GameManager.player.transform.position = pos - GameManager.player.transform.forward;
        }
        else
        {
            Vector3 pos = GameManager.player.transform.position +GameManager.player.transform.forward * teleportDis;
            pos.y = 0;
            GameManager.player.transform.position = pos;
            // Debug.Log(HitInfo.collider.name);

        }

        Tools.PlayParticletAtPosByName("SmokeEffect", GameManager.player.transform);



    }

}