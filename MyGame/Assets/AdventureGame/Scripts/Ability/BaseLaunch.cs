using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class BaseLuanch : MyGameObject
{
    public float coolTime = 1f;
    public float nextTime = 0.0f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ShootItem(Var.ItemType type) {
        GameObject go = Instantiate(Tools.GetItemGameObjectByType(type), transform.position, transform.rotation) as GameObject;
        go.GetComponent<Rigidbody>().AddRelativeForce(0, 0, 500);
    }
    public void SetItem(Var.ItemType type)
    {
        GameObject go = Instantiate(Tools.GetItemGameObjectByType(type), transform.position, transform.rotation) as GameObject;
        go.GetComponent<Rigidbody>().AddRelativeForce(0, 0, 100);
    }

    public bool CoolEnd()
    {

        if (nextTime < Time.time)
        {
            nextTime = coolTime + Time.time;
            return true;
        }
        else return false;
    }

    public void ShootItemAtPos(Var.ItemType type,Vector3 pos)
    {
        GameObject go = Instantiate(Tools.GetItemGameObjectByType(type)) as GameObject;
        go.transform.position = pos;
        Vector3 dir = (pos-transform.position).normalized;
        go.GetComponent<Rigidbody>().AddRelativeForce(dir*500);
    }


}