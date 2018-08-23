using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Luanch : MonoBehaviour
{

    public GameObject bullet;
    public GameObject boom;
    public int bulletNum=5;
    public int boomNum=3;
    float firetime = 0.2f;
    float nexttime = 0.0f;
    float firetimeB = 1f;
    float nexttimeB = 0.0f;
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
            if (Input.GetKeyDown(openFire) && nexttime < Time.time&&bulletNum>0)
        {
           
            nexttime = firetime + Time.time;
            GameObject go = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
            go.GetComponent<Rigidbody>().AddRelativeForce(0, 0, 1200);
            bulletNum--;
            updateUI();
        }
        if (Input.GetKeyDown(setBoom) && nexttimeB < Time.time && boomNum > 0)
        {
            nexttimeB = firetimeB + Time.time;
            GameObject go = Instantiate(boom, transform.position, transform.rotation) as GameObject;
            go.GetComponent<Rigidbody>().AddRelativeForce(0, 0, 100);
            boomNum--;
            updateUI();
        }
    }
    public void getBullet(int a) {
        bulletNum += a;
        updateUI();
    }
    public void getBoom(int a)
    {
        boomNum += a;
        updateUI();
    }
    void updateUI() {
        bulletNumText.text = "子弹：" + bulletNum;
        boomNumText.text = "炸弹：" + boomNum;
    }
}