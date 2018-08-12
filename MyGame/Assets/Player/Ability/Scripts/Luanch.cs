using UnityEngine;
using System.Collections;

public class Luanch : MonoBehaviour
{

    public GameObject bullet;
    public GameObject boom;
    float firetime = 0.2f;
    float nexttime = 0.0f;
    KeyCode openFire = KeyCode.F;
    KeyCode setBoom = KeyCode.B;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
            if (Input.GetKeyDown(openFire) && nexttime < Time.time)
        {
            nexttime = firetime + Time.time;
            GameObject go = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
            go.GetComponent<Rigidbody>().AddRelativeForce(0, 0, 1200);

        }
        if (Input.GetKeyDown(setBoom) && nexttime < Time.time)
        {
            nexttime = firetime + Time.time;
            GameObject go = Instantiate(boom, transform.position, transform.rotation) as GameObject;
            go.GetComponent<Rigidbody>().AddRelativeForce(0, 0, 100);

        }
    }
}