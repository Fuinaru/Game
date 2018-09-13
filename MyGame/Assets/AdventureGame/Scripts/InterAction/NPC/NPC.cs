using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : PressKeyButton
{

    // Use this for initialization
    public static Fungus.Flowchart flowchart;
    public static Fungus.Character character;
    public  Fungus.Character m_character;

    public string name;
    public string say;

    void Start () {
        if (transform.tag == "chart") {
            flowchart = GetComponent<Fungus.Flowchart>();
            character = m_character;
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
        flowchart.SetStringVariable("str", say);
        Fungus.Flowchart.BroadcastFungusMessage("say");
    }
    public static void SetConver(string NPCName,string sayWhat)
    {
        character.SetStandardText(NPCName);
        flowchart.SetStringVariable("str", sayWhat);
        Fungus.Flowchart.BroadcastFungusMessage("say");
    }


}
