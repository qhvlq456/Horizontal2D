using UnityEngine;
using UnityEngine.UI;
using HorizontalGame;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public enum LoginError { none, success, empty, fail, create, exist, logout }
public class AlertMenu : Menu
{    
    MenuManager menuManager;
    Button okBtn;
    public int errorCode { private get; set; }
    int kindLength;
    string[] titles;
    string[] bodys;
    public AlertMenu()
    {
        errorCode = -1;
        kindLength = Enum.GetValues(typeof(LoginError)).Length;
        titles = new string[kindLength];
        bodys = new string[kindLength];
        InitString();
    }
    private void Start()
    {
        errorCode = (int)Singleton.singleton.loginCode;
        //Debug.Log(Singleton.singleton.loginCode);
        SetGame();
        SetButtonEvent();        
    }
    private void Update()
    {
        UpdateMenu();
    }
    void SetGame()
    {
        titleText = GameObject.Find("Alert_Title_Text").GetComponent<Text>();
        bodyText = GameObject.Find("Alert_Body_Text").GetComponent<Text>();
        menuManager = GameObject.Find("UIManager").GetComponent<MenuManager>();

        // find button
        okBtn = transform.Find("Alert_Exit_btn").GetComponent<Button>();
    }
    void InitString()
    {
        int num = 0;
        for (int i = 0; i < kindLength; i++)
        {
            titles[i] = ((LoginError)num).ToString();
            if (titles[i] == LoginError.success.ToString())
            {
                bodys[i] = "Plase Enter Game!!";
            }
            else if (titles[i] == LoginError.empty.ToString())
            {
                bodys[i] = "Please Check Input Field..";
            }
            else if (titles[i] == LoginError.fail.ToString())
            {
                bodys[i] = "Not found your ID..";
            }
            else if (titles[i] == LoginError.create.ToString())
            {
                bodys[i] = "Create your ID..!!";
            }
            else if (titles[i] == LoginError.exist.ToString())
            {
                bodys[i] = "The ID that already exists!!";
            }
            else if (titles[i] == LoginError.logout.ToString())
            {
                bodys[i] = "Success Logout!!";
            }
            num = (num + 1) % kindLength;
        }
    }
    void SetButtonEvent()
    {
        okBtn.onClick.AddListener(DestroyAlert);
    }
    public void DestroyAlert()
    {
        menuManager.EnableButton();
        menuManager.PopMenu();
        Destroy(gameObject);
    }

    public override void UpdateMenu()
    {
        if (errorCode < 0) return;

        titleText.text = titles[errorCode];
        bodyText.text = bodys[errorCode];
    }

}
