using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayerLaunch : BaseLuanch
{


    public int bulletNum = 5;
    public int boomNum = 3;

   

    KeyCode start = KeyCode.Keypad1;

    public static Transform trans;


    // Use this for initialization
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        trans = transform;
        if (GameManager.IsTimePause()) return;

        for (int i = 0; i < GameManager.bagSys.EquipNum; i++) {
            if (Input.GetKeyDown(start+i) )
            {
                nextTime = coolTime + Time.time;
                UseWeapon(GameManager.bagSys.equipObj.transform.GetChild(i).gameObject);
            }

        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            getItem(Var.ItemType.PoisonItem, 10);
            getItem(Var.ItemType.MpPotionItem, 10);
            getItem(Var.ItemType.IceBallItem, 10);
            getItem(Var.ItemType.FireBallItem, 10);
            getItem(Var.ItemType.SwordItem, 10);
            getItem(Var.ItemType.PotionItem, 10);
            getItem(Var.ItemType.BoomItem, 10);
            getItem(Var.ItemType.BulletItem, 10);
            getItem(Var.ItemType.TeleportItem, 10);
            getItem(Var.ItemType.MaxHpUpItem, 10);
            getItem(Var.ItemType.MaxMpUpItem, 10);
        }
  
    }
    void UseWeapon(GameObject o) {
        try {
            Transform i;
            i = o.transform.GetChild(0);
         //   useItem(i);
            i.GetComponent<BagItem>().useItem();
        } catch { }
     
    }
    public void getItem(Var.ItemType type, int a)
    {
        GameManager.bagSys.AddItem(type, a);
    }
   
    //public void useItem(Transform i)
    //{
    //    BagItem o = i.GetComponent<BagItem>();
    //    Debug.Log("useItem");
    //    if (o.itemType == MyGameVariable.ItemType.BulletItem)
    //    {
    //        Debug.Log("bullet");
    //        GameObject go = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
    //        go.GetComponent<Rigidbody>().AddRelativeForce(0, 0, 1200);
    //    }
    //    if (o.itemType == MyGameVariable.ItemType.BoomItem)
    //    {
    //        Debug.Log("Boom");
    //        GameObject go = Instantiate(boom, transform.position, transform.rotation) as GameObject;
    //        go.GetComponent<Rigidbody>().AddRelativeForce(0, 0, 100);
    //    }

    //    o.itemNum--;
    //    o.updateText();
    //}


}