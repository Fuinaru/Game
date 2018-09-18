using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayEndDestory : MonoBehaviour {

    // Use this for initialization
    public Transform m_target;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        try { if (GetComponent<ParticleSystem>().isStopped) Destroy(gameObject); } catch { }
        try { if (!GetComponent<AudioSource>().isPlaying) Destroy(gameObject); } catch { }
        if (m_target != null) transform.position = m_target.position;
    }
    public void SetFollowingTarget(Transform target) {
        m_target = target;

    }
}
