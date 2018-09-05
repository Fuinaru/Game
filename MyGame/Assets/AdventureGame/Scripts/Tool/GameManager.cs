using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
    {
    public static int CurrentStage = 3;
    public bool s = false;
      static public bool isTimePause = true;
    static public bool isGameOver = false;
    public GameObject gameover;
    public static bool isBagShow;
    public GameObject bag;
    // public bool m_isTimePause = false;
    // Use this for initialization
    void Start()
        {
        //  isTimePause = m_isTimePause;
        isBagShow = bag.activeInHierarchy;
        }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            gameover.transform.SetSiblingIndex(999);

            // isTimePause = true;
            gameover.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!bag.activeInHierarchy) { bag.SetActive(true); isBagShow = true; }
            else { bag.SetActive(false);isBagShow = false; };
        }
    }

    public void GameStart()
    {
        CurrentStage = 3;
        SceneManager.LoadScene("stage1");
        SceneManager.LoadSceneAsync("player", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("dialog", LoadSceneMode.Additive);
    }

}
