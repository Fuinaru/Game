using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayerLaunch : BaseLuanch
{


    public int bulletNum = 5;
    public int boomNum = 3;
    public BagSystem bagSystem;
    public GameObject Eone;
    public GameObject Etwo;


    KeyCode A = KeyCode.J;
    KeyCode B = KeyCode.K;
    public static Transform trans;


    // Use this for initialization
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        trans = transform;
        if (GameManager.isTimePause) return;
        if (Input.GetKeyDown(A) &&  BulletCoolEnd())
        {
            Debug.Log("keyDown");
            nexttime = bulletCoolTime + Time.time;
            UseWeapon(Eone);
        }
        if (Input.GetKeyDown(B) &&  BoomCoolEnd())
        {
            nexttimeB = boomCoolTime + Time.time;
            UseWeapon(Etwo);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            getItem(MyGameVariable.ItemType.BoomItem, 10);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            getItem(MyGameVariable.ItemType.BulletItem, 10);
        }
    }
    void UseWeapon(GameObject o) {
        try {
            Debug.Log("useWeapon");
            Transform i;
            i = o.transform.GetChild(0);
         //   useItem(i);
            i.GetComponent<BagItem>().useItem();
        } catch { }
     
    }
    public void getItem(MyGameVariable.ItemType type, int a)
    {
        bagSystem.AddItem(type, a);
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