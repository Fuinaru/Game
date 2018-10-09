using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NPC : PressKeyButton
{

    // Use this for initialization
    public static Fungus.Flowchart flowchart;
    public static Fungus.Character character;
    public  Fungus.Character m_character;
    public static Image img;
    public Image m_img;
    public string name;
    public string say;
    public Var.ItemType addItem;
    public int addItemNum;
    void Start () {
        if (transform.tag == "chart") {
            flowchart = GetComponent<Fungus.Flowchart>();
            character = m_character;
          if(m_img != null) img = m_img;
        }
    }
	
	// Update is called once per frame
	void Update () {
       
    }

    protected override void  InterAction() {
        SetConversation();
    }

    public void SetConversation() {
        if (transform.tag == "chart") return;
        character.SetStandardText(name);
        try { img.sprite = Tools.GetCharaImgByStr(name); }
        catch { }
     
        if (addItem != Var.ItemType.Coin && !GameManager.GM.PlayerHaveKeyItem(addItem))
        {
            flowchart.SetStringVariable("str", say);
            GameManager.playerLaunch.GetItem(addItem, addItemNum);
          
        }
        else {
            flowchart.SetStringVariable("str", "东西已经给你了，滚！！");
        }

        Fungus.Flowchart.BroadcastFungusMessage("say");
    }
    public static void SetConver(string NPCName,string sayWhat)
    {
        character.SetStandardText(NPCName);
        try { img.sprite = Tools.GetCharaImgByStr(NPCName); }
        catch { }
        flowchart.SetStringVariable("str", sayWhat);
        Fungus.Flowchart.BroadcastFungusMessage("say");
    }


}
