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
        GameManager.itemInScene.Add(this);
	}
	
	// Update is called once per frame
	void Update () {
        destoryTime -= Time.deltaTime;
        if (destoryTime <= 2f&& destoryTime>0)GetComponentInChildren<Renderer>().enabled = !GetComponentInChildren<Renderer>().enabled;
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
    private void OnDestroy()
    {
        GameManager.itemInScene.Remove(this);
    }
    public ItemInSceneData GetItemInSceneData()
    {
        ItemInSceneData itemInSceneData = new ItemInSceneData();
        itemInSceneData.type = type;
        itemInSceneData.num = num;
        itemInSceneData.time = destoryTime;
        itemInSceneData.itemInScenePos[0] = transform.position.x;
        itemInSceneData.itemInScenePos[1] = transform.position.y;
        itemInSceneData.itemInScenePos[2] = transform.position.z;
        return itemInSceneData;
    }

}
