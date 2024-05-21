using NUnit.Framework.Internal.Execution;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Ui : MonoBehaviour{
    public UIDocument document;
    public Label health, health1, health2, time, time_game, status_game;
    private float startTime;
    private Button play_again, exit;
    public VisualElement menu;
    public bool game=true;

    private void OnEnable() {
        document=GetComponent<UIDocument>();
        VisualElement root=document.rootVisualElement;

        health=root.Q<Label>("health");
        health1=root.Q<Label>("health1");
        health2=root.Q<Label>("health2");
        time=root.Q<Label>("time");
        time_game=root.Q<Label>("time_game");
        status_game=root.Q<Label>("status_game");
        play_again=root.Q<Button>("play_again");
        exit=root.Q<Button>("exit");
        menu=root.Q<VisualElement>("menu");

        startTime=Time.time;

        play_again.RegisterCallback<ClickEvent>(play_again_game);
        exit.RegisterCallback<ClickEvent>(exit_game);
    }

    private void Update() {
        float t = Time.time-startTime;

        string minutes = ((int) t/60).ToString("00");
        string seconds = (t%60).ToString("00");
        string milliseconds = ((t*1000)%1000).ToString("000");

        if(game){
            time.text=minutes+":"+seconds+":"+milliseconds;
        }
    }

    public void visible(int id){
        switch(id){
            case 1:
                health.style.display=DisplayStyle.None;
            break;

            case 2:
                health1.style.display=DisplayStyle.None;
            break;

            case 3:
                health2.style.display=DisplayStyle.None;
            break;
        }
    }

    private void exit_game(ClickEvent evt){
        Application.Quit();
    }

    private void play_again_game(ClickEvent evt){
        Time.timeScale=1;
        SceneManager.LoadSceneAsync(0);
    }
}
