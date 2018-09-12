using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;
public class Player : HPObject
{

    public int coinNum=0 ;

    ForInclude LowHPTool = new ForInclude();
    [System.Serializable]
    public struct LowHpEffect
    {
        public Image lowHpRedImg;
        public Color minColor;
        public Color maxColor;
        public float speed;
    };
    public LowHpEffect lowHpEffect;
    public Text hpText; public Text coinText;

    void Start()
    {
        base.Start();
        UpdateCoinUI();
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.IsTimePause()) { m_rigidbody.velocity = Vector3.zero; return; }
        base.Update();
        LowHp();
        // if (transform.position.y > 30) Flowchart.BroadcastFungusMessage("flyhigh");
    }


    private bool IsLowHp()
    {
        return hp <= 2;
    }
    private void LowHp()
    {

        try
        {
            if (IsLowHp()) LowHPTool.ColorA2BCir(lowHpEffect.lowHpRedImg, lowHpEffect.minColor, lowHpEffect.maxColor, lowHpEffect.speed);
            else LowHPTool.Color2B(lowHpEffect.lowHpRedImg, Color.clear, lowHpEffect.speed);

        }
        catch { }
    }
    public override void GoDie()
    {
        
            GameManager.isGameOver = true;
            GameManager.GameOver();
    }
    public  void MaxHPUp(int value)
    {
        maxHp += value;
    }
    public override void UpdateHpUI() {
        HpContorl.value = hp;
        hpText.text = hp + "/" + maxHp;
    }


    public void GetCoin(int num)
    {
        coinNum += num;
        UpdateCoinUI();
    }
    public bool CostCoin(int num)
    {
        if (coinNum < num) return false;
        coinNum -= num;
        UpdateCoinUI();
        return true;
    }

    public  void UpdateCoinUI()
    {
        coinText.text = "金币："+ coinNum;
    }
    public void DamageWithAni(int a,Transform trans)
    {
        if (!hurted) {
            hp -= a; getHurted();
            Tools.LookAtOnlyYAxis(transform, trans);
          //  m_rigidbody.AddForce((transform.position-trans.position).normalized*100,ForceMode.VelocityChange);
            GameManager.playerAni.SetTrigger("GetHurted");
            UpdateHpUI();
        }

    }
  

} 
