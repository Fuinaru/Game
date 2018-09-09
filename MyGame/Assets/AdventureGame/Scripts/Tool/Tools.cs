using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Tools : MonoBehaviour {

    public static string itemObjectPath = "Item/ItemGameObject/";
    public static string itemImgPath = "Item/ItemImg/";
    public static string effectsPath = "Effects/";

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public static Type ReturnTypeByStr(string str)
    {
        Type classType = Type.GetType(new Tools().GetType().Namespace + "." + str, true, true);
        return classType;
    }



    //动态加载
    public static GameObject GetGameObjectByPath(string path)
    {
        GameObject o = (GameObject)Resources.Load(path);

        return o;
    }
    public static GameObject GetItemGameObjectByType(MyGameVariable.ItemType type)
    {
        return GetGameObjectByPath(itemObjectPath + type.ToString());
    }

    public static Sprite GetImgByPath(string path)
    {
        Texture2D tex = (Texture2D)Resources.Load(path);
        Sprite img = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        return img;
    }
    public static Sprite GetItemImgByType(MyGameVariable.ItemType type)
    {
        return GetImgByPath(itemImgPath + type.ToString());
    }


    public static GameObject GetParticleSystemGameObjectByName(string name)
    {
        return GetGameObjectByPath(effectsPath +name);
    }

    public static void LookAtOnlyYAxis(Transform self, Transform target)
    {
        self.LookAt(target);
        self.eulerAngles = new Vector3(0, self.eulerAngles.y, 0);
    }


}
