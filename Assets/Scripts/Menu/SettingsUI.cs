using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HorizontalGame;

public class SettingsUI : Menu
{
    Animator animator;
    InputField input;
    [SerializeField]
    Button createBtn;
    [SerializeField]
    Button verifyBtn;
    [SerializeField]
    Button logoutBnt;
    [SerializeField]
    Button changeBtn;
    [SerializeField]
    Button backBtn;
    int changeButtonIndex = 0;
    string tempStr = "";
    public override void Awake()
    {
        SetSettings();
        IsLogin(Singleton.singleton.player);
    }
    
    public override void Start()
    {        
    }
    
    void Update()
    {        
        UpdateMenu();
    }
    private void OnDisable() {
        input.text = "";
    }
    void SetSettings()
    {
        animator = GetComponent<Animator>();
        input = transform.Find("InputField").GetComponent<InputField>();

        // find Text
        titleText = GameObject.Find("SettingsInfoPanel").transform.GetChild(0).GetComponent<Text>();
        bodyText = GameObject.Find("SettingsInfoPanel").transform.GetChild(1).GetComponent<Text>();

        // default text
        titleText.text = titleText.text = "Name : ";
        bodyText.text = "Flappy Score : ";
    }
    public override void UpdateMenu()
    {        
        Debug.Log($"login = {isLogin}");
        if (!isLogin)
        {            
            titleText.text = "Name : ";
            bodyText.text = "Flappy Score : ";
            return;
        }        
        titleText.text = "Name : " + player.PlayerName;
        SelectChangeScore();
    }
    void SelectChangeScore()
    {
        string kind = ((ESceneKind)(changeButtonIndex + 2)).ToString();
        tempStr = kind + " Score : " + Singleton.singleton.player.PlayerScore[changeButtonIndex];

        bodyText.text = tempStr;
    }
    
    public void OnClickChangeScoreTextButton()
    {
        if(!isLogin) return;
        
        changeButtonIndex = (changeButtonIndex + 1) % Singleton.singleton.player.PlayerScore.Count;
        SelectChangeScore();
    }

    public void OnClickCloseButton()
    {
        StartCoroutine(CloseAfterDelay());
    }
    IEnumerator CloseAfterDelay()
    {
        animator.SetTrigger("Close");
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        animator.ResetTrigger("Close");
    }
    public void OnClickVerifyButton()
    {        
        List<Menu> menus = FindObjectsOfType<Menu>().ToList();

        Singleton.singleton.SearchPlayer(input.text);
        foreach (var menu in menus)
        {
            menu.GetComponent<Menu>().IsLogin(Singleton.singleton.player);
        }
    }
    public void OnClickLogoutButton()
    {
        List<Menu> menus = FindObjectsOfType<Menu>().ToList();

        Singleton.singleton.Logout();
        foreach (var menu in menus)
        {
            menu.GetComponent<Menu>().IsLogOut();
        }
    }
    public void OnClickCreateButton()
    {
        Singleton.singleton.CreatePlayer(input.text);
    }
    void ClickBundleEvent()
    {

    }

}
