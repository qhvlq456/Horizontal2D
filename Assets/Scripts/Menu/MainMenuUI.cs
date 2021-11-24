using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HorizontalGame;
public class MainMenuUI : Menu
{
    public override void Awake()
    {
        SetMainMenu();
        IsLogin(Singleton.singleton.player);
    }
    public override void Start()
    {        
        base.Start();
    }
    private void Update()
    {
        UpdateMenu();
    }
    void SetMainMenu()
    {        
        titleText = GameObject.Find("PlayerInfoPanel").transform.GetChild(0).GetComponent<Text>();
        bodyText = GameObject.Find("PlayerInfoPanel").transform.GetChild(1).GetComponent<Text>();
        
        canvas = GameObject.Find("Canvas").transform;
    }    
    public override void UpdateMenu()
    {        
        if (!isLogin)
        {
            titleText.text = "Name : ";
            bodyText.text = "Coin : ";
            return;
        }
        titleText.text = "Name : " + player.PlayerName;
        bodyText.text = "Coin : " + player.PlayerCoin;
    }    

    public void OnClickQuitButton()
    {
        Singleton.singleton.Logout();
        
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else 
            Application.Quit();
        #endif
    }
    public void OnClickMoveScene(int sceneNum)
    {
        if (!Singleton.singleton.isLogin) return;
        if (!Singleton.singleton.CheckCoin()) return;

        Singleton.singleton.ConsumCoin();
        SceneKind.sceneNum = (ESceneKind)sceneNum;
        CreateFade("in");
    }

    public void OnClickAddCoinButton() // Please Text Create!! Alert String Change!!
    {
        Singleton.singleton.AddCoin();
    }
}
