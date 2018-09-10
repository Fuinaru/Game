using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BagItem : MonoBehaviour
{
    public bool consumAble = true;
    public bool isMouseDown = false;
    private Vector2 size;

    public ItemData itemData = new ItemData();

    public Text numText;
    public Text nameText;
    public Image img;
    public Image coolImg;

    public float coolTime=1f;
    public float nextTime = 0.0f;
    // Use this for initialization
    private Transform origin = null;

    protected void Start()
    {

        size.x = GetComponent<RectTransform>().sizeDelta.x;
        size.y = GetComponent<RectTransform>().sizeDelta.y;
        transform.GetChild(0).GetComponent<Image>().sprite = GetItemImg();
        coolImg = transform.GetChild(3).GetComponent<Image>();
        coolImg.fillAmount = 0;
        origin = transform.parent;

    }



        public void Initial(Var.ItemType type, int num, int sn)
    {
        nameText = transform.GetChild(1).GetComponent<Text>();
        numText = transform.GetChild(2).GetComponent<Text>();
        transform.localPosition = Vector3.zero;
        itemData.itemType = type;
        itemData.itemNum = num;
        itemData.spaceNum = sn;
        updateText();

    }
    // Update is called once per frame
    protected void Update()
    {
        if (GameManager.isTimePause) {return; }
        AutoOrder();
        IsMouseDown();
        MouseDrag();
        ItemCoolEnd();
        if (itemData.numChanged) { updateText(); itemData.numChanged = false; }
        if (itemData.itemNum <= 0)
        {
            BagSystem.bagItems.Remove(itemData);
            Destroy(gameObject);
        }

    }

    void IsMouseDown()
    {
        if (IsInArea() && Input.GetKeyDown(KeyCode.Mouse0)) isMouseDown = true;
        if (Input.GetKeyUp(KeyCode.Mouse0)) isMouseDown = false;
    }
    void MouseDrag()
    {
        if (isMouseDown)
        {
            Vector3 pos;
            pos.x = Input.mousePosition.x - size.x / 2;
            pos.y = Input.mousePosition.y + size.y / 2;
            pos.z = transform.position.z;
            transform.position = pos;
        }
    }
    bool IsInArea()
    {
        Vector2 pos = Input.mousePosition - transform.position;
        pos.y *= -1;
        if (pos.x > 0 && pos.y > 0 && pos.x < size.x && pos.y < size.y) return true;
        else return false;

    }
    void AutoOrder()
    {
        if (isMouseDown)
        {


            if (origin != null) transform.SetParent(origin.parent.parent);
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                transform.SetParent(origin);
                if (GameManager.isUIShow)
                {
                   
                    if (GameManager.bagSys.IsInBagArea())
                    {
                        GameObject obj = GameManager.bagSys.bagObj.transform.GetChild(GameManager.bagSys.GetItemNumInBag() - GameManager.bagSys.EquipNum).gameObject;
                        //obj.GetComponent<Image>().color = Color.red;
                        Swap(obj);

                    }
                
                }
                if (GameManager.bagSys.IsInEquipArea())
                {
                    GameObject obj = GameManager.bagSys.equipObj.transform.GetChild(GameManager.bagSys.GetItemNumInEquip()).gameObject;
                    //obj.GetComponent<Image>().color = Color.red;
                    Swap(obj);

                }
                transform.localPosition = Vector3.zero;

            }
        }


    }
    public void updateText()
    {
        numText.text = itemData.itemNum.ToString();
        nameText.text = itemData.itemType.ToString();
    }
    public void useUpdate()
    {
        nextTime = coolTime;
        if (consumAble) itemData.itemNum--;
        updateText();
    }

    bool isInEquip(GameObject o)
    {
        Vector2 size = o.GetComponent<RectTransform>().sizeDelta;
        Vector2 pos = Input.mousePosition - o.transform.position;
        pos.y *= -1;
        if (pos.x > 0 && pos.y > 0 && pos.x < size.x && pos.y < size.y) return true;
        else return false;

    }
    void Swap(GameObject o)
    {

        try
        {

            Transform child = o.transform.GetChild(0).transform;
            if (child.tag == "BagItem")
            {
                child.SetParent(transform.parent);
                child.SetAsFirstSibling();
                child.localPosition = Vector3.zero;
                child.GetComponent<BagItem>().itemData.spaceNum = itemData.spaceNum;
                child.GetComponent<BagItem>().origin = child.parent;

            }

        }
        catch { }
        itemData.spaceNum = int.Parse(o.name);
        transform.SetParent(o.transform);
        transform.SetAsFirstSibling();
        origin = transform.parent;
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
    public virtual void useItem()
    {

    }

    public bool ItemCoolEnd()
    {
        coolImg.fillAmount = nextTime / coolTime;
        if (nextTime > 0) nextTime -= Time.deltaTime;
        if (nextTime <=0f)
        {
            nextTime = 0f;
            return true;
        }
        else return false;
    }


    public GameObject GetItemObject()
    {
        return Tools.GetItemGameObjectByType(itemData.itemType);

    }
    public Sprite GetItemImg()
    {
        return Tools.GetItemImgByType(itemData.itemType);
    }
}
