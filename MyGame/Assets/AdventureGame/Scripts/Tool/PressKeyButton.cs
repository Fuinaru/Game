using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
 public abstract class PressKeyButton : MonoBehaviour 
{
    public KeyCode key = KeyCode.E;
    public bool pressKey = true;

    private void Start()
    {

    }
    private void OnTriggerStay(Collider other)
    {
        if (pressKey)
        {
            if (Input.GetKeyDown(key) && other.tag == "Player" && !GameManager.IsTimePause()) InterAction();
        }
        else InterAction();

    }
    protected virtual void InterAction() {

    }
}
