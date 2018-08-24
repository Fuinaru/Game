using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

using ToolF;
public class Player : MonoBehaviour {

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
                hurted = false; time = 0;

                Renderer[] rds = transform.GetComponentsInChildren<Renderer>();
                //逐一遍历他的子物体中的Renderer
                foreach (Renderer render in rds)
                {
                    //逐一遍历子物体的子材质（renderer中的material）
                    foreach (Material material in render.materials)
                    {

                        material.color = Color.white;
                        //ColorA2BCir(lowHpEffect.lowHpRedImg.material, material.color, Color.white, 20f);

                    }
                }
            }
        }
    }
    private void PlayerFlash()
    {
        if (hurted)
        {
            Renderer[] rds = transform.GetComponentsInChildren<Renderer>();
            //逐一遍历他的子物体中的Renderer
            foreach (Renderer render in rds)
            {
                //逐一遍历子物体的子材质（renderer中的material）
                foreach (Material material in render.materials)
                {

                    HurtedTool.ColorA2BCir(material, new Color(1, 0, 0, 0.7f), Color.white, 20);
                    //ColorA2BCir(lowHpEffect.lowHpRedImg.material, material.color, Color.white, 20f);

                }
            }
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
        else LowHPTool.ColorA2B(lowHpEffect.lowHpRedImg, Color.clear, lowHpEffect.speed);
    }
    public Transform ReturnTransform()
    {

        return GetComponent<Transform>();
    }
    }
