using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HPObject : myGameObject {

    public int maxHp = 5;
    public int Hp = 5;
    public GameObject HpContorl;
    protected bool hurted = false;
    private float time = 0;
    ForInclude HurtedTool = new ForInclude();
    protected bool isDestory = false;
    // Use this for initialization
    protected void Start () {
        base.Start();

        if (transform.tag=="Player") HpContorl.transform.GetComponent<Slider>().maxValue = maxHp;
      
        if (transform.tag == "Monster")
        {
            HpContorl = GetComponent<creatFollowingUI>().Tar;
            HpContorl.transform.GetComponent<Slider>().maxValue = maxHp;
        }

    }

    // Update is called once per frame
    protected void Update () {
        base.Update();
        hurtCount();
        Flash();
        if (Hp <= 0) goToDie();
    }
    public void Damage(int a)
    {
        if (!hurted) { Hp -= a; getHurted(); }
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
            if (time >= 3)
            {
                HurtedTool.materialBecomeWhite(transform);
                if (HurtedTool.isChildrenColorB(transform, Color.white)) hurted = false; time = 0;
            }
        }
    }
    private void Flash()
    {
        if (hurted && !isDestory)
        {
            try { HurtedTool.flash(transform, new Color(1, 0, 0, 0.7f), Color.white, 10); }
            catch { }
        }

    }

    public void goToDie()
    {
        if (transform.tag == "Player") {
            GameManager.isGameOver = true;
        }
        else {
            if (!isDestory)
            {
                Destroy(gameObject.GetComponent<Collider>());
                Destroy(m_rigidbody);
                isDestory = true;
            }

            HurtedTool.Color2B(GetComponentInChildren<Renderer>().material, new Color(1, 1, 1, 0), 20);
            if (HurtedTool.isChildrenColorB(transform, new Color(1, 1, 1, 0))) Destroy(gameObject);
        }
    }
}
