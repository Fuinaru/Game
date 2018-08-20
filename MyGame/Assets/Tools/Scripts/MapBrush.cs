using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class MapBrush : MonoBehaviour {
	public Camera camera;
	public GameObject tile;
	int tileNum=1;
	public List<GameObject> tiles;
	int[,] map = new int[20,20];
	// Use this for initialization
	void Awake () {
		//初始化地图数组
		for (int i = 0; i < map.GetLength (0); i++) {
			for (int j = 0; j < map.GetLength (1); j++) {
				map [i, j] = 0;
			}
		}
		//初始化tiles

		tiles = new List<GameObject> ();
		//tiles.Clear();
		string fullPath = Application.dataPath+"/Resource/Maps/Tiles/";  

		//获取指定路径下面的所有资源文件  
		if (Directory.Exists(fullPath)){  
			DirectoryInfo direction = new DirectoryInfo(fullPath);  
			FileInfo[] files = direction.GetFiles("*",SearchOption.AllDirectories);  

			Debug.Log(files.Length);  

			for(int i=0;i<files.Length;i++){  
				if (files[i].Name.EndsWith(".meta")){  
					continue;  
				}  
				//Debug.Log( "Name:" + files[i].Name );  
				Debug.Log( "FullName:" + files[i].FullName );  
				//Debug.Log( "DirectoryName:" + files[i].DirectoryName );  
				string dir=files[i].FullName.Substring (files[i].FullName.IndexOf ("Assets"));
				GameObject obj=AssetDatabase.LoadMainAssetAtPath(dir)as GameObject;
				Debug.Log ("!!!");
				tiles.Add(obj);
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

	void generateTile(int x,int z,int num){
		Debug.Log ((float)x + " " + (float)z);
		GameObject obj=Instantiate (tiles[num-1], new Vector3((float)x,0,(float)z), transform.rotation)as GameObject;
		obj.name = "(" + x.ToString () + "," + z.ToString () + ")";
		obj.transform.parent = this.transform;
	}

	void regenerateTile(){
		for (int i = 0; i < map.GetLength (1); i++) {
			for (int j = 0; j < map.GetLength (0); j++) {
				if (map [i, j] != 0) {
					generateTile (i, j,map [i, j]);
				}
			}
		}
	}

	// Update is called once per frame
	void Update () {

		//平移
		if (Input.GetKey (KeyCode.Mouse2)) {
			camera.transform.position += new Vector3 (-Input.GetAxis ("Mouse X")*0.6f, 0, -Input.GetAxis ("Mouse Y")*0.6f);
		}
		//缩放
		if (Mathf.Abs(Input.GetAxis ("Mouse ScrollWheel"))>0.1) {
			camera.transform.position += new Vector3 (0, Input.GetAxis ("Mouse ScrollWheel")*2f, 0);
		}


		//生成
		if (Input.GetKey(KeyCode.Mouse0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			int layerMask = 1 << 8;
			if (Physics.Raycast (ray, out hit,100,layerMask)) {
				Debug.DrawLine (ray.origin, hit.point, Color.green);
				if ((int)hit.point.x < map.GetLength (0) && (int)hit.point.z < map.GetLength (1)) {
					if (map [(int)hit.point.x, (int)hit.point.z] == 0) {
						generateTile ((int)hit.point.x, (int)hit.point.z,tileNum);
						map [(int)hit.point.x, (int)hit.point.z] = tileNum;
					} else {
						Destroy(this.transform.Find ("(" + ((int)hit.point.x).ToString () 
							+ "," + ((int)hit.point.z).ToString () + ")").gameObject);
						generateTile ((int)hit.point.x, (int)hit.point.z,tileNum);
						map [(int)hit.point.x, (int)hit.point.z] = tileNum;
					}

				}
			}
		}

		//删除
		if (Input.GetKey(KeyCode.Mouse1)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			int layerMask = 1 << 8;
			if (Physics.Raycast (ray, out hit,100,layerMask)) {
				Debug.DrawLine (ray.origin, hit.point, Color.red);
				if ((int)hit.point.x < map.GetLength (0) && (int)hit.point.z < map.GetLength (1)) {
					Destroy(this.transform.Find ("(" + ((int)hit.point.x).ToString () 
						+ "," + ((int)hit.point.z).ToString () + ")").gameObject);
					map [(int)hit.point.x, (int)hit.point.z] = 0;

				}
			}
		}



	}


	void OnGUI(){
		if (tiles == null) {

			tiles = new List<GameObject> ();
		}
		if (GUILayout.Button("保存",GUILayout.Width(40))) {
			FileStream fs = new FileStream(Application.dataPath+"/Resource/Maps/"+"Map.map", FileMode.OpenOrCreate);


			for (int i = 0; i < map.GetLength (1); i++) {
				string mapLine = "";
				for (int j = 0; j < map.GetLength (0); j++) {
					mapLine += map [i,j].ToString();
					mapLine += ",";
				}
				mapLine += "\r\n";
				//逐行写入
				byte[] data = System.Text.Encoding.Default.GetBytes (mapLine); 
				fs.Write(data, 0, data.Length);
			}

			fs.Flush();
			fs.Close();


		}
		if (GUILayout.Button("读取",GUILayout.Width(40))) {


			clearMap ();
			StreamReader sr = new StreamReader(Application.dataPath+"/Resource/Maps/"+"/Map.map");
			string line;
			int i = 0;
			while ((line = sr.ReadLine()) != null) 
			{
				string[] nums = line.ToString ().Split (',');

				for (int j = 0; j < nums.Length-1; j++) {
					map [i,j] = int.Parse(nums [j]);

					Debug.Log ((nums.Length-1)+","+i+","+j);
				}
				i++;
			}

			regenerateTile ();
		}
		if (GUILayout.Button ("清空",GUILayout.Width(40))) {
			clearMap ();
		}


		GUILayout.Label ("当前:"+tile.name);
		for (int i = 0; i < tiles.Count; i++) {
			if (GUILayout.Button (tiles[i].name, GUILayout.Width (80))) {
				tileNum = i+1;
				tile = tiles [i];

			}
		}

	}


}
