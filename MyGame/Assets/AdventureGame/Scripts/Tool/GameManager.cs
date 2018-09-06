using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //stageSceneManage
    public static int CurrentStage = 3;
    public void GameStart()
    {
        CurrentStage = 3;
        SceneManager.LoadScene("stage1");
        SceneManager.LoadSceneAsync("player", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("dialog", LoadSceneMode.Additive);
    }
    //GameProgress
    static public bool isTimePause = true;
    static public bool isGameOver = false;
    public GameObject m_gameover;
    public static GameObject gameover;
    private void SetGameover(){
        gameover = m_gameover;
    }
    public static void GameOver() {
        gameover.transform.SetSiblingIndex(999);
        gameover.SetActive(true);
    }

//GameObject
    //bagShow
    public static bool isBagShow;
    public GameObject bag;
    private void SetIsBagShow() {
        isBagShow = bag.activeInHierarchy;
    }

    //Player
    public static Player player;
    public  Player m_player;
    private void SetPlayer()
    {
    player = m_player;
     }









    void Start()
    {
        //  isTimePause = m_isTimePause;
        SetIsBagShow();
        SetPlayer();
        SetGameover();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!bag.activeInHierarchy) { bag.SetActive(true); isBagShow = true; }
            else { bag.SetActive(false); isBagShow = false; };
        }
    }

}
