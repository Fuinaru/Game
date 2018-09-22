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
        try { go.GetComponent<Boom>().isStart = true; } catch { }
        go.GetComponent<Rigidbody>().AddRelativeForce(0, 0, 500);
    }
    public void SetItem(Var.ItemType type)
    {
        GameObject go = Instantiate(Tools.GetItemGameObjectByType(type), transform.position, transform.rotation) as GameObject;
        try { go.GetComponent<Boom>().isStart = true; } catch { }
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


    public void ShootItemAtPos(Var.ItemType type, Vector3 pos)
    {
        GameObject go = Instantiate(Tools.GetItemGameObjectByType(type)) as GameObject;
        go.transform.position = pos;
        Vector3 dir = (pos - transform.position).normalized;
        try { go.GetComponent<Boom>().isStart = true; } catch { }
        go.GetComponent<Rigidbody>().AddRelativeForce(dir * 500);
    }

    public void ShootItemAtPos(Var.ItemType type, Vector3 pos,float power)
    {
        GameObject go = Instantiate(Tools.GetItemGameObjectByType(type)) as GameObject;
        go.transform.position = pos;
        Vector3 dir = (pos - transform.position).normalized;
        try { go.GetComponent<Boom>().isStart = true; } catch { }
        go.GetComponent<Rigidbody>().AddRelativeForce(dir * power);
    }

    public void AroundShootAttack(Var.ItemType bullet, int start, int n)
    {

        for (float i = start; i < 360 + start; i += n)
        {
            ShootItemAtPos(bullet, transform.position + new Vector3(Mathf.Sin(i / 180 * Mathf.PI), 0.5f, Mathf.Cos(i / 180 * Mathf.PI)) * 1.5f);

        }

    }

    public void ShootItemAtPos(Var.ItemType type, Vector3 pos,float power,float boomTime)
    {
        GameObject go = Instantiate(Tools.GetItemGameObjectByType(type)) as GameObject;
        go.transform.position = pos;
        Vector3 dir = (pos - transform.position).normalized;
        try { go.GetComponent<Boom>().isStart = true; } catch { }
        if (boomTime>-1) try { go.GetComponent<Boom>().aliveTime = boomTime; } catch { }
        go.GetComponent<Rigidbody>().AddRelativeForce(dir * power);
    }

    public IEnumerator ShootItemAtPosWait(Var.ItemType type, Vector3 pos,float waittime, float power, float boomTime)
    {
        yield return new WaitForSeconds(waittime);
        ShootItemAtPos(type, pos,power,boomTime);

    }
    public void ShootItemAtPosWithDir(Var.ItemType type, Vector3 pos, float power, Vector3 dir)
    {
        GameObject go = Instantiate(Tools.GetItemGameObjectByType(type)) as GameObject;
        go.transform.position = pos;
        dir = dir.normalized;
        go.GetComponent<Rigidbody>().AddRelativeForce(dir * power);
    }
    public void ShootItemAtPosToPlayer(Var.ItemType type, Vector3 pos, float power)
    {
        GameObject go = Instantiate(Tools.GetItemGameObjectByType(type)) as GameObject;
        go.transform.position = pos;
        Vector3 dir = (GameManager.player.transform.position-pos).normalized;
        go.GetComponent<Rigidbody>().AddRelativeForce(dir * power);
    }


    public void AroundShootAttack(Var.ItemType bullet, int start, int n, float power, float boomTime)
    {

        for (float i = start; i < 360 + start; i += n)
        {
            ShootItemAtPos(bullet, transform.position + new Vector3(Mathf.Sin(i / 180 * Mathf.PI), 0.5f, Mathf.Cos(i / 180 * Mathf.PI)) * 1.5f,power, boomTime);

        }

    }



    public IEnumerator AroundShootAttackWait(Var.ItemType bullet, int start, int n, float waittime)
    {
        yield return new WaitForSeconds(waittime);
        for (float i = start; i < 360 + start; i += n)
        {
            ShootItemAtPos(bullet, transform.position + new Vector3(Mathf.Sin(i / 180 * Mathf.PI), 0.5f, Mathf.Cos(i / 180 * Mathf.PI)) * 1.5f);

        }

    }

    public GameObject ShootItemAtPosWithOutForce(Var.ItemType type, Vector3 pos)
    {
        GameObject go = Instantiate(Tools.GetItemGameObjectByType(type)) as GameObject;
        go.transform.position = pos;
        return go;
    }

    public IEnumerator AroundPlayerShootAttackWaitAddForce(Var.ItemType bullet, int start, int n,float power ,float dis,float waittime)
    {
        ArrayList array = new ArrayList();
        Vector3 pos = GameManager.player.transform.position;
        for (float i = start; i < 360 + start; i += n)
        {
            array.Add( ShootItemAtPosWithOutForce(bullet, pos + new Vector3(Mathf.Sin(i / 180 * Mathf.PI),0, Mathf.Cos(i / 180 * Mathf.PI)) * dis+new Vector3(0,0.5f,0)));
           
        }
        yield return new WaitForSeconds(waittime);
        
        foreach (GameObject o in array) {

            try { o.GetComponent<Rigidbody>().AddRelativeForce((pos - o.transform.position).normalized * power); }
            catch { }
        }

    }

    public IEnumerator AroundPlayerShootAttackWaitAddForceTwice(Var.ItemType bullet, int start, int n, float power, float dis, float waittime,float waittime2)
    {
        ArrayList array = new ArrayList();
        Vector3 pos = GameManager.player.transform.position;
        for (float i = start; i < 360 + start; i += n)
        {
            array.Add(ShootItemAtPosWithOutForce(bullet, pos + new Vector3(Mathf.Sin(i / 180 * Mathf.PI), 0, Mathf.Cos(i / 180 * Mathf.PI)) * dis + new Vector3(0, 0.5f, 0)));

        }
        yield return new WaitForSeconds(waittime);

        foreach (GameObject o in array)
        {

            try { o.GetComponent<Rigidbody>().AddRelativeForce((pos - o.transform.position).normalized * power); }
            catch { }
        }
        yield return new WaitForSeconds(waittime2);

        foreach (GameObject o in array)
        {

            try { o.GetComponent<Rigidbody>().velocity = Vector3.zero; }
            catch { }
        }
        yield return new WaitForSeconds(1f);
        foreach (GameObject o in array)
        {
            yield return new WaitForSeconds(0.1f);
            try {
                Vector3 _dir = o.transform.position - pos;
                _dir.y = 0;
                o.GetComponent<Rigidbody>().AddRelativeForce((new Vector3(_dir.z,0,_dir.x)).normalized * power); }
            catch { }
        }

    }






    public IEnumerator ShootAttackAtPosWaitAddForce(Var.ItemType bullet, Vector3 pos, float power, float waittime)
    {
   GameObject o=ShootItemAtPosWithOutForce(bullet, pos);
   yield return new WaitForSeconds(waittime);
            try { o.GetComponent<Rigidbody>().AddRelativeForce(( GameManager.player.transform.position-pos).normalized * power); }
            catch { }


    }


    public IEnumerator ShootAttackAtPosWaitAddForceTwice(Var.ItemType bullet, Vector3 pos, float power, float waittime, float waittime2)
    {
        GameObject o = ShootItemAtPosWithOutForce(bullet, pos);
        yield return new WaitForSeconds(waittime);
        try { o.GetComponent<Rigidbody>().AddRelativeForce((GameManager.player.transform.position - pos).normalized * power); }
        catch { }
        yield return new WaitForSeconds(0.5f);
        try
        {
            o.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        catch { }
        yield return new WaitForSeconds(waittime2);
        try {
           o.GetComponent<Rigidbody>().AddRelativeForce((GameManager.player.transform.position - o.transform.position).normalized * power); }
        catch { }
    }

    public IEnumerator ShootAttackAtPosWaitAddForceTriple(Var.ItemType bullet, Vector3 pos, float power, float waittime, float waittime2, float waittime3)
    {
        GameObject o = ShootItemAtPosWithOutForce(bullet, pos);
        yield return new WaitForSeconds(waittime);
        try { o.GetComponent<Rigidbody>().AddRelativeForce((GameManager.player.transform.position - pos).normalized * power); }
        catch { }
        yield return new WaitForSeconds(0.5f);
        try
        {
            o.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        catch { }
        yield return new WaitForSeconds(waittime2);
        try
        {
            o.GetComponent<Rigidbody>().AddRelativeForce((GameManager.player.transform.position - o.transform.position).normalized * power);
        }
        catch { }
        yield return new WaitForSeconds(0.5f);
        try
        {
            o.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        catch { }

        yield return new WaitForSeconds(waittime3);
        try
        {
            o.GetComponent<Rigidbody>().AddRelativeForce((GameManager.player.transform.position - o.transform.position).normalized * power);
        }
        catch { }


    }


    public IEnumerator ShootAttackAtPosWaitAddForceDirAndPlayerTwice(Var.ItemType bullet, Vector3 pos, float power, Vector3 dir, float waittime, float waittime2)
    {
        GameObject o = ShootItemAtPosWithOutForce(bullet, pos);
        yield return new WaitForSeconds(waittime);
        try { o.GetComponent<Rigidbody>().AddRelativeForce(dir.normalized * power); }
        catch { }
        yield return new WaitForSeconds(0.5f);
        try
        {
            o.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        catch { }
        yield return new WaitForSeconds(waittime2);
        try
        {
            o.GetComponent<Rigidbody>().AddRelativeForce((GameManager.player.transform.position - o.transform.position).normalized * power);
        }
        catch { }
    }




}