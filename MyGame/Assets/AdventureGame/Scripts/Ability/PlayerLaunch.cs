﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayerLaunch : BaseLuanch
{


    public int bulletNum = 5;
    public int boomNum = 3;
    public BagSystem bagSystem;
    public GameObject Eone;
    public GameObject Etwo;

    public static GameObject SBullet;
    public static GameObject SBoom;
    KeyCode A = KeyCode.J;
    KeyCode B = KeyCode.K;
    public static Transform trans;


    // Use this for initialization
    void Start()
    {
        SBullet = bullet;
        SBoom = boom;
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
    }
    void UseWeapon(GameObject o) {
        try {
            Debug.Log("useWeapon");
            Transform i;
            i = o.transform.GetChild(0);
            useItem(i);

        } catch { }

    }
    public void getItem(BagItem.ItemType type, int a)
    {
        bagSystem.AddItem(type, a);
    }

    public void useItem(Transform i)
    {
        BagItem o = i.GetComponent<BagItem>();
        Debug.Log("useItem");
        if (o.itemType == BagItem.ItemType.bullet)
        {
            Debug.Log("bullet");
            GameObject go = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
            go.GetComponent<Rigidbody>().AddRelativeForce(0, 0, 1200);
        }
        if (o.itemType == BagItem.ItemType.boom)
        {
            Debug.Log("Boom");
            GameObject go = Instantiate(boom, transform.position, transform.rotation) as GameObject;
            go.GetComponent<Rigidbody>().AddRelativeForce(0, 0, 100);
        }

        o.itemNum--;
        o.updateText();
    }


}