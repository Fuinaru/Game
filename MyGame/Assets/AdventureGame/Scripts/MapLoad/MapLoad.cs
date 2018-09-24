using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapLoad : MonoBehaviour {
    public static MapLoad mapload;

    public int width = 5;
    public int height = 5;
    public float mapSize = 20;
    public bool isLoadAllMaps = false;
    private List<MapData> maps = new List<MapData>();
    private List<MapInSceneData> mapInScene = new List<MapInSceneData>();
    private int length;
    public GameObject map;

    public bool mapIsLoad = false;
    // Use this for initialization
    void Awake (){
        mapload = this;
    }
    void Start () {
        MapDataInitial();
   
    if(isLoadAllMaps)LoadAllMapIntoScene();
	
    }
	
	// Update is called once per frame
	void Update () {
        if (!isLoadAllMaps)
        {
            LoadMapsIntoSceneByPlayerPos();
            UnloadMapByPlayerPos();
        }
    }

    private void OnGUI()
    {
        GUI.skin.label.richText = true;
        GUI.Label(new Rect(10, 10, 50, 50), "<color=red>"+Player.playEnd+"</color>");

    }

    void MapDataInitial() {

            for (int i = 0; i < width*height; i++)
            {
                maps.Add(new MapData(maps.Count));
            }

        length = maps.Count;
    }
  private void  LoadAllMapIntoScene() {
        foreach (MapData o in maps) {
            LoadMapIntoScene(o);
        }


    }
    void LoadMapIntoScene(MapData o)
    {
        if (!o.isInScene)
        {
			GameObject go = Instantiate(o.GetMapResource(GameManager.stageName)) as GameObject;
            go.transform.SetParent(map.transform);
            go.transform.position = MapWorldPosition(o);
            o.isInScene = true;
            go.name = "MapEditor " + o.mapNum;
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
        mapIsLoad = true;
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
    void UnloadALLMapInScene()
    {
        for (int i = mapInScene.Count - 1; i >= 0; i--)
        {
            UnloadMap(mapInScene[i]); 
        }
        mapInScene.Clear();
    }

    public void LoadNextStage(int stageName,int _width,int _height) {
        GameManager.stageName = stageName;
        width = _width;height = _height;
        UnloadALLMapInScene();
        maps.Clear();
        MapDataInitial();
        mapIsLoad = false;
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
        pos.x = MapPos(o).y * mapSize;
        pos.y = 0;
        pos.z = MapPos(o).x * mapSize;
        return pos;
    }
    Vector2Int PlayerPos() {
        Vector2Int pos = Vector2Int.zero;
        pos.x= (int)(GameManager.player.transform.position.z / mapSize);
        pos.y= (int)(GameManager.player.transform.position.x / mapSize);
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
        List<int> nums = new List<int>();

        Vector2Int pos = PlayerPos();
        int num = PlayerPosNum();
        if (pos.x < 0 || pos.x >= height || pos.y < 0 || pos.y >= width) return nums;
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
        return (GameObject)Resources.Load("MapBlocks/stage1/" + "MapEditor " + mapNum.ToString());
    }
    public GameObject GetMapResource(int stageName)
    {
        return (GameObject)Resources.Load("MapBlocks/"+stageName+"/" + "MapEditor " + mapNum.ToString());
    }
}

public class MapInSceneData :MapData{
	public GameObject mapObject;
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