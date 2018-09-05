using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Tools : MonoBehaviour {

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

    public static object CreateClassByStr(string str)
    {
        Type classType = ReturnTypeByStr(str);
    var s = Activator.CreateInstance(classType);
        return s;
}
}
