using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreNPC : MonoBehaviour {
    public List<Var.ItemType> goodsList;
    public List<int> goodsPrice;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E)&&other.tag=="Player")
        {
            GameManager.store.transform.parent.gameObject.SetActive(true);
            GameManager.store.NPCSetData(goodsList, goodsPrice);
        }
    }

}
