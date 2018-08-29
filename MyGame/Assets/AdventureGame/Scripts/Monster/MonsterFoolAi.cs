﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MonsterFoolAi : myGameObject
{

    // Use this for initialization
    public Player player;
    private Transform m_Player;
    public float speed = 1;
    public float viewMinDistance = 5;
    public float viewMaxDistance = 10;
    private Vector3 dir;
    public bool IsFindPlayer=false;
    public int maxHp = 6;
    public int Hp = 6;
    public int atk =1;
    public int flickPower = 5;
    private GameObject HpContorl;
    public bool hurted = false;
    private float time = 0;
    private bool isAttacking = false;
    ForInclude EffectTool = new ForInclude();
    private bool isDestory = false;

    void Start () {
        if (player == null) m_Player = GameObject.FindWithTag("Player").transform;
        else m_Player = player.ReturnTransform();
        Hp = maxHp;
        HpContorl=GetComponent<creatFollowingUI>().tar;
        HpContorl.transform.GetComponent<Slider>().maxValue = maxHp;
    }

    // Update is called once per frame
    void Update() {
        if (GameManager.isTimePause) return;
        dir = m_Player.position - transform.position;
        Debug.Log(dir.magnitude);
        isFindPlayer();
        if (IsFindPlayer&&!isDestory)
        {
           // Debug.Log("faxian");
            transform.LookAt(m_Player);

           if(!isAttacking) GetComponent<Rigidbody>().velocity=dir.normalized*10* speed * Time.deltaTime;
        }
        hurtCount();
        Flash();
        if (Hp <= 0) goToDie();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player") {
            collision.transform.GetComponent<Rigidbody>().velocity = dir.normalized * flickPower;
            collision.transform.GetComponent<Player>().Damage(atk);
            isAttacking = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {

            isAttacking = false;
        }
    }
    void isFindPlayer() {
        if (dir.magnitude > viewMaxDistance)
            IsFindPlayer = false;
        else if (hurted || dir.magnitude < viewMinDistance) IsFindPlayer = true;
      //  else IsFindPlayer = false;
    }

    public void Damage(int a)
    {
        // if (!hurted) { Hp -= a; getHurted(); }
        Hp -= a; getHurted();
          HpContorl.GetComponent<Slider>().value = Hp;
    }
    public void getHurted()
    {
        hurted = true;
    }
    private void hurtCount()
    {
        if (hurted)
        {
            time += Time.deltaTime;
            if (time >= 3&&!isDestory)
            {
                hurted = false; time = 0;

                EffectTool.materialBecomeWhite(transform);
            }
        }
    }
    private void Flash()
    {
        if (hurted&&!isDestory)
        {
            EffectTool.flash(transform, new Color(1, 0, 0, 0.7f), Color.white, 20);
        }

    }
    public void goToDie() {
        if (!isDestory)
        {
            Destroy(gameObject.GetComponent<Collider>());
            Destroy(gameObject.GetComponent<Rigidbody>());
            isDestory = true;
        }

        EffectTool.Color2B(GetComponentInChildren<Renderer>().material, new Color(1, 1, 1, 0), 20);
        if (EffectTool.isChildrenColorB(transform, new Color(1, 1, 1, 0))) Destroy(gameObject);
    }

}
