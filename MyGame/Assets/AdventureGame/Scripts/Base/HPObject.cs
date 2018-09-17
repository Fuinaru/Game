using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HPObject : MyGameObject {

    public int maxHp = 5;
    public int hp = 5;
    public Slider HpContorl;
    public bool hurted = false;
    private float time = 0;
    protected ForInclude HurtedTool = new ForInclude();
    protected bool isDestory = false;
    public float hurtedCoolTime = 3;
    protected bool otherFlash = false;
    // Use this for initialization
    protected void Start () {
        base.Start();




        if (transform.tag == "Monster")
        {
            HpContorl = GetComponent<creatFollowingUI>().Tar.GetComponent<Slider>();
            HpContorl.maxValue = maxHp;
            UpdateHpUI();
        }

    }

    // Update is called once per frame
    protected void Update () {
        base.Update();
        hurtCount();
      if(!otherFlash)Flash();
        if (hp <= 0) GoDie();
    }
    public virtual void Damage(int a)
    {
        if (!hurted) { hp -= a; getHurted(); }
        UpdateHpUI();

    }
    public void Cure(int a)
    {
        hp += a;
        if (hp > maxHp) hp = maxHp;
        UpdateHpUI();
    }
    public virtual void getHurted()
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
                if (!otherFlash)
                {
                    HurtedTool.materialBecomeWhite(transform);
                  
                }
                time = 0;
                hurted = false;
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
    public void FlashOther(Color color,float speed)
    {
        otherFlash = true;
        if (!isDestory)
        {
            try { HurtedTool.flash(transform, color, Color.white, speed); }
            catch { }
        }
    }
    public void FlashOtherEnd() {
        otherFlash = false;
        HurtedTool.materialBecomeWhite(transform);
    }
    public virtual void UpdateHpUI()
    {
        HpContorl.value = hp;
 
    }

    public virtual void GoDie()
    { 
            if (!isDestory)
            {
            Tools.PlayFollowingParticletByName("SmokeEffect", transform);
                Destroy(gameObject.GetComponent<Collider>());
                Destroy(m_rigidbody);
                isDestory = true;
            }

            HurtedTool.Color2B(GetComponentInChildren<Renderer>().material, new Color(1, 1, 1, 0), 30);
            if (HurtedTool.isChildrenColorB(transform, new Color(1, 1, 1, 0))) Destroy(gameObject);
        }
   
}
