using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class Store : MonoBehaviour {
    public List<Var.ItemType> goodsList;
    public List<int> goodsPrice;
    public GameObject storeItem;
    private float width;
    // Use this for initialization
    void Start () {
        transform.localScale = new Vector3(Screen.width / 1280f, Screen.width / 1280f, Screen.width / 1280f);
        //InitialData();
    }
	
	// Update is called once per frame
	void Update () {
       
    }
    public void NPCSetData(List<Var.ItemType> gL, List<int> gP) {
        goodsList = gL;
        goodsPrice = gP;
        GameManager.GM.CloseUI();
        GameManager.isInStore = true;
        if (transform.childCount==0)InitialData();
        NPC.SetConver("OldMan", "要点什么？");
    }


    void InitialData() {
        width = storeItem.GetComponent<RectTransform>().sizeDelta.x;
        foreach (Var.ItemType o in goodsList)
        {
            SetItem(o);
        }
    }
    void SetItem(Var.ItemType o) {
        int n = goodsList.IndexOf(o);
        GameObject go = Instantiate(storeItem);
        go.transform.GetChild(0).GetComponent<Image>().sprite = Tools.GetItemImgByType(o);
        go.transform.GetChild(1).GetComponent<Text>().text = goodsPrice[n].ToString()+"$";
        go.transform.SetParent(transform);
  
        go.transform.position = new Vector3(n * width, 0)+transform.position;
        go.GetComponent<Button>().onClick.AddListener(delegate ()
        {
            this.OnClick(o);
        });
    }

    public void OnClick(Var.ItemType o) {
        int n= goodsList.IndexOf(o);
        n = goodsPrice[n];
        if (GameManager.player.CostCoin(n))
        {
            GameManager.player.CostCoin(n);
            if (o == Var.ItemType.Coin) { GameManager.player.GetCoin(10); }
            else GameManager.playerLaunch.GetItem(o, 1);
            NPC.SetConver("OldMan", "顾客就是上帝");
        }
        else {
            NPC.SetConver("OldMan", "没钱滚！！！");
            CloseStore();
        }
    }

    public void CloseStore() {
        GameManager.isInStore = false;
        NPC.SetConver("OldMan", "慢走");
        int childCount = transform.childCount;
        Debug.Log(childCount);
        for (int i = childCount-1; i >=0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
            Debug.Log(i);
        }
        transform.parent.gameObject.SetActive(false);
    }
}
