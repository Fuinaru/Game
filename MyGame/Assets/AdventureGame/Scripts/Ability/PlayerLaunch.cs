using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayerLaunch : BaseLuanch
{


    public int bulletNum = 5;
    public int boomNum = 3;

    KeyCode openFire = KeyCode.F;
    KeyCode setBoom = KeyCode.B;
    public Text bulletNumText;
    public Text boomNumText;
    // Use this for initialization
    void Start()
    {
        updateUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isTimePause) return;
        if (Input.GetKeyDown(openFire) && bulletNum > 0 && BulletCoolEnd())
        {
            Shoot();
            bulletNum--;
            updateUI();
        }
        if (Input.GetKeyDown(setBoom) && boomNum > 0 && BoomCoolEnd())
        {
            nexttimeB = boomCoolTime + Time.time;
            SetBoom();
            boomNum--;
            updateUI();
        }
    }
    public void getBullet(int a)
    {
        bulletNum += a;
        updateUI();
    }
    public void getBoom(int a)
    {
        boomNum += a;
        updateUI();
    }
    void updateUI()
    {
        bulletNumText.text = "子弹：" + bulletNum;
        boomNumText.text = "炸弹：" + boomNum;
    }


}