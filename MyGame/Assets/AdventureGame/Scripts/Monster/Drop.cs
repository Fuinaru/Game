using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour {
   public List<GameObject> dropItem;
    private bool isQuit = false;
	// Use this for initialization
	void Start () {
        isQuit = false;
      //  dropItem.Reverse();
    }
	
	// Update is called once per frame
	void Update () {
   

	}
   
    private void OnDestroy()
    {
        if (transform.parent.gameObject.activeInHierarchy) { 
        GameObject o = Instantiate(DropItemCaculate(), new Vector3(transform.position.x, 0.5f, transform.position.z), transform.rotation);
        o.transform.SetParent(transform.parent);
    }
    }
    private void OnApplicationQuit()
    {
        isQuit = true;
    }
    GameObject DropItemCaculate() {
        float n=0;
        for (int i = 1; i <= dropItem.Count; i++) {
            n += i;
        }
        float random = Random.Range(0, n);
        for (int i = 1; i <=dropItem.Count; i++) {
            if (random < i) { return dropItem[dropItem.Count-i]; }
            else random -= i;
        }
        return dropItem[dropItem.Count - 1];

    }

}
