﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
    {
    public static int CurrentStage = 3;
    public bool s = false;
      static public bool isTimePause = true;
       // public bool m_isTimePause = false;
        // Use this for initialization
        void Start()
        {
          //  isTimePause = m_isTimePause;

        }

        // Update is called once per frame
        void Update()
        {
        //   isTimePause = m_isTimePause;
        isTimePause = s;
        if (isTimePause) Time.timeScale = 0;
        }
    public void GameStart()
    {
        CurrentStage = 3;
        SceneManager.LoadScene("stage1");
        SceneManager.LoadSceneAsync("player", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("dialog", LoadSceneMode.Additive);
    }

}
