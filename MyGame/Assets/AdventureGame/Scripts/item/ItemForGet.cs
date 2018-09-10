using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
public class ItemForGet : MonoBehaviour {

    // Use this for initialization
      
        public Var.ItemType type;
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
             other.GetComponentInChildren<PlayerLaunch>().getItem(type, num); Destroy(gameObject); 
            Flowchart.BroadcastFungusMessage("itemGet");
        }
    }
}
