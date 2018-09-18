using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SaveData {
    public int stageName = 1;

    public int MaxHp = 0;
    public int Hp = 0;
    public int MaxMp = 0;
    public int Mp = 0;
    public int coin=0;
   public float[] playerPos =new float[3];

    public List<ItemData> itemDatas = new List<ItemData>();
    public List<MonsterData> monsterData = new List<MonsterData>();
    public List<ItemInSceneData> itemInSceneData = new List<ItemInSceneData>();
}
[System.Serializable]
public class MonsterData
{
    public int monsterNum = 0;
    public int Hp = 0;
    public float[] monsterPos = new float[3];

}
[System.Serializable]
public class ItemInSceneData
{
    public Var.ItemType type;
    public int num = 1;
    public float time = 1;
    public float[] itemInScenePos = new float[3];

}