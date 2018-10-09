using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TaskManager : MonoBehaviour {

    // Use this for initialization
    public Toggle taskToggle;
    public static List<Task> taskList=new List<Task>();
    private float toggleHeight;
    private float taskUIHeight;
    public Transform taskUI;
    public static TaskManager taskManager;
    void Start () {
        taskManager = this;
        toggleHeight = taskToggle.GetComponent<RectTransform>().sizeDelta.y;
    //    toggleHeight = (toggleHeight / 1280) * Screen.width;
        AddTask("获得地之魂");
        AddTask("获得火之魂");
        AddTask("获得冰之魂");
        AddTask("打倒BOSS");
        InitialAllTaskUI();
        if (GameManager.GM.hasTeleport) CompleteTask(0);
        if (GameManager.GM.hasFire) CompleteTask(1);
        if (GameManager.GM.hasIce) CompleteTask(2);
    }
	
	// Update is called once per frame
	void Update () {

    }
    public void AddTask(string str) {
        taskList.Add(new Task(taskList.Count, str));
    }
    public void AddTaskWithUpdateUI(string str)
    {
        taskList.Add(new Task(taskList.Count, str));
        Toggle o = Instantiate(taskToggle);
       
        o.transform.SetParent(taskUI);
        o.transform.localScale = Vector3.one;
        UpdateTaskUI(taskList.Count);
    }
    public void CompleteTask(int num)
    {
        if (num < taskList.Count)
        {
            taskList[num].isCompleted = true;
            UpdateTaskUI(num);
        }
    }
    public void CompleteTask(int num,bool s)
    {
        if (num < taskList.Count)
        {
            taskList[num].isCompleted = s;
            UpdateTaskUI(num);
        }
    }
    public void UpdateTaskUI(int n)
    {
        if (n >= taskList.Count) return;
            taskUI.GetChild(n).transform.position = new Vector3(0, -taskList[n].num * toggleHeight / 1280 * Screen.width,0)+ taskUI.position;
 
            taskUI.GetChild(n).GetChild(1).GetComponent<Text>().text = taskList[n].taskText;
            taskUI.GetChild(n).GetComponent<Toggle>().isOn = taskList[n].isCompleted;
    }

    public void UpdateTaskUIBackground() {
        taskUIHeight = taskList.Count * toggleHeight;
        taskUI.GetComponent<RectTransform>().sizeDelta = new Vector2(300, taskUIHeight);
    }
    public void InitialAllTaskUI() {
        UpdateTaskUIBackground();
        foreach (Task o in taskList) {
            Toggle oj = Instantiate(taskToggle);
             oj.transform.SetParent(taskUI);
            oj.transform.localScale = Vector3.one;
            UpdateTaskUI(o.num);
        }
    }
}

public class Task {
   public int num;
    public string taskText;
    public bool isCompleted=false;
    public Task(int n,string text) {
        num = n;
        taskText = text;

    }

}
