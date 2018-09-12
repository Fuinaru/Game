using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnLoadResources : MonoBehaviour {

    // Use this for initialization
    float time = 0;
    public float m_time =60;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time >= m_time)
        {
            Resources.UnloadUnusedAssets();
            time = 0;
        }
    }
}
