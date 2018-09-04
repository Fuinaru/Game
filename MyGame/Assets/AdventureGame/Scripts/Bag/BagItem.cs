using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BagItem : MonoBehaviour {

    public bool isDragable = true;
    public bool isMouseDown = false;
    private Vector2 size ;
    public  enum ItemType
    {
        bullet,
        boom
    }
    public  ItemType itemType;
    public int itemNum=0;
    public Text numText;
    public Text nameText;
    // Use this for initialization
    void Start () {
        size.x = GetComponent<RectTransform>().sizeDelta.x;
        size.y = GetComponent<RectTransform>().sizeDelta.y;
        updateText();
    }

    public void  Initial(ItemType type,int num ) {
        transform.localPosition = Vector3.zero;
        itemType = type;
        itemNum = num;
        updateText();
    }
    // Update is called once per frame
    void Update()
    {
        AutoOrder();
        IsMouseDown();
        MouseDrag();
        if (itemNum <= 0)
        {
            BagSystem.bagItems.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    void IsMouseDown() {
        if (IsInArea()&&Input.GetKeyDown(KeyCode.Mouse0)) isMouseDown = true;
        if(Input.GetKeyUp(KeyCode.Mouse0)) isMouseDown = false;
    }
     void MouseDrag()
    {
        if (isMouseDown) {
            Vector3 pos;
            pos.x = Input.mousePosition.x - size.x / 2;
            pos.y= Input.mousePosition.y + size.y / 2;
            pos.z = transform.position.z;
            transform.position = pos;
        }
    }
     bool IsInArea() {
        Vector2 pos=Input.mousePosition - transform.position;
        pos.y *= -1;
        if (pos.x > 0 && pos.y > 0 && pos.x < size.x && pos.y < size.y) return true;
        else return false;
        
    }
    void AutoOrder() {
        if (isMouseDown)
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
            { 
            if (BagSystem.ReturnNum() > 0 && BagSystem.ReturnNum() <= BagSystem.bag.Count)
            {
                GameObject obj = BagSystem.bag[BagSystem.ReturnNum() - 1];
                    //obj.GetComponent<Image>().color = Color.red;
                    Swap(obj);
               
            }
                if (isInEquip(BagSystem.bag[BagSystem.bag.Count-2])) Swap(BagSystem.bag[BagSystem.bag.Count - 2]);
                if (isInEquip(BagSystem.bag[BagSystem.bag.Count - 1])) Swap(BagSystem.bag[BagSystem.bag.Count - 1]);

                transform.localPosition = Vector3.zero;
            }
        }
       

    }
    public void updateText() {
        numText.text = itemNum.ToString();
        nameText.text = itemType.ToString();
    }
    bool isInEquip(GameObject o) {
        Vector2 size = o.GetComponent<RectTransform>().sizeDelta;
        Vector2 pos = Input.mousePosition - o.transform.position;
        pos.y *= -1;
        if (pos.x > 0 && pos.y > 0 && pos.x < size.x && pos.y < size.y) return true;
        else return false;

    }
    void Swap(GameObject o) {
        try
        {
            Transform child = o.transform.GetChild(0).transform;
            if (child.tag == "BagItem") child.SetParent(transform.parent);
            child.localPosition = Vector3.zero;
        }
        catch { }
        transform.SetParent(o.transform);
    }
    //public void useItem() {
    //    Debug.Log("useItem");
    //    if (itemType == ItemType.bullet) {
    //        Debug.Log("bullet");
    //        GameObject go = Instantiate(PlayerLaunch.SBullet, PlayerLaunch.trans.position, PlayerLaunch.trans.rotation) as GameObject;
    //        go.GetComponent<Rigidbody>().AddRelativeForce(0, 0, 1200);
    //    }
    //    if (itemType == ItemType.boom)
    //    {
    //        Debug.Log("Boom");
    //        GameObject go = Instantiate(PlayerLaunch.SBoom, PlayerLaunch.trans.position, PlayerLaunch.trans.rotation) as GameObject;
    //        go.GetComponent<Rigidbody>().AddRelativeForce(0, 0, 100);
    //    }
        
    //    itemNum--;
    //    updateText();
    //}

}
