using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MyGameVariable : MonoBehaviour {

    public enum ItemType
    {
        BulletItem,
        BoomItem
    }
    public List<Sprite> m_itemImage = new List<Sprite>();
    //key name
    public static List<Sprite> itemImage ;
    private void Start()
    {
        itemImage = m_itemImage;
    }

}
