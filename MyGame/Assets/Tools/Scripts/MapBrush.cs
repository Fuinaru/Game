using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
[ExecuteInEditMode]
public class MapBrush : MonoBehaviour {
	public GameObject Tile;
	int[,] map = new int[20,20];
	// Use this for initialization
	void Start () {
		//初始化地图数组
		for (int i = 0; i < map.GetLength (0); i++) {
			for (int j = 0; j < map.GetLength (1); j++) {
				map [i, j] = 0;
			}
		}
	}

	void generateTile(int x,int z){
		Debug.Log ((float)x + " " + (float)z);
		GameObject tile=Instantiate (Tile, transform.position+new Vector3((float)x,0,(float)z), transform.rotation)as GameObject;
		tile.name = "(" + x.ToString () + "," + z.ToString () + ")";
		tile.transform.parent = this.transform;
	}


	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Mouse0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				if (hit.transform.tag == "Ground") {
					Debug.DrawLine (ray.origin, hit.point, Color.red);
					if ((int)hit.point.x < map.GetLength (0) && (int)hit.point.z < map.GetLength (1)) {
						if (map [(int)hit.point.x+10, (int)hit.point.z+10] == 0) {
							generateTile ((int)hit.point.x, (int)hit.point.z);
							map [(int)hit.point.x+10, (int)hit.point.z+10] = 1;
						} else {
							map [(int)hit.point.x+10, (int)hit.point.z+10] = 1;
						}

					}

				}
			}
		}
	}


	void OnGUI(){
		if (GUI.Button(new Rect(20,20,50,20),"保存")) {



		}
		if (GUI.Button(new Rect(80,20,50,20),"读取")) {



		}


	}


}
