using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
public class ItemForGet : MonoBehaviour {

    // Use this for initialization
      
        public Var.ItemType type;
        public int num = 1;
    public float destoryTime=30f;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        destoryTime -= Time.deltaTime;
        if (destoryTime <= 0) Destroy(gameObject);
	}
     void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (type==Var.ItemType.Coin) { GameManager.player.GetCoin(num); }
             else other.GetComponentInChildren<PlayerLaunch>().getItem(type, num);

            Destroy(gameObject); 
        }
    }
}
