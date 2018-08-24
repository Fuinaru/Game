using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolF;
using UnityEngine.UI;
public class MonsterFoolAi : MonoBehaviour {

    // Use this for initialization
    public Player player;
    private Transform m_Player;
    public float speed = 1;
    public float viewMinDistance = 5;
    public float viewMaxDistance = 10;
    private Vector3 dir;
    public bool IsFindPlayer=false;
    public int maxHp = 5;
    public int Hp = 5;
    public GameObject HpContorl;
    public bool hurted = false;
    private float time = 0;
    ForInclude HurtedTool = new ForInclude();


    void Start () {
        if (player == null) m_Player = GameObject.FindWithTag("Player").transform;
        else m_Player = player.ReturnTransform();
	}

    // Update is called once per frame
    void Update() {
         dir = m_Player.position - transform.position;
        Debug.Log(dir.magnitude);
        isFindPlayer();
        if (IsFindPlayer)
        {
            Debug.Log("faxian");
            transform.LookAt(m_Player);
      
           GetComponent<Rigidbody>().velocity=dir.normalized*10* speed * Time.deltaTime;
        }
        hurtCount();
        Flash();
    }
   void isFindPlayer() {
        if (dir.magnitude > viewMaxDistance)
            IsFindPlayer = false;
        else if (hurted || dir.magnitude < viewMinDistance) IsFindPlayer = true;
      //  else IsFindPlayer = false;
    }

    public void Damage(int a)
    {
        if (!hurted) { Hp -= a; getHurted(); }
      //  HpContorl.GetComponent<Slider>().value = Hp;
    }
    public void getHurted()
    {
        hurted = true;
    }
    private void hurtCount()
    {
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
    private void Flash()
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

}
