using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemForGet : MonoBehaviour {

    // Use this for initialization
        public enum Type1
        {
            bullet,
            boom
        }
        public Type1 type;
        public int num = 1;
    public bool playEffect=false;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
     void OnTriggerEnter(Collider other)
    {
        playEffect = true;
        if (other.tag == "Player")
        {
            if (type == Type1.boom) { other.GetComponentInChildren<Luanch>().getBoom(num); Destroy(gameObject); }
            if (type == Type1.bullet) { other.GetComponentInChildren<Luanch>().getBullet(num); Destroy(gameObject); }
        }
    }
}
