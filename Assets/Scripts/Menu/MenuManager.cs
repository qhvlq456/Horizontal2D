using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HorizontalGame;
using UnityEngine.UI;
using System.Linq;

public class MenuManager : MonoBehaviour
{
    public GameObject _main,_alert, _setting;    
    public Stack<GameObject> menus = new Stack<GameObject>();
    public List<Button> buttons = new List<Button>();
    Transform _transform;

    private void Start()
    {        
        SetGame();
        CreateMainMenu();
        Singleton.singleton.Fade_Out(_transform);
    }

    void SetGame()
    {
        _transform = GameObject.Find("Main_cns").transform;

        _main = Resources.Load("Menu/MainMenu") as GameObject;
        _alert = Resources.Load("Menu/Alert_Panel") as GameObject;
        _setting = Resources.Load("Menu/Setting_Panel") as GameObject;        
    }
    public void CreateMainMenu()
    {
        menus.Push(Instantiate(_main, _transform));
    }
    public void CreateAlert()
    {
        DisableButton();
        menus.Push(Instantiate(_alert, _transform));
    }
    public void CreateSetting()
    {
        DisableButton();
        menus.Push(Instantiate(_setting, _transform));
    }
    public void PopMenu()
    {
        menus.Pop();
    }
    public void IsLogInMenu()
    {
        foreach (var menu in menus)
        {
            menu.GetComponent<Menu>().IsLogin(Singleton.singleton.player);
        }
    }
    public void IsLogOutMenu()
    {
        Singleton.singleton.LogOut();
        foreach (var menu in menus)
        {
            menu.GetComponent<Menu>().IsLogOut();
        }
    }
    public void UpdateText() // 일단 보류
    {
        foreach (var menu in menus)
        {
            menu.GetComponent<Menu>().UpdateMenu();
        }
    }
    public void DisableButton()
    {
        // 딱 뒤에 존재하는 오브젝트만 제거해야 한다        
        // create 이전        
        // 딱딱 짤라서 됬으면 좋을 것 같은데..        
        GameObject tmpObject = menus.Peek();
        buttons = tmpObject.transform.GetComponentsInChildren<Button>().ToList();

        foreach (var button in buttons) // 전 메뉴의 button을 disable시킴
        {
            button.interactable = false;
        }
    }
    public void EnableButton() // pop하기 전에 호출한다,,, // 방법은 setting destroy 시점에 초기화 작업 함수를 만들던가 아님 새로운 로직 짜는거 전자가 훨씬 좋음
    {
        foreach (var button in buttons)
        {
            button.interactable = true;
        }
    }
    public void InitAvailableButton() // setting destroy 후 사용 즉, setting pop 이후 초기화 작업임
    {
        GameObject tmpObject = menus.Peek();
        buttons = tmpObject.transform.GetComponentsInChildren<Button>().ToList();
        EnableButton();
    }
}
