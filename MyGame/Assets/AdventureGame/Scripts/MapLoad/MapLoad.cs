﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class MapLoad : MonoBehaviour {
    public int width = 4;
    public int height = 4;
    public List<MapData> maps=new List<MapData>();
	public List<MapInSceneData> mapInScene = new List<MapInSceneData>();
    public int length;

    // Use this for initialization
    void Start () {
        mapInitial();
        length = maps.Count;
       //LoadAllMapIntoScene();
	
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F1)) {

           

            }
   
         LoadMapsIntoSceneByPlayerPos();
        UnloadMapByPlayerPos();
        //    UnloadMapByPlayerPos();
    }


    void mapInitial() {
		string fullPath = "Assets/Resources/MapBlocks" + "/";  //路径

        //获取指定路径下面的所有资源文件  
        if (Directory.Exists(fullPath))
        {
            DirectoryInfo direction = new DirectoryInfo(fullPath);
            FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);

            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Name.EndsWith(".meta"))
                {
                    continue;
                }

                maps.Add(new MapData(maps.Count));
            }
        }

    }
    void LoadMapIntoScene(MapData o)
    {
        if (!o.isInScene)
        {
			GameObject go = Instantiate(o.GetMapResource()) as GameObject;
            go.transform.position = MapWorldPosition(o);
            o.isInScene = true;
			MapInSceneData i = new MapInSceneData(go, o.mapNum, true);
            mapInScene.Add(i);
        }
    }
	void UnloadMap(MapInSceneData o)
    {
        if (o.isInScene)
        {
            maps[o.mapNum].isInScene = false;
            Destroy(o.mapObject);
            mapInScene.Remove(o);
        }
     //   Debug.Log(maps.IndexOf(o) + "/" + maps.IndexOf(o));
    }
//    void LoadAllMapIntoScene() {
//        foreach (MapData o in maps) {
//         
//            LoadMapIntoScene(o);
//        }
//    }

    void LoadMapsIntoSceneByPlayerPos() {
   
        foreach (int n in NearByPlayerNum()) {

            LoadMapIntoScene(maps[n]);
        }

    }

    void UnloadMapByPlayerPos()
    {
        bool same = false;
        for (int i = mapInScene.Count - 1; i >= 0; i--)
            {
            foreach (int n in NearByPlayerNum())
            {

                if (mapInScene[i].mapNum == n) {  same = true; break; }
            }
            if (!same) { UnloadMap(mapInScene[i]);  }
            same = false;
        }

    }
   
        Vector2Int MapPos(MapData o)
    {
        int n = maps.IndexOf(o);
        Vector2Int pos=Vector2Int.zero;
        pos.x = n / width;
        pos.y = n - pos.x * width;
   
        return pos;
    }

    Vector3 MapWorldPosition(MapData o) {
        Vector3 pos;
        pos.x = MapPos(o).y * 10;
        pos.y = 0;
        pos.z = MapPos(o).x * 10;
        return pos;
    }
    Vector2Int PlayerPos() {
        Vector2Int pos = Vector2Int.zero;
        pos.x= (int)GameManager.player.transform.position.z / 10 ;
        pos.y= (int)GameManager.player.transform.position.x / 10;
        return pos;

    }
    int PlayerPosNum() {
        return Pos2Num(PlayerPos());
    }
    int Pos2Num(Vector2Int pos) {
        int num = 0;
        num += pos .x * width;
        num += pos.y;
        return num;
    }

    List<int> NearByPlayerNum() {
        List < int > nums= new List<int>();

        Vector2Int pos = PlayerPos();
        int num = PlayerPosNum();
        nums.Add(num);
        if (pos.y - 1 >= 0) nums.Add(num - 1);
        if (pos.y + 1 < width) nums.Add(num + 1);
        if (pos.x + 1 < height) nums.Add(num + width);
        if (pos.x - 1 >= 0) nums.Add(num - width);
        if (pos.x + 1 < height && pos.y - 1 >= 0) nums.Add(num + width - 1);
        if (pos.x + 1 < height && pos.y + 1 < width) nums.Add(num + width + 1);
        if (pos.x - 1 >= 0 && pos.y - 1 >= 0) nums.Add(num - width - 1);
        if (pos.x - 1 >= 0 && pos.y + 1 < width) nums.Add(num - width + 1);

        return nums;
    }
}
public class MapData {
    public int mapNum;
    public bool isInScene;
	public MapData(){
	}
    public MapData(int m_mapNum) {
        mapNum = m_mapNum;
        isInScene = false;
    }
    public MapData(int m_mapNum,bool isIn)
    {

        mapNum = m_mapNum;
        isInScene = isIn;
    }

	public GameObject GetMapResource(){
		Debug.Log (Application.dataPath+"/AdventureGame/Lib/Map/"+"MapEditor "+mapNum.ToString());
		GameObject obj = (GameObject)Resources.Load ("MapBlocks/" + "MapEditor " + mapNum.ToString ());
		return obj;
	}

}

public class MapInSceneData :MapData{
	public GameObject mapObject;
	public int mapNum;
	public bool isInScene;
	public MapInSceneData(GameObject m_map, int m_mapNum) {
		mapObject = m_map;
		mapNum = m_mapNum;
		isInScene = false;
	}
	public MapInSceneData(GameObject m_map, int m_mapNum,bool isIn)
	{
		mapObject = m_map;
		mapNum = m_mapNum;
		isInScene = isIn;
	}

}