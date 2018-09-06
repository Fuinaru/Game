using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HPObject : myGameObject {

    public int maxHp = 5;
    public int Hp = 5;
    public Slider HpContorl;
    protected bool hurted = false;
    private float time = 0;
    ForInclude HurtedTool = new ForInclude();
    protected bool isDestory = false;
    public float hurtedCoolTime = 3;
    // Use this for initialization
    protected void Start () {
        base.Start();

        if (transform.tag=="Player") HpContorl.maxValue = maxHp;
      
        if (transform.tag == "Monster")
        {
            HpContorl = GetComponent<creatFollowingUI>().Tar.GetComponent<Slider>();
            HpContorl.maxValue = maxHp;
        }

    }

    // Update is called once per frame
    protected void Update () {
        base.Update();
        hurtCount();
        Flash();
        if (Hp <= 0) GoToDie();
    }
    public void Damage(int a)
    {
        if (!hurted) { Hp -= a; getHurted(); }
        HpContorl.value = Hp;

    }
    public void Cure(int a)
    {
        Hp += a;
        HpContorl.value = Hp;
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
            if (time >= hurtedCoolTime)
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

    public virtual void GoToDie()
    { 
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
