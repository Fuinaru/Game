using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class creatFollowingUI : MonoBehaviour
{

    public GameObject target;
    public Vector3 offset = new Vector3(0, 2.5f, 0);
    public GameObject Tar;

    // Use this for initialization
    void Start()
    {
        if (target != null)
        {
             Tar = Instantiate(target);
        }
        Tar.transform.SetParent(GameObject.Find("MonsterHP").transform);

         
    }
    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Tar.transform.position = Camera.main.WorldToScreenPoint(transform.position + offset);
        }
    }
    private void OnDestroy()
    {
        Destroy(Tar);
    }

}