using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool shouldMapBrushBeDestroyed ;
    public  bool m_shouldMapBrushBeDestroyed = true;
    private void SetShouldMapBrushBeDestroyed()
    {
        shouldMapBrushBeDestroyed = m_shouldMapBrushBeDestroyed;
    }
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

    public static BagSystem bagSys;
    public BagSystem m_bagSys;
    private void SetBagSys()
    {
        bagSys = m_bagSys;
    }
    public static TaskManager taskManager;
    public TaskManager m_taskManager;
    private void SetTaskManager()
    {
        taskManager = m_taskManager;
    }
    public static bool isUIShow;
    public GameObject bag;
    public GameObject TaskUI;
    private void SetIsUIShow()
    {
        bag.SetActive(isUIShow);
        TaskUI.SetActive(isUIShow);
    }

    //Player
    public static Player player;
    public  Player m_player;
    private void SetPlayer()
    {
    player = m_player;
     }


    public static Animator playerAni;
    private void SetPlayerAni()
    {
        playerAni = m_player.GetComponent<Animator>();
    }








    void Start()
    {
        SetShouldMapBrushBeDestroyed();
        //  isTimePause = m_isTimePause;
        SetIsUIShow();
        SetBagSys();
        SetTaskManager();
        SetPlayer();
        SetPlayerAni();
        SetGameover();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) )
        {
            isUIShow = !isUIShow;
            bag.SetActive(isUIShow);
            TaskUI.SetActive(isUIShow);
        }
    }

}
