using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MyGameObject {

    public int speed;
    public static float m_speed = 0;
    private Vector2 moveDir = Vector2.zero;

    [System.Serializable]
    public struct ControlCode
    {
        public KeyCode upKey;
        public KeyCode downKey;
        public KeyCode leftKey;
        public KeyCode rightKey;
    };

    public  ControlCode controlCode;
    Animator m_Animator;

    static List<KeyCode> pressedKeyList = new List<KeyCode>();

    void Start() {
        base.Start();
        dataInitial();
        m_rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
    }

    // Update is called once per frame
    void Update() {

        PlayerMove();

        //AniStateRecovery();
    }
    public void dataInitial() {
        m_Animator = GetComponent<Animator>();

    }



    public void PlayerMove() {

           if (KeyPressDect() )
          {

            moveDir = Vector2.zero;
            for (int i = pressedKeyList.Count - 2; i <= pressedKeyList.Count - 1; i++)
            {
               if(i>=0) SetMoveDirByPressedKey(pressedKeyList[i]);
            } 
        }
        if (moveDir.magnitude != 0 && !GameManager.IsTimePause() && !IsAniState("Hurted") && Player.playEnd == 1)
        {
            transform.eulerAngles = new Vector3(0, -45 + Mathf.Atan2(moveDir.y, moveDir.x) * 180 / Mathf.PI, 0);
            m_speed = speed;
        }
        if (Player.playEnd<=0|| pressedKeyList.Count ==0 || GameManager.IsTimePause()||Player.playEnd!=1) m_speed = 0;
        if((Player.playEnd==2)) m_speed = -speed*1.2f;
        if (Player.playEnd ==1) m_Animator.SetFloat("Speed", m_speed);
         m_rigidbody.velocity = transform.forward * m_speed;
    }
    
    public bool KeyPressDect()
    {
        if (!Input.anyKey) pressedKeyList.Clear();
        bool isChanged = false;
        if (Input.GetKeyDown(controlCode.downKey)) { pressedKeyList.Add(controlCode.downKey); isChanged = true; }
        if (Input.GetKeyDown(controlCode.upKey)) {pressedKeyList.Add(controlCode.upKey); isChanged = true; }
        if (Input.GetKeyDown(controlCode.rightKey)){ pressedKeyList.Add(controlCode.rightKey); isChanged = true; }
        if (Input.GetKeyDown(controlCode.leftKey)){ pressedKeyList.Add(controlCode.leftKey); isChanged = true; }
        if (Input.GetKeyUp(controlCode.downKey)){ pressedKeyList.Remove(controlCode.downKey); isChanged = true; }
        if (Input.GetKeyUp(controlCode.upKey)){ pressedKeyList.Remove(controlCode.upKey); isChanged = true; }
        if (Input.GetKeyUp(controlCode.rightKey)){ pressedKeyList.Remove(controlCode.rightKey); isChanged = true; }
        if (Input.GetKeyUp(controlCode.leftKey)){ pressedKeyList.Remove(controlCode.leftKey); isChanged = true; }
        return isChanged;
        }
    public void SetMoveDirByPressedKey(KeyCode key)
    {
        if (key == controlCode.downKey) { moveDir.y = -1; return; }
        if (key == controlCode.upKey) { moveDir.y = 1; return; }
        if (key == controlCode.leftKey) { moveDir.x = 1; return; }
        if (key == controlCode.rightKey) { moveDir.x = -1; return; }
    }
    public bool IsAniState(string str)
    {
       return  m_Animator.GetCurrentAnimatorStateInfo(0).IsName(str);

    }
}
