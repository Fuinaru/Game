using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class MapBrush : MonoBehaviour {
	public GameObject brushObj;
	public GameObject mapDataObj;
	public GameObject mapBaseObj;
	public Camera camera;
	public GameObject tile;
	int tileNum=1;
	public List<GameObject> tiles;
	public int mapHeight = 20;
	public int mapWidth = 20;
	int[,] map;
	// Use this for initialization
	void Awake () {
		//初始化地图数组
		map= new int[mapWidth,mapHeight];
		mapBaseObj.transform.localScale = new Vector3 (mapWidth/10f, 1, mapHeight/10f);
		for (int i = 0; i < mapWidth; i++) {
			for (int j = 0; j < mapHeight; j++) {
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

		//更新笔刷预览
		refreshBrushPreview(tileNum-1);

	}


	void clearChild(Transform tran){

		int childCount = tran.childCount;
		for (int i = 0; i < childCount ; i++) {
			Destroy (tran.GetChild (i).gameObject);
		}

	}


	void clearMap(){
		clearChild (mapDataObj.transform);
		for (int i = 0; i < mapWidth; i++) {
			for (int j = 0; j < mapHeight; j++) {
				map [i, j] = 0;
			}
		}

	}

	void generateTile(int x,int z,int num){
		Debug.Log ((float)x + " " + (float)z);
		GameObject obj=Instantiate (tiles[num-1], new Vector3((float)x,0,(float)z), mapDataObj.transform.rotation)as GameObject;
		obj.name = "(" + x.ToString () + "," + z.ToString () + ")";
		obj.transform.parent = mapDataObj.transform;
	}

	void regenerateTile(){
		for (int i = 0; i < mapWidth; i++) {
			for (int j = 0; j < mapHeight; j++) {
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
				if ((int)hit.point.x < mapWidth && (int)hit.point.z < mapHeight) {
					Debug.Log ((int)hit.point.x + "," + (int)hit.point.z);
					if (map [(int)hit.point.x, (int)hit.point.z] == 0) {
						generateTile ((int)hit.point.x, (int)hit.point.z,tileNum);
						map [(int)hit.point.x, (int)hit.point.z] = tileNum;
					} else {
						Destroy(mapDataObj.transform.Find ("(" + ((int)hit.point.x).ToString () 
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
				if ((int)hit.point.x < mapHeight && (int)hit.point.z < mapWidth) {
					if (map [(int)hit.point.x, (int)hit.point.z] != 0) {
						Destroy (mapDataObj.transform.Find ("(" + ((int)hit.point.x).ToString ()
						+ "," + ((int)hit.point.z).ToString () + ")").gameObject);
						map [(int)hit.point.x, (int)hit.point.z] = 0;
					}
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


			for (int i = 0; i < mapWidth; i++) {
				string mapLine = "";
				for (int j = 0; j < mapHeight; j++) {
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
				changeBrush (i);



			}
		}

	}

	//更新笔刷预览
	void refreshBrushPreview(int i){
		//清空笔刷预览
		clearChild(brushObj.transform);
		//笔刷加载预览
		GameObject obj=Instantiate(tiles[i],brushObj.transform.position,brushObj.transform.rotation);
		obj.transform.parent = brushObj.transform;
		//笔刷物体层级
		obj.layer = 9;
		//子孙所有层级为9
		Transform[] sons;
		sons = obj.GetComponentsInChildren<Transform> ();
		foreach (Transform son in sons) {
			son.gameObject.layer = 9;
		}

	}


	//更换笔刷
	void changeBrush(int i){

		tileNum = i+1;
		tile = tiles [i];

		refreshBrushPreview (i);


	}

}
