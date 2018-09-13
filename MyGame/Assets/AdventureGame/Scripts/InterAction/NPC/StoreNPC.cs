using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreNPC : PressKeyButton {
    public List<Var.ItemType> goodsList;
    public List<int> goodsPrice;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected override void InterAction()
    {
      
            GameManager.store.transform.parent.gameObject.SetActive(true);
            GameManager.store.NPCSetData(goodsList, goodsPrice); }



}
