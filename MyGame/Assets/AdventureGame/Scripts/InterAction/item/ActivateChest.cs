using UnityEngine;
using System.Collections;

public class ActivateChest : Drop
{

	public Transform lid, lidOpen, lidClose;	// Lid, Lid open rotation, Lid close rotation
	public float openSpeed = 5F;				// Opening speed
	public bool canClose;						// Can the chest be closed
	
	[HideInInspector]
	public bool _open;							// Is the chest opened

	void Update () {
		if(_open){
			ChestClicked(lidOpen.rotation);

		}
		else{
			ChestClicked(lidClose.rotation);
		}
	}
	
	// Rotate the lid to the requested rotation
	void ChestClicked(Quaternion toRot){
        if (lid.rotation != toRot)
        {
            lid.rotation = Quaternion.Lerp(lid.rotation, toRot, Time.deltaTime * openSpeed);
        }
        else if(_open)Destroy(gameObject);
	}
	
	//void OnMouseDown(){
	//	if(canClose) _open = !_open; else _open = true;
	//}
    private void OnTriggerStay(Collider other)
    {
       
        if (Input.GetKeyDown(KeyCode.E)&&other.tag == "Player")
        {
           
            if (canClose) _open = !_open; else _open = true;
            Tools.PlayFollowingParticletByName("SmokeEffect", transform);
            Debug.Log(_open);
        }
   
    }

}
