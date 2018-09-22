using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool isLoading = false;

    public static int stageName = 1;

    public static GameManager GM;
    private void SetGM()
    {
        GM = this.GetComponent<GameManager>();
    }



    public static bool shouldMapBrushBeDestroyed ;
    public  bool m_shouldMapBrushBeDestroyed = true;
    private void SetShouldMapBrushBeDestroyed()
    {
        shouldMapBrushBeDestroyed = m_shouldMapBrushBeDestroyed;
    }
    //stageSceneManage

    public void GameStart()
    {
        SceneManager.LoadScene("openDialog");
        // SceneManager.LoadScene("player");
        //  SceneManager.LoadSceneAsync("dialog", LoadSceneMode.Additive);
    }
    //GameProgress
    static public bool isTalking = false;
    static public bool isGameOver = false;
    static public bool isTimePause = false;
    public GameObject m_gameover;
    public static GameObject gameover;
    private void SetGameover(){
        gameover = m_gameover;
    }
    public static void GameOver() {
        isGameOver = true;
        gameover.transform.SetSiblingIndex(999);
        gameover.SetActive(true);
    }



    public Transform monAndItemInScene;

    //GameObject
    //bagShow

    public static BagSystem bagSys;
    public BagSystem m_bagSys;
    private void SetBagSys()
    {
        bagSys = m_bagSys;
    }

    public static Store store;
    public Store m_store;
    public static bool isInStore = false;
    private void SetStores()
    {
        store = m_store;
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
    public GameObject SettingUI;
    public static bool isSettingUIShow=false;

    //Player
    public static Player player;
    public  Player m_player;
    private void SetPlayer()
    {
    player = m_player;
     }

    public static PlayerLaunch playerLaunch;
    public PlayerLaunch m_playerLaunchr;
    private void SetPlayerLaunchr()
    {
        playerLaunch = m_playerLaunchr;
    }
    public static Animator playerAni;
    private void SetPlayerAni()
    {
        playerAni = m_player.GetComponent<Animator>();
    }

    public static List<BaseMonster> Monsters=new List<BaseMonster>();
    public static List<ItemForGet>  itemInScene= new List<ItemForGet>();


    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "open") return;
        SetGM();
        SetShouldMapBrushBeDestroyed();
        //  isTimePause = m_isTimePause;
        SetIsUIShow();
        SetBagSys();
        SetStores();
        SetTaskManager();
        SetPlayer();
        SetPlayerLaunchr();
        SetPlayerAni();
        SetGameover();
    }


    void Start()
    {
        if (isLoading) { LoadGame(); OpenOrCloseSettingUI(); }
       
    }



        // Update is called once per frame
        void Update()
    {
        if (SceneManager.GetActiveScene().name == "open") return;
        try
        {
            GameManager.isTalking = NPC.flowchart.GetBooleanVariable("isTalking");
        }
        catch { }

        if (Input.GetKeyDown(KeyCode.Q)&&!isInStore&&!isSettingUIShow)
        {
            OpenOrCloseUI();
        }
        if (Input.GetKeyDown(KeyCode.Escape) )
        {
            OpenOrCloseSettingUI();
        }

    }
    void OpenOrCloseUI() {
        isUIShow = !isUIShow;
        bag.SetActive(isUIShow);
        TaskUI.SetActive(isUIShow);
    }
    void OpenOrCloseSettingUI()
    {
        isSettingUIShow = !isSettingUIShow;
        SettingUI.SetActive(isSettingUIShow);
      //  CloseUI();
    }
    public void CloseUI()
    {
        isUIShow = false;
        bag.SetActive(false);
        TaskUI.SetActive(false);
    }

    public static bool IsTimePause() {
        if (isInStore) return true;
        if (isTalking) return true;
        if(isGameOver) return true;
        if (isTimePause) return true;
        if(isSettingUIShow) return true;
        return false;

    }
    public void ExitGame() {
        Application.Quit();

    }
    public  void LoadGame()
    {

        if (SceneManager.GetActiveScene().name == "open")
        {
            isLoading = true;

            SceneManager.LoadScene("player");
           SceneManager.LoadSceneAsync("dialog", LoadSceneMode.Additive);
        }
        else
        {
            isLoading = false;
            OpenOrCloseSettingUI();
            SavaAndLoad.saveload.LoadGame();
        }

    }


    public bool DestoryAllMonsters() {

        for (int i = Monsters.Count - 1; i >= 0; i--)
        {
            try { Monsters[i].gameObject.GetComponent<Drop>().dropOrNot = false; }
            catch { }
            Destroy(Monsters[i].gameObject);
        }
        Monsters.Clear();
        return true;
    }

    public bool DestoryAllItemsInScene()
    {

        for (int i = itemInScene.Count - 1; i >= 0; i--)
        {
            Destroy(itemInScene[i].gameObject);
        }
        itemInScene.Clear();
        return true;
    }
}
