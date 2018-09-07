using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagSystem : MonoBehaviour
{
    public int width = 5;
    public int height = 5;
    public int EquipNum = 2;
    public GameObject itemSpace;
    public GameObject bagItem;
    private Vector2 spaceSzie;
    public GameObject bagObj;
    private Vector2 backgroundSize;
    public static int num;
    public static List<ItemData> bagItems = new List<ItemData>();
    public GameObject equipOne;
    public GameObject equipTwo;
    public  int bagSize;

    // Use this for initialization
    void Start()
    {

        GetSpaceSize();
        SetBackgroundSize();
        SpaceInitial();
        bagSize = width * height;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) Debug.Log(GetItemNum());
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
        bagObj.GetComponent<RectTransform>().sizeDelta = backgroundSize;
    }
    void SpaceInitial()
    {

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                GameObject go = Instantiate(itemSpace, GetWorldPositionByPos(i, j), bagObj.transform.rotation) as GameObject;
                go.name = (j + i * width+EquipNum).ToString();
                go.transform.SetParent(bagObj.transform);
            }
        }
    }
    public void AddItem(MyGameVariable.ItemType type, int num)
    {
        foreach (ItemData o in bagItems)
        {
            if (o.itemType.Equals(type))
            {
                o.itemNum += num;
                o.numChanged = true;
                return;
            }
        }
        for (int i = 0; i < EquipNum; i++)
        {
            GameObject o = transform.GetChild(i).gameObject;
            if (o.transform.childCount == 0)
            {
                Type classType = Tools.ReturnTypeByStr(type.ToString());
                GameObject go = Instantiate(bagItem, Vector3.zero, bagObj.transform.rotation) as GameObject;
                go.transform.SetParent(o.transform);
                go.AddComponent(classType);
                go.GetComponent<BagItem>().Initial(type, num, i);
                bagItems.Add(go.GetComponent<BagItem>().itemData);
                return;
            }
        }


        for (int i = 0; i < bagSize; i++)
        {
            GameObject o = bagObj.transform.GetChild(i).gameObject;
            if (o.transform.childCount==0) { 
            Type classType = Tools.ReturnTypeByStr(type.ToString());
            GameObject go = Instantiate(bagItem, Vector3.zero, bagObj.transform.rotation) as GameObject;
            go.transform.SetParent(o.transform);
            go.AddComponent(classType);
            go.GetComponent<BagItem>().Initial(type, num, i);
            bagItems.Add(go.GetComponent<BagItem>().itemData);
            return;
        }
            }

        Debug.Log("满了");

    }
    public void DropItem()
    {

    }
    public Vector2 GetItemPos()
    {

        Vector2 pos = Input.mousePosition - bagObj.transform.position;
        pos.y = Mathf.Floor(pos.y * (-1) / spaceSzie.y + 1);
        pos.x = Mathf.Floor(pos.x / spaceSzie.x + 1);
        if (pos.x > 0 && pos.x <= width && pos.y > 0 && pos.y <= height) return pos;
        else return Vector2.zero;

    }
    public int GetItemNum()
    {
        num = (int)(GetItemPos().x + (GetItemPos().y - 1) * width + EquipNum-1);
        return num;
    }
    public static int ReturnNum()
    {
        return num;
    }
    public Vector3 GetWorldPositionByPos(int x, int y)
    {
        Vector3 worldpos;
        worldpos.x = bagObj.transform.position.x + y * spaceSzie.x;
        worldpos.y = bagObj.transform.position.y - x * spaceSzie.y;
        worldpos.z = bagObj.transform.position.z;
        return worldpos;
    }
    bool IsInArea()
    {
        Vector2 pos = Input.mousePosition - bagObj.transform.position;
        pos.y *= -1;
        if (pos.x > 0 && pos.y > 0 && pos.x < backgroundSize.x && pos.y < backgroundSize.y) return true;
        else return false;

    }
    private void PrintBag()
    {
        foreach (ItemData i in bagItems)
        {
            Debug.Log(i.itemType + "/" + i.itemNum + "/" + i.spaceNum);
        }

    }


}
