using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MyGameVariable : MonoBehaviour {
//ItemType
    public enum ItemType
    {
        BulletItem,
        BoomItem,
        PotionItem
    }
//ItemImg
    public List<Sprite> m_itemImage = new List<Sprite>();
    public static List<Sprite> itemImage ;
    private void SetItemImage() {
        itemImage = m_itemImage;
    }
 //ItemObject
    public List<GameObject> m_itemObject = new List<GameObject>();
    public static List<GameObject> itemObject;
    private void SetItemObject()
    {
        itemObject = m_itemObject;
    }

    //ParticleEffects
    public List<ParticleSystem> m_particleEffects = new List<ParticleSystem>();
    public static List<ParticleSystem> particleEffects;
    private void SetParticleEffects()
    {
        particleEffects = m_particleEffects;
    }






    private void Start()
    {
        SetItemObject();
        SetItemImage();
        SetParticleEffects();
       
    }
}
