using UnityEngine;
using System.Collections;

public class Follow3DObject : MonoBehaviour
{

    public Transform target;
    public Vector3 offset = new Vector3(0, 1, 0);

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(target.position + offset);
        }
    }
}