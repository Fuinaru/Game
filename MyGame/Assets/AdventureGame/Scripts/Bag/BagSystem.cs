using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class BagSystem : MonoBehaviour {
    public int width = 5;
    public int height = 5;
    public GameObject itemSpace;
    public GameObject bagItem;
    private Vector2 spaceSzie;
    public GameObject background;
private Vector2 backgroundSize;
    public static int num;
   public static List<GameObject>  bag = new List<GameObject>();
    public static List<ItemData> bagItems = new List<ItemData>();
    public  GameObject equipOne;
    public GameObject equipTwo;

    // Use this for initialization
    void Start() {

        GetSpaceSize();
        SetBackgroundSize();
        SpaceInitial();

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha2)) PrintBag();
        num = GetItemNum();
   
    }
    void GetSpaceSize()
    {
        spaceSzie.x = itemSpace.GetComponent<RectTransform>().sizeDelta.x;
        spaceSzie.y = itemSpace.GetComponent<RectTransform>().sizeDelta.y;
    }
    void SetBackgroundSize()
    {
        backgroundSize.x = width * spaceSzie.x;
        backgroundSize.y = height * spaceSzie.y;
        background.GetComponent<RectTransform>().sizeDelta = backgroundSize;
    }
    void SpaceInitial() {
        bag.Add(equipOne);
        bag.Add(equipTwo);
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {

                Vector3 pos;
                pos.x = background.transform.position.x + j * spaceSzie.x;
                pos.y = background.transform.position.y - i * spaceSzie.y;
                pos.z = background.transform.position.z;
                GameObject go = Instantiate(itemSpace, pos, background.transform.rotation) as GameObject;
                go.transform.SetParent(background.transform);
                bag.Add(go);
            }
        }
    }
    public  void AddItem(MyGameVariable.ItemType type,int num) {
        foreach (ItemData o in bagItems)
        {
            if (o.itemType.Equals(type))
            {
                o.itemNum += num;
                o.numChanged = true;
                return;
             }
        }

        foreach (GameObject o in bag)
        {
            if (o.transform.childCount == 0)       
            {
                Type classType = Tools.ReturnTypeByStr(type.ToString());
                GameObject go = Instantiate(bagItem, Vector3.zero, background.transform.rotation) as GameObject;
                go.transform.SetParent(o.transform);
                go.AddComponent(classType);
                go.GetComponent<BagItem>().Initial(type, num,bag.IndexOf(o));
                bagItems.Add(go.GetComponent<BagItem>().itemData);
                return;
            }
        }
        Debug.Log("满了");

    }
    public void DropItem()
    {

    }
    public  Vector2 GetItemPos(){

            Vector2 pos = Input.mousePosition - background.transform.position;
            pos.y = Mathf.Floor(pos.y*(-1)/spaceSzie.y+1);
            pos.x= Mathf.Floor(pos.x  / spaceSzie.x + 1);
        if (pos.x > 0 && pos.x <= width && pos.y > 0 && pos.y <= height) return pos;
        else return Vector2.zero;

    }
    public  int GetItemNum() {
        num = (int)(GetItemPos().x + (GetItemPos().y - 1) * width+1);
        return num;
    }
    public static int ReturnNum()
    {
        return num;
    }
    bool IsInArea()
    {
        Vector2 pos = Input.mousePosition - background.transform.position;
        pos.y *= -1;
        if (pos.x > 0 && pos.y > 0 && pos.x < backgroundSize.x && pos.y < backgroundSize.y) return true;
        else return false;

    }
    private void PrintBag() {
        foreach (ItemData i in bagItems) {
            Debug.Log(i.itemType+"/"+i.itemNum+"/"+i.spaceNum);
        }

    }


}
