using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public static void LookAt(Transform m,Vector3 dir,float speed) {
        if (m.forward.normalized.Equals(dir.normalized)) return ;
        Debug.Log(m.forward.normalized+"+"+ dir.normalized);
      //  m.localEulerAngles = Vector3.Lerp(m.forward, dir, speed * Time.deltaTime);
        m.localEulerAngles = dir;

    }

}
