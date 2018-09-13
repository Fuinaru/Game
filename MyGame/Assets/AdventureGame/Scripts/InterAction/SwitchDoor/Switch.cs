using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  Fungus;
public class Switch : MonoBehaviour
{
    public GameObject door;
	// Use this for initialization
	void Start () {
       

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            door.GetComponent<door>().performance();
            Flowchart.BroadcastFungusMessage("openDoor");
        }
    }
}
