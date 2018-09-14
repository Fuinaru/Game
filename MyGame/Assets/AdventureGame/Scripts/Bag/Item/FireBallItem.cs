using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallItem : BagItem
{
    public float autoShootDis = 8;
    public int costMp = 1;
    private void Awake()
    {
        consumAble = false;
        coolTime = 3f;
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
        if (!GameManager.player.CostMp(costMp)) return;
        Player.playEnd = -1;
        if (BaseMonster.nearestMonster != null)
        {
            Vector3 dir = BaseMonster.nearestMonster.position - GameManager.player.transform.position;
            Debug.Log(dir.magnitude);
            if (dir.magnitude <= autoShootDis) GameManager.player.transform.eulerAngles = new Vector3(0, Mathf.Atan2(dir.x, dir.z) * 180 / Mathf.PI, 0);
        }
        PlayerControl.m_speed = 0;
        GameManager.playerAni.SetTrigger("MagicAttack");
        useUpdate();
        StartCoroutine(PlayerAttack());

    }

    IEnumerator PlayerAttack()
    {
        yield return new WaitUntil(() => Player.playEnd==0);
        //yield return null;
        GameObject go = Instantiate(GetItemObject(), PlayerLaunch.trans.position, PlayerLaunch.trans.rotation) as GameObject;
        go.GetComponent<Rigidbody>().AddRelativeForce(0, 0, 500);


    }

}