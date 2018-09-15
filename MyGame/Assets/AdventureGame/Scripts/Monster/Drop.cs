using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour {
   public List<GameObject> dropItem;
	// Use this for initialization
	void Start () {

      //  dropItem.Reverse();
    }
	
	// Update is called once per frame
	void Update () {
   

	}
   
    private void OnDestroy()
    {
     if (transform.parent.gameObject.activeInHierarchy&&!GameManager.isLoading) { 
            GameObject o = Instantiate(DropItemCaculate(), new Vector3(transform.position.x, 0.5f, transform.position.z), transform.rotation);
        o.transform.SetParent(transform.parent);
    }
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
