using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class creatFollowingUI : MonoBehaviour
{

    public GameObject target;
    public Vector3 offset = new Vector3(0, 2.5f, 0);
    public GameObject tar;

    // Use this for initialization
    void Start()
    {
        if (target != null)
        {
             tar = Instantiate(target);
        }
        tar.transform.SetParent(GameObject.Find("Canvas").transform);

         
    }
    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            tar.transform.position = Camera.main.WorldToScreenPoint(transform.position + offset);
        }
    }
    private void OnDestroy()
    {
        Destroy(tar);
    }

}