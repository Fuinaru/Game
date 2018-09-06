using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;
public class Player : HPObject
{



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


    void Start()
    {
        base.Start();
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.isTimePause) { m_rigidbody.velocity = Vector3.zero; return; }
        base.Update();
        LowHp();
        // if (transform.position.y > 30) Flowchart.BroadcastFungusMessage("flyhigh");
    }


    private bool IsLowHp()
    {
        return Hp <= 2;
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
    public override void GoToDie()
    {
        
            GameManager.isGameOver = true;
            GameManager.GameOver();
    }

} 
