using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Open : MonoBehaviour {
    public bool over = false;
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {

       
        if (!NPC.flowchart.GetBooleanVariable("isTalking")) {
            if (TwirlScripts.angle == 360f &&!over)
            {
                TwirlScripts.angle = 0;             
                StartCoroutine(StartLoad());
                over = true;
            }
        }


    }

    IEnumerator StartLoad() {
        yield return new WaitUntil(() => TwirlScripts.angle>=120);
        SceneManager.LoadSceneAsync("player");
        SceneManager.LoadSceneAsync("dialog", LoadSceneMode.Additive);
      

    }
}
