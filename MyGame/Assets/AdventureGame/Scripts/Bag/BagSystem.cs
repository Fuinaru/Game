using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BagSystem : MonoBehaviour
{
    public int width = 5;
    public int height = 5;
    public int EquipNum = 2;
    public GameObject itemSpace;
    public GameObject equipSpace;
    public GameObject bagItem;
    public static Vector2 spaceSize;
    public GameObject bagObj;
    public GameObject equipObj;
    private Vector2 bagBackgroundSize;
    private Vector2 equipBackgroundSize;
    public static List<ItemData> bagItems = new List<ItemData>();


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
       
    }
    void GetSpaceSize()
    {
        spaceSize = itemSpace.GetComponent<RectTransform>().sizeDelta;
        spaceSize = (spaceSize / 1280) * Screen.width;
     
        // spaceSize = (spaceSize / 1280) * Screen.width;
        // rt.sizeDelta = spaceSize;
        //  rt.transform.localScale = new Vector3(Screen.width / 1280f, Screen.width / 1280f, Screen.width / 1280f);
        // var rt2 =equipSpace.GetComponent<RectTransform>();
        //  rt2.sizeDelta = spaceSize;
        //  rt2.transform.localScale = new Vector3(Screen.width / 1280f, Screen.width / 1280f, Screen.width / 1280f);



        Debug.Log(spaceSize);
    }
    void SetBackgroundSize()
    {
        bagBackgroundSize.x = width * 75;
        bagBackgroundSize.y = height * 75;
        bagObj.GetComponent<RectTransform>().sizeDelta = bagBackgroundSize;

        equipBackgroundSize.x = EquipNum * 75;
        equipBackgroundSize.y = 75;
        equipObj.GetComponent<RectTransform>().sizeDelta = equipBackgroundSize;
    }
    public void SpaceInitial()
    {
        for (int i = 0; i < EquipNum; i++)
        {
            GameObject go = Instantiate(equipSpace) as GameObject;
            go.name = (i).ToString();
           go.transform.localScale= new Vector3(Screen.width / 1280f, Screen.width / 1280f, Screen.width / 1280f);
            go.transform.SetParent(equipObj.transform);
            go.transform.position = new Vector3(i * spaceSize.x, 0, 0)+ equipObj.transform.position;
            go.transform.GetChild(0).GetComponent<Text>().text = (i + 1).ToString();
        }
            for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                GameObject go = Instantiate(itemSpace, GetWorldPositionByPos(i, j), bagObj.transform.rotation) as GameObject;
                 go.transform.localScale= new Vector3(Screen.width / 1280f, Screen.width / 1280f, Screen.width / 1280f);
                go.name = (j + i * width+EquipNum).ToString();
                go.transform.SetParent(bagObj.transform);
            }
        }
    }

    public bool SpaceClear()
    {
        for (int i = 0; i < EquipNum; i++)
        {
            // Destroy(equipObj.transform.GetChild(i).gameObject);
            if(equipObj.transform.GetChild(i).childCount==2) Destroy(equipObj.transform.GetChild(i).GetChild(0).gameObject);
        }
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                //Destroy(bagObj.transform.GetChild(j + i * width).gameObject);
                if (bagObj.transform.GetChild(j + i * width).childCount == 1) Destroy(bagObj.transform.GetChild(j + i * width).GetChild(0).gameObject);
            }
        }
        return true;
    }

    public void AddItemAtNum(ItemData itemData)
    {

        GameObject o;
        if (itemData.spaceNum < EquipNum)
        { o = equipObj.transform.GetChild(itemData.spaceNum).gameObject; }
        else
        { o = bagObj.transform.GetChild(itemData.spaceNum - EquipNum).gameObject;  }
        Type classType = Tools.ReturnTypeByStr(itemData.itemType.ToString());
        GameObject go = Instantiate(bagItem, Vector3.zero, o.transform.rotation) as GameObject;
        go.transform.SetParent(o.transform);
        go.transform.localScale = Vector3.one;
        go.transform.SetAsFirstSibling();
        go.AddComponent(classType);
        go.GetComponent<BagItem>().Initial(itemData.itemType, itemData.itemNum, itemData.spaceNum);
        bagItems.Add(go.GetComponent<BagItem>().itemData);

    }




    public void AddItem(Var.ItemType type, int num)
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
            GameObject o = equipObj.transform.GetChild(i).gameObject;
            if (o.transform.childCount == 1)
            {
                Type classType = Tools.ReturnTypeByStr(type.ToString());
                GameObject go = Instantiate(bagItem, Vector3.zero, equipObj.transform.rotation) as GameObject;
                go.transform.SetParent(o.transform);
                go.transform.SetAsFirstSibling();
                go.AddComponent(classType);
                go.transform.localScale =  Vector3.one;
                go.GetComponent<BagItem>().Initial(type, num, i);
                bagItems.Add(go.GetComponent<BagItem>().itemData);
                return;
            }
        }


        for (int i = 0; i < bagSize; i++)
        {
            GameObject o = bagObj.transform .GetChild(i).gameObject;
            if (o.transform.childCount==0) { 
            Type classType = Tools.ReturnTypeByStr(type.ToString());
            GameObject go = Instantiate(bagItem, Vector3.zero, bagObj.transform.rotation) as GameObject;
            go.transform.SetParent(o.transform);
            go.AddComponent(classType);
                go.transform.localScale = Vector3.one;
                go.GetComponent<BagItem>().Initial(type, num, i+EquipNum);
            bagItems.Add(go.GetComponent<BagItem>().itemData);
            return;
        }
            }

        Debug.Log("满了");

    }
    public void DropItem()
    {

    }
    public Vector2 GetItemPosInBag()
    {

        Vector2 pos = Input.mousePosition - bagObj.transform.position;
        pos.y = Mathf.Floor(pos.y * (-1) / spaceSize.y + 1);
        pos.x = Mathf.Floor(pos.x / spaceSize.x + 1);
        if (pos.x > 0 && pos.x <= width && pos.y > 0 && pos.y <= height) return pos;
        else return Vector2.zero;

    }

    public int GetItemNumInBag()
    {
        return (int)(GetItemPosInBag().x + (GetItemPosInBag().y - 1) * width + EquipNum-1);

    }
    public int GetItemNumInEquip()
    {
        float pos = (Input.mousePosition - equipObj.transform.position).x;
        Debug.Log(spaceSize.x + "/" + pos+"/" + pos / spaceSize.x);
        return (int)(pos/spaceSize.x);

    }
    public Vector3 GetWorldPositionByPos(int x, int y)
    {
        Vector3 worldpos;
        worldpos.x = bagObj.transform.position.x + y * spaceSize.x;
        worldpos.y = bagObj.transform.position.y - x * spaceSize.y;
        worldpos.z = bagObj.transform.position.z;
        return worldpos;
    }
   public bool IsInBagArea()
    {
        Vector2 pos = Input.mousePosition - bagObj.transform.position;
        pos.y *= -1;
        if (pos.x > 0 && pos.y > 0 && pos.x < bagBackgroundSize.x && pos.y < bagBackgroundSize.y) return true;
        else return false;
    }
    public bool IsInEquipArea()
    {
        Vector2 pos = Input.mousePosition - equipObj.transform.position;
        pos.y *= -1;
        if (pos.x > 0 && pos.y > 0 && pos.x < equipBackgroundSize.x && pos.y < equipBackgroundSize.y) return true;
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
