using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour {

	public int maxHp=5;
    public int Hp=5;
    public GameObject HpContorl;
    bool hurted = false;
    public Image hurtRedImg;
    private float time=0;
    private float count=0;
    Color color ;
    private void Start()
    {
        HpContorl.transform.GetComponent<Slider>().maxValue=maxHp;
        color = hurtRedImg.GetComponent<Image>().color;
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

        if (hurted)
        {
            time += Time.deltaTime;
            count += Time.deltaTime;
            if (count > 0.4) {
                hurtRedImg.GetComponent<Image>().color = new Color(1, 0, 0, 0.5f);
                hurtRedImg.GetComponent<Image>().CrossFadeColor(Color.clear, 0.2f, true, true);
				  hurtRedImg.GetComponent<Image>().CrossFadeColor(Color.red, 0.2f, true, true);
              //  hurtRedImg.GetComponent<Image>().color = new Color(color.r, color.g, color.b, Random.value);
                count = 0;
            }
            if (time >= 3) { hurted = false;time = 0; Color color = hurtRedImg.GetComponent<Image>().color;
                hurtRedImg.GetComponent<Image>().color = new Color(1,0,0, 0);
            }
        }
    }
    public void Damage(int a) {
      if(!hurted) Hp-=a;
        HpContorl.GetComponent<Slider>().value=Hp;
    }
    public void getHurted() {
        hurted = true;
      
    }
}
