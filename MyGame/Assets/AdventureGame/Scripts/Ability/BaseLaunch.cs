using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class BaseLuanch : myGameObject
{

    public  GameObject bullet;
    public  GameObject boom;
    public float bulletCoolTime = 0.2f;
    protected float nexttime = 0.0f;
    public float boomCoolTime = 1f;
    protected float nexttimeB = 0.0f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Shoot() {
        GameObject go = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
        go.GetComponent<Rigidbody>().AddRelativeForce(0, 0, 1200);
    }
    public void SetBoom()
    {
        GameObject go = Instantiate(boom, transform.position, transform.rotation) as GameObject;
        go.GetComponent<Rigidbody>().AddRelativeForce(0, 0, 100);
    }

    public bool BulletCoolEnd()
    {

        if (nexttime < Time.time)
        {
            nexttime = bulletCoolTime + Time.time;
            return true;
        }
        else return false;
    }
    public bool BoomCoolEnd()
        {

            if (nexttimeB < Time.time)
            {
                nexttimeB = boomCoolTime + Time.time;
                return true;
            }
            else return false;
        }
    }