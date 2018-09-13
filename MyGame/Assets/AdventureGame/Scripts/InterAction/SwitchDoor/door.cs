using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    Animator m_Animator;
    // Use this for initialization
    void Start () {
        m_Animator= GetComponent<Animator>();
        m_Animator.Play("stop");
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Keypad3)) {

            m_Animator.Play("down");
        }
	}
   public void  performance() {
        m_Animator.Play("down");
    }
}
