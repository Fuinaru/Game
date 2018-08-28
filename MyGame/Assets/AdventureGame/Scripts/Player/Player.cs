using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using Fungus;
public class Player : myGameObject
{

    public int maxHp=5;
    public int Hp=5;
    public GameObject HpContorl;
    bool hurted = false;
    private float time=0;
    ForInclude LowHPTool = new ForInclude();
    ForInclude HurtedTool = new ForInclude();


   [System.Serializable]
    public struct LowHpEffect
    {
        public Image lowHpRedImg;
        public Color minColor;
        public Color maxColor;
        public float speed;
    };
    public LowHpEffect lowHpEffect;


    private void Start()
    {
        HpContorl.transform.GetComponent<Slider>().maxValue=maxHp;
    }
    // Update is called once per frame
    void Update () {
        if (GameManager.isTimePause) { return; }
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            Damage(1);
                       }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Damage(-1);
        }
        hurtCount();
        LowHp();
        PlayerFlash();

       // if (transform.position.y > 30) Flowchart.BroadcastFungusMessage("flyhigh");
    }
    public void Damage(int a) {
        if (!hurted) { Hp -= a; getHurted(); }
        HpContorl.GetComponent<Slider>().value=Hp;
    }
    public void getHurted() {
        hurted = true;  
    }
    private void hurtCount() {
        if (hurted)
        {
            time += Time.deltaTime;
            if (time >= 3)
            {
                HurtedTool.materialBecomeWhite(transform);
                if(HurtedTool. isChildrenColorB(transform,Color.white)) hurted = false; time = 0;
            }
        }
    }
    private void PlayerFlash()
    {
        if (hurted)
        {
            HurtedTool.flash(transform,new Color(1, 0, 0, 0.7f), Color.white, 10);
        }
      
    }
    private bool IsLowHp() {
        return Hp <= 2;
    }

  private  bool comp = true;
    private void LowHp() {
        
        if (IsLowHp())
        {

            LowHPTool.ColorA2BCir(lowHpEffect.lowHpRedImg, lowHpEffect.minColor, lowHpEffect.maxColor, lowHpEffect.speed);
        }
        else LowHPTool.Color2B(lowHpEffect.lowHpRedImg, Color.clear, lowHpEffect.speed);
    }
    public Transform ReturnTransform()
    {

        return GetComponent<Transform>();
    }
    }
