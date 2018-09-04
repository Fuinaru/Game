using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class BagSystem : MonoBehaviour {
    public int width = 5;
    public int height = 5;
    public GameObject itemSpace;
    public GameObject bagItem;
    private Vector2 spaceSzie;
    private Vector2 backgroundSize;
    public static int num;
   public static List<GameObject>  bag = new List<GameObject>();
    public static List<GameObject> bagItems = new List<GameObject>();
    public  GameObject equipOne;
    public GameObject equipTwo;
    // Use this for initialization
    void Start() {

        GetSpaceSize();
        SetBackgroundSize();
        SpaceInitial();
        bag.Add(equipOne);
        bag.Add(equipTwo);
    }

    // Update is called once per frame
    void Update() {
        num = GetItemNum();
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
           Debug.Log(GetItemNum());
        }
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
        GetComponent<RectTransform>().sizeDelta = backgroundSize;
    }
    void SpaceInitial() {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {

                Vector3 pos;
                pos.x = transform.position.x + j * spaceSzie.x;
                pos.y = transform.position.y - i * spaceSzie.y;
                pos.z = transform.position.z;
                GameObject go = Instantiate(itemSpace, pos, transform.rotation) as GameObject;
                go.transform.SetParent(transform);
                bag.Add(go);
            }
        }
    }
    public  void AddItem(BagItem.ItemType type,int num) {
        foreach (GameObject o in bagItems)
        {
            if (o.GetComponent<BagItem>().itemType.Equals(type))
            {
                o.GetComponent<BagItem>().itemNum += num;
                o.GetComponent<BagItem>().updateText();
                return;
             }
        }
        foreach (GameObject o in bag)
        {
            if (o.transform.childCount == 0)
            {
                GameObject go = Instantiate(bagItem, Vector3.zero, transform.rotation) as GameObject;
                go.transform.SetParent(o.transform);
                go.GetComponent<BagItem>().Initial(type,num);
                bagItems.Add(go);
                return;
            }
        }
        Debug.Log("满了");

    }
    public void DropItem()
    {

    }
    public  Vector2 GetItemPos(){

            Vector2 pos = Input.mousePosition - transform.position;
            pos.y = Mathf.Floor(pos.y*(-1)/spaceSzie.y+1);
            pos.x= Mathf.Floor(pos.x  / spaceSzie.x + 1);
        if (pos.x > 0 && pos.x <= width && pos.y > 0 && pos.y <= height) return pos;
        else return Vector2.zero;

    }
    public  int GetItemNum() {
        num = (int)(GetItemPos().x + (GetItemPos().y - 1) * width);
        return num;
    }
    public static int ReturnNum()
    {
        return num;
    }
    bool IsInArea()
    {
        Vector2 pos = Input.mousePosition - transform.position;
        pos.y *= -1;
        if (pos.x > 0 && pos.y > 0 && pos.x < backgroundSize.x && pos.y < backgroundSize.y) return true;
        else return false;

    }

}
