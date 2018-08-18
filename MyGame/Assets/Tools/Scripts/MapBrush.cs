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


	void clearMap(){
		int childCount = transform.childCount;
		for (int n = 0; n < childCount ; n++) {
			Destroy (transform.GetChild (n).gameObject);
		}
		for (int i = 0; i < map.GetLength (1); i++) {
			for (int j = 0; j < map.GetLength (0); j++) {
				map [i, j] = 0;
			}
		}

	}

	void generateTile(int x,int z){
		Debug.Log ((float)x + " " + (float)z);
		GameObject tile=Instantiate (Tile, new Vector3((float)x,0,(float)z), transform.rotation)as GameObject;
		tile.name = "(" + x.ToString () + "," + z.ToString () + ")";
		tile.transform.parent = this.transform;
	}
	void regenerateTile(){
		for (int i = 0; i < map.GetLength (1); i++) {
			for (int j = 0; j < map.GetLength (0); j++) {
				if (map [i, j] == 1) {
					generateTile (i, j);
				}
			}
		}
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
						if (map [(int)hit.point.x, (int)hit.point.z] == 0) {
							generateTile ((int)hit.point.x, (int)hit.point.z);
							map [(int)hit.point.x, (int)hit.point.z] = 1;
						} else {
							map [(int)hit.point.x, (int)hit.point.z] = 1;
						}

					}

				}
			}
		}
	}


	void OnGUI(){
		if (GUI.Button(new Rect(20,20,50,20),"保存")) {
			FileStream fs = new FileStream(Application.dataPath+"/Resource/Maps/"+"Map.map", FileMode.OpenOrCreate);


			for (int i = 0; i < map.GetLength (1); i++) {
				string mapLine = "";
				for (int j = 0; j < map.GetLength (0); j++) {
					mapLine += map [i,j].ToString();
					mapLine += ",";
				}
				mapLine += "\n";
				//逐行写入
				byte[] data = System.Text.Encoding.Default.GetBytes (mapLine); 
				fs.Write(data, 0, data.Length);
			}

			fs.Flush();
			fs.Close();


		}
		if (GUI.Button(new Rect(80,20,50,20),"读取")) {


			clearMap ();
			StreamReader sr = new StreamReader(Application.dataPath+"/Resource/Maps/"+"/Map.map");
			string line;
			int i = 0;
			while ((line = sr.ReadLine()) != null) 
			{
				string[] nums = line.ToString ().Split (',');

				for (int j = 0; j < nums.Length-2; j++) {
					map [i,j] = int.Parse(nums [j]);

					Debug.Log ((nums.Length-1)+","+i+","+j);
				}
				i++;
			}

			regenerateTile ();
		}
		if (GUI.Button (new Rect (140, 20, 50, 20), "清空")) {

			clearMap ();
		}

	}


}
