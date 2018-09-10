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
    void Start () {
        toggleHeight = taskToggle.GetComponent<RectTransform>().sizeDelta.y;
        AddTask("完成新手教学");
        AddTask("获得火之魂");
        AddTask("获得冰之魂");
        InitialAllTaskUI();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F1)) CompleteTask(0);
        if (Input.GetKeyDown(KeyCode.F2)) CompleteTask(1);
        if (Input.GetKeyDown(KeyCode.F3)) CompleteTask(2);

    }
    public void AddTask(string str) {
        taskList.Add(new Task(taskList.Count, str));
    }
    public void AddTaskWithUpdateUI(string str)
    {
        taskList.Add(new Task(taskList.Count, str));
        Instantiate(taskToggle).transform.SetParent(taskUI);
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
            taskUI.GetChild(n).transform.localPosition = new Vector2(0, -taskList[n].num * toggleHeight);
 
            taskUI.GetChild(n).GetChild(1).GetComponent<Text>().text = taskList[n].taskText;
            taskUI.GetChild(n).GetComponent<Toggle>().isOn = taskList[n].isCompleted;
    }

    public void UpdateTaskUIBackground() {
        taskUIHeight = taskList.Count * toggleHeight;
        taskUI.GetComponent<RectTransform>().sizeDelta = new Vector2(100, taskUIHeight);
    }
    public void InitialAllTaskUI() {
        UpdateTaskUIBackground();
        foreach (Task o in taskList) {
            Instantiate(taskToggle).transform.SetParent(taskUI);
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
