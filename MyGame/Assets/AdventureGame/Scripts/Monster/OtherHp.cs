using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OtherHp : MonoBehaviour
{

    public int maxHp = 5;
    public int Hp = 5;
   // public GameObject HpContorl;

    private void Start()
    {
      //  HpContorl.transform.GetComponent<Slider>().maxValue = maxHp;
    }
    // Update is called once per frame
    void Update()
    {
 
    }
    public void HpChange(int a)
    {
        Hp--;
      //  HpContorl.GetComponent<Slider>().value = Hp;
    }

}
