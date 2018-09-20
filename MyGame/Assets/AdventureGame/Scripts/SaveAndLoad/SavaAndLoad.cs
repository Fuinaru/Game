using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SavaAndLoad : MonoBehaviour {


    public static SavaAndLoad saveload;
	// Use this for initialization
	void Start () {
        saveload = this;

    }
	
	// Update is called once per frame
	void Update () {

    }

    public void SaveGame()
    {
        // 1创建了一个 SaveData 对象，同时当前游戏的所有数据都会保存到这个对象中。
        SaveData saveData = CreateSaveGameObject();

        // 2创建了一个 BinaryFormatter，然后创建一个 FileStream，在创建时指定文件路径和要保存的 Save 对象。它会序列化数据（转换成字节），然后写磁盘，关闭 FileStream。现在在电脑上会多一个名为 gamesave.save 的文件。.save 后缀只是一个示例，你可以使用任意扩展名
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, saveData);
        file.Close();


        Debug.Log("Game Saved");
    }

    public void LoadGame()
    {
        // 1
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {

            GameManager.isLoading = true;
                        // 2
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            SaveData saveData = (SaveData)bf.Deserialize(file);
            file.Close();

            // 3
        
          
           // GameManager.bagSys.SpaceInitial();
            StartCoroutine(DestroyItem(saveData));
            StartCoroutine(DestroyMonster(saveData));
            StartCoroutine(DestroyItemInScene(saveData));
            GameManager.stageName = saveData.stageName ;
            MapLoad.mapload.LoadNextStage(saveData.stageName, 5, 5);
            GameManager.player.maxHp= saveData.MaxHp ;
            GameManager.player.hp = saveData.Hp ;
           GameManager.player.maxMp = saveData.MaxMp ;
           GameManager.player.mp = saveData.Mp ;
          GameManager.player.coinNum =saveData.coin ;
            GameManager.player.transform.position=new Vector3( saveData.playerPos[0], saveData.playerPos[1], saveData.playerPos[2]) ;

            GameManager.player.UpDateAllUI();

            Debug.Log("Game Loaded");

            // Unpause();
        }
        else
        {
            Debug.Log("No game saved!");
        }
    }

    IEnumerator DestroyItem(SaveData saveData)
    {
        yield return new WaitUntil(() => GameManager.bagSys.SpaceClear());

        BagSystem.bagItems.Clear();
        for (int i = 0; i < saveData.itemDatas.Count; i++)
        {
            GameManager.bagSys.AddItemAtNum(saveData.itemDatas[i]);
        }
    }
    IEnumerator DestroyMonster(SaveData saveData)
    { 

        yield return new WaitUntil(() => GameManager.GM.DestoryAllMonsters());

        Debug.Log(saveData.monsterData.Count);
        for (int i = 0; i < saveData.monsterData.Count; i++)
        {
            GameObject o =Instantiate(Tools.GetMonsterByNum(saveData.monsterData[i].monsterNum));
            o.GetComponent<BaseMonster>().hp = saveData.monsterData[i].Hp;
            o.transform.position = new Vector3(saveData.monsterData[i].monsterPos[0], saveData.monsterData[i].monsterPos[1], saveData.monsterData[i].monsterPos[2]);
            o.transform.SetParent(GameManager.GM.monAndItemInScene.GetChild(0));
        }
        GameManager.isLoading = false;
    }
    IEnumerator DestroyItemInScene(SaveData saveData)
    {

        yield return new WaitUntil(() => GameManager.GM.DestoryAllItemsInScene());

        Debug.Log(saveData.itemInSceneData.Count);
        for (int i = 0; i < saveData.itemInSceneData.Count; i++)
        {
            GameObject o = Instantiate(Tools.GetItemInSceneByType(saveData.itemInSceneData[i].type));
            o.GetComponent<ItemForGet>().num = saveData.itemInSceneData[i].num;
            o.GetComponent<ItemForGet>().type = saveData.itemInSceneData[i].type;
            o.GetComponent<ItemForGet>().destoryTime = saveData.itemInSceneData[i].time;
            o.transform.position = new Vector3(saveData.itemInSceneData[i].itemInScenePos[0], saveData.itemInSceneData[i].itemInScenePos[1], saveData.itemInSceneData[i].itemInScenePos[2]);
            o.transform.SetParent(GameManager.GM.monAndItemInScene.GetChild(1));
        
        }
    }


    private SaveData CreateSaveGameObject()
    {
        SaveData saveData = new SaveData();
        foreach (ItemData o in BagSystem.bagItems) {
            saveData.itemDatas.Add(o);

        }

        for(int i= GameManager.Monsters.Count-1; i>=0;i--)
        {
            saveData.monsterData.Add(GameManager.Monsters[i].GetMonsterData());
        }
        for (int i = GameManager.itemInScene.Count - 1; i >= 0; i--)
        {
            saveData.itemInSceneData.Add(GameManager.itemInScene[i].GetItemInSceneData());
        }
       

        saveData.stageName = GameManager.stageName;

        saveData.MaxHp = GameManager.player.maxHp;
        saveData.Hp = GameManager.player.hp;
        saveData.MaxMp = GameManager.player.maxMp;
        saveData.Mp = GameManager.player.mp;
        saveData.coin = GameManager.player.coinNum;
        saveData.playerPos[0] = GameManager.player.transform.position.x;
        saveData.playerPos[1] = GameManager.player.transform.position.y;
        saveData.playerPos[2] = GameManager.player.transform.position.z;
        return saveData;
    }

}






