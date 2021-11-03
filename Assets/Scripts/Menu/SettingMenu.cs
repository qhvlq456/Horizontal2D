using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HorizontalGame;

public class SettingMenu : Menu
{
    public InputField input;
    public MenuManager menuManager;

    public Button createBtn;
    public Button verifyBtn;
    public Button logoutBnt;
    public Button changeBtn;
    public Button exitBtn;
    bool isKind;
    public override void UpdateMenu()
    {        
        if (!isLogin)
        {            
            titleText.text = "Name : ";
            bodyText.text = "Flappy Score : ";
            return;
        }        
        titleText.text = "Name : " + player.nickName;
        bodyText.text = (isKind == true) ? "Flappy Score : " + player.FlappybestScore.ToString() :
            "Angry Score : " + player.AngrybestScore.ToString();
    }
    
    void Start()
    {
        IsLogin(Singleton.singleton.player);
        SetGame();
        SetButtonEvent();        
    }
    
    void Update()
    {        
        UpdateMenu();
    }

    void SetGame()
    {
        input = transform.Find("InputField").GetComponent<InputField>();
        menuManager = GameObject.Find("UIManager").GetComponent<MenuManager>();

        // find Text
        titleText = GameObject.Find("Setting_NameInfo_Text").GetComponent<Text>();
        bodyText = GameObject.Find("Setting_ScoreInfo_Text").GetComponent<Text>();

        // button find        
        createBtn = gameObject.transform.Find("Setting_CreateInfo_btn").GetComponent<Button>();
        verifyBtn = gameObject.transform.Find("Setting_VerifyInfo_btn").GetComponent<Button>();
        changeBtn = gameObject.transform.Find("Setting_ChangeInfo_btn").GetComponent<Button>();
        exitBtn = gameObject.transform.Find("Setting_ExitInfo_btn").GetComponent<Button>();
        logoutBnt = gameObject.transform.Find("Setting_LogOut_btn").GetComponent<Button>();

        // default text
        titleText.text = titleText.text = "Name : ";
        bodyText.text = "Flappy Score : ";

        isKind = true;
        
    }
    void SetButtonEvent()
    {        
        createBtn.onClick.AddListener(CreateBtn);
        verifyBtn.onClick.AddListener(VerifyBtn);
        changeBtn.onClick.AddListener(ChangeBtn);
        exitBtn.onClick.AddListener(() => {
            menuManager.PopMenu();
            menuManager.InitAvailableButton(); // 나머지는 반대임
            Destroy(gameObject);
            });
        logoutBnt.onClick.AddListener(LogOutBtn);
    }
    public void LogOutBtn() // error
    {
        Singleton.singleton.LogOut();
        menuManager.IsLogOutMenu();
        menuManager.CreateAlert();
    }
    public void VerifyBtn() // alert를 각 메뉴에서 관리한다면?? // 여기에 넣는가 맞다
    {
        Singleton.singleton.SearchMyInfo(input.text);
        menuManager.IsLogInMenu();
        menuManager.CreateAlert(); // 이것도 한번 설계해보자!!
    }
    public void CreateBtn()
    {
        Singleton.singleton.OnCreatePlayer(input.text);
        menuManager.CreateAlert();
    }
    public void ChangeBtn()
    {
        isKind = !isKind;
    }    

}
