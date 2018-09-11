using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour {
   public List<string> dropItem;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Mouse0)) { Debug.Log(DropItemCaculate()); }

	}
    string DropItemCaculate() {
        float n = dropItem.Count;
        n=Mathf.FloorToInt( (Random.Range(-n, n) / Random.Range(1, 2 * n)));
        if (n < 0) return "nothing";
        return dropItem[(int)n];
    }

}
