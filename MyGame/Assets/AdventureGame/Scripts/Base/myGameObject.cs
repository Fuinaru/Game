using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameObject : MonoBehaviour {
    // Use this for initialization
    public Rigidbody m_rigidbody;
    protected void Start () {
        if (GetComponent<Rigidbody>() != null) m_rigidbody = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    protected void Update () {

    }

}
