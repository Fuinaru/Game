using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Tools : MonoBehaviour {

    public static string itemObjectPath = "Item/ItemGameObject/";
    public static string itemImgPath = "Item/ItemImg/";
    public static string itemInScenePath = "Item/ItemForGet/";
    public static string effectsPath = "Effects/";
    public static string monsterPath = "Monster/";
    public static string characterPath = "Characters/";
    public static string soundPath = "Sound/";
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
    public static GameObject GetItemGameObjectByType(Var.ItemType type)
    {
        return GetGameObjectByPath(itemObjectPath + type.ToString());
    }
    public static GameObject GetItemInSceneByType(Var.ItemType type)
    {
        return GetGameObjectByPath(itemInScenePath + type.ToString());
    }
    public static Sprite GetImgByPath(string path)
    {
        Texture2D tex = (Texture2D)Resources.Load(path);
        Sprite img = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        return img;
    }
    public static Sprite GetItemImgByType(Var.ItemType type)
    {
        return GetImgByPath(itemImgPath + type.ToString());
    }
    public static Sprite GetCharaImgByStr(string name)
    {
        return GetImgByPath(characterPath + name);
    }

    public static GameObject GetParticleSystemGameObjectByName(string name)
    {
        return GetGameObjectByPath(effectsPath +name);
    }
    public static void PlayFollowingParticletByName(string name,Transform transform)
    {
        ParticleSystem go = Instantiate(GetParticleSystemGameObjectByName(name).GetComponent<ParticleSystem>()) as ParticleSystem;
        go.gameObject.AddComponent<PlayEndDestory>();
        go.transform.SetParent(GameManager.GM.monAndItemInScene.GetChild(2));
        go.GetComponent<PlayEndDestory>().SetFollowingTarget(transform);
        go.Play();
    }
    public static void PlayParticletAtPosByName(string name, Transform transform)
    {
        ParticleSystem go = Instantiate(GetParticleSystemGameObjectByName(name).GetComponent<ParticleSystem>(),transform.position,transform.rotation) as ParticleSystem;
        go.gameObject.AddComponent<PlayEndDestory>();
        go.transform.SetParent(GameManager.GM.monAndItemInScene.GetChild(2));
        go.transform.localScale = Vector3.one;
        go.Play();
    }
    public static GameObject GetMonsterByNum(int num)
    {
        return GetGameObjectByPath(monsterPath + num.ToString());
    }


    public static void PlaySoundByName(string name, Transform transform)
    {
        AudioSource go = Instantiate(GetGameObjectByPath(soundPath+"AudioSource").GetComponent<AudioSource>()) as AudioSource;
        go.clip = (AudioClip)Resources.Load(soundPath + name, typeof(AudioClip));
        go.gameObject.AddComponent<PlayEndDestory>();
        go.transform.localScale = Vector3.one;
        go.Play();
    }
    public static void PlaySoundByName(string name, Transform transform,float speed)
    {
        AudioSource go = Instantiate(GetGameObjectByPath(soundPath + "AudioSource").GetComponent<AudioSource>()) as AudioSource;
        go.clip = (AudioClip)Resources.Load(soundPath + name, typeof(AudioClip));
        go.gameObject.AddComponent<PlayEndDestory>();
        go.pitch = speed;
        go.transform.localScale = Vector3.one;
        go.Play();
    }

    public static void LookAtOnlyYAxis(Transform self, Transform target)
    {
        self.LookAt(target);
        self.eulerAngles = new Vector3(0, self.eulerAngles.y, 0);
    }


}
