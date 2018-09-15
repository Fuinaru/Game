using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;
public class Player : HPObject
{
    public int maxMp = 5;
    public int mp = 5;
    public Slider MpContorl;

    public int coinNum = 0;
    public static int playEnd = 1;
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
    public Text hpText; public Text mpText; public Text coinText;

    public GameObject sword;

    void Start()
    {
        base.Start();
        UpdateHpUI();
        UpdateMpUI();
        UpdateCoinUI();
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.IsTimePause()) { m_rigidbody.velocity = Vector3.zero;return; }
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


    public bool CostMp(int a)
    {
        if (mp < a) return false;
        mp -= a;
        UpdateMpUI();
        return true;
    }
    public void MpRecovery(int a)
    {
        mp += a;
        if (mp > maxHp) mp = maxMp;
        UpdateMpUI();
    }


    public override void GoDie()
    {

        GameManager.isGameOver = true;
        GameManager.GameOver();
    }
    public void MaxHPUp(int value)
    {
        maxHp += value;
        hp = maxHp;
        UpdateHpUI();
    }
    public void MaxMPUp(int value)
    {
        maxMp += value;
        mp = maxMp;
        UpdateMpUI();
    }
    public override void UpdateHpUI() {
        HpContorl.maxValue = maxHp;
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
    public virtual void UpdateMpUI()
    {
        MpContorl.maxValue = maxMp;
        MpContorl.value = mp;
        mpText.text = mp + "/" + maxMp;

    }

    public void UpdateCoinUI()
    {
        coinText.text = "金币：" + coinNum;
    }
    public void DamageWithAni(int a, Transform trans)
    {

        if (!hurted) {
            hp -= a; getHurted();

            if (!GameManager.playerAni.GetCurrentAnimatorStateInfo(0).IsName("GetHurted"))
            {
                Tools.LookAtOnlyYAxis(transform, trans);
                GameManager.playerAni.SetTrigger("GetHurted");
            }

            //   Vector3 dir = (transform.position - trans.position).normalized;
            //    dir.y = 0;
            //    m_rigidbody.AddForce(dir * 30, ForceMode.Impulse);


            //  m_rigidbody.AddForce((transform.position-trans.position).normalized*100,ForceMode.VelocityChange);

            UpdateHpUI();
        }

    }
    public void UpDateAllUI(){
        UpdateHpUI();
        UpdateMpUI();
        UpdateCoinUI();
}



    public void PlayEnd(int a)
    {
        playEnd = a;
    }

    public void SetSwordVisible() {
        sword.SetActive(true);

    }

    public void SetSwordInvisible()
    {
        sword.SetActive(false);

    }
} 
