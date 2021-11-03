using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HorizontalGame;
public class MainMenu : Menu
{
    MenuManager menumManager;
    Transform _transform;
    Button _flappy, _angry,_coin, _setting;
    void Start()
    {        
        SetGame();
        SetButtonEvent();
        IsLogin(Singleton.singleton.player);
    }
    private void Update()
    {
        UpdateMenu();
    }
    void SetGame()
    {        
        titleText = GameObject.Find("Name_Text").GetComponent<Text>();
        bodyText = GameObject.Find("Coin_Text").GetComponent<Text>();

        menumManager = GameObject.Find("UIManager").GetComponent<MenuManager>();
        _transform = GameObject.Find("Main_cns").transform;

        // find button
        _flappy = GameObject.Find("Flappy_btn").GetComponent<Button>();
        _angry = GameObject.Find("Angry_btn").GetComponent<Button>();
        _coin = GameObject.Find("Coin_btn").GetComponent<Button>();
        _setting = GameObject.Find("Setting_btn").GetComponent<Button>();
    }
    void SetButtonEvent()
    {
        _flappy.onClick.AddListener(() => MoveScene((int)SceneNum.flappy));
        _angry.onClick.AddListener(() => MoveScene((int)SceneNum.angry));
        _setting.onClick.AddListener(() => menumManager.CreateSetting());
        _coin.onClick.AddListener(AddCoinBtn);         
    }
    public override void UpdateMenu()
    {        
        if (!isLogin)
        {
            titleText.text = "Name : ";
            bodyText.text = "Coin : ";
            return;
        }
        titleText.text = "Name : " + player.nickName;
        bodyText.text = "Coin : " + player.coin;
    }

    public void CreateSetting()
    {
        menumManager.CreateSetting();
    }

    public void MoveScene(int sceneNum)
    {
        if (!Singleton.singleton.isLogin) return;
        if (!Singleton.singleton.CheckCoin()) return;

        Singleton.singleton.ConsumCoin();
        Singleton.singleton.sceneNum = (SceneNum)sceneNum;
        Singleton.singleton.Fade_In((SceneNum)sceneNum);
    }

    public void AddCoinBtn() // Please Text Create!! Alert String Change!!
    {
        Singleton.singleton.AddCoin();
    }
}
