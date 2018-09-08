using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MyGameObject {

    public int speed;
    private int m_speed=0;

    [System.Serializable]
    public struct ControlCode
    {
        public KeyCode upKey ;
        public KeyCode downKey ;
        public KeyCode leftKey ;
        public KeyCode rightKey ;
    };
    KeyCode pressedKey;

    public ControlCode controlCode;
    Animator m_Animator;



    void Start() {
        base.Start();
        dataInitial();
    }

    // Update is called once per frame
    void Update() {

        PlayerMove();

        //AniStateRecovery();
    }
    public void dataInitial() {
        pressedKey = KeyCode.A + 999;
        m_Animator= GetComponent<Animator>();

    }


    public void PlayerMove() {

        if (pressedKey!= KeyCode.A + 999 && Input.GetKeyUp(pressedKey))
        {
            pressedKey = KeyCode.A + 999;
            m_speed = 0;
        }
        if (!IsAniState("Hurted"))
        {

            if (Input.GetKeyDown(controlCode.downKey))
            {
                m_speed = speed;
                pressedKey = controlCode.downKey;
                transform.eulerAngles = new Vector3(0, -135, 0);
            }
            if (Input.GetKeyDown(controlCode.upKey))
            {
                pressedKey = controlCode.upKey;
                m_speed = speed;
                transform.eulerAngles = new Vector3(0, 45, 0);
            }
            if (Input.GetKeyDown(controlCode.leftKey))
            {
                pressedKey = controlCode.leftKey;
                m_speed = speed;
                transform.eulerAngles = new Vector3(0, -45, 0);
            }
            if (Input.GetKeyDown(controlCode.rightKey))
            {
                pressedKey = controlCode.rightKey;
                m_speed = speed;
                transform.eulerAngles = new Vector3(0, 135, 0);
            }
        }
        else { m_speed = 0; }
        m_Animator.SetFloat("Speed", m_speed);
  
            m_rigidbody.velocity = transform.forward * m_speed;
      
    }


    public bool IsAniState(string str)
    {
       return  m_Animator.GetCurrentAnimatorStateInfo(0).IsName(str);

    }
}
