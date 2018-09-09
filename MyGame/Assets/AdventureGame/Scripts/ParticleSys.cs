using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSys : MonoBehaviour {

    // Use this for initialization
    public Transform m_target;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GetComponent<ParticleSystem>().isStopped) Destroy(gameObject);
        if(m_target != null) transform.position = m_target.position;
    }
    public void SetFollowingTarget(Transform target) {
        m_target = target;

    }
}
