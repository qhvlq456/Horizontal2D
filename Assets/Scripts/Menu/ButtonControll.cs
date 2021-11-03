using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HorizontalGame;

// 이걸 바꿔야 한다
public class ButtonControll : MonoBehaviour
{    
    Transform _transform;
    GameObject alert_prefebs;
    GameObject setting_prefebs;
    GameObject _tempObject;

    Button flappyBtn;
    Button angryBtn;
    Button addcoinBtn;

    public List<Button> buttons;
    List<Menu> subjects = new List<Menu>();
    void Start()
    {        
        SetInit();
        Singleton.singleton.Fade_Out(_transform);
    }
    
    void SetInit()
    {
        _tempObject = null;
        _transform = GameObject.Find("Main_cns").transform;

        flappyBtn = GameObject.Find("Flappy_btn").GetComponent<Button>();
        angryBtn = GameObject.Find("Angry_btn").GetComponent<Button>();
        addcoinBtn = GameObject.Find("Coin_btn").GetComponent<Button>();
        buttons.Add(flappyBtn); buttons.Add(angryBtn); buttons.Add(addcoinBtn);

        alert_prefebs = Resources.Load("Menu/Alert_Panel") as GameObject;
        setting_prefebs = Resources.Load("Menu/Setting_Panel") as GameObject;        
    }
    public void AddMenu(Menu menu)
    {
        if (subjects.Contains(menu))
        {
            Debug.Log("exist is menu!!");
        }
        else
        {
            subjects.Add(menu);
        }

    }
    public void RemoveMenu(Menu menu)
    {
        if (subjects.Contains(menu))
        {
            subjects.Remove(menu);
        }
        else
        {
            Debug.Log("not exist is menu!!");
        }
    }
        
    void IsLogin()
    {
        foreach (var item in subjects)
        {
            item.IsLogin(Singleton.singleton.player);
            item.UpdateMenu();
        }        
    }
    public void IsLogOut()
    {
        Singleton.singleton.LogOut();
        foreach (var item in subjects)
        {
            item.IsLogOut();
            item.UpdateMenu();
        }
        CreateAlert();
    }
    public void VerifyBtn(string str) // alert를 각 메뉴에서 관리한다면?? // 여기에 넣는가 맞다
    {                
        Singleton.singleton.SearchMyInfo(str);
        IsLogin(); // 만약 위 함수가 성공하게 된다면 singleton에선 islogin은 true가 된다
        CreateAlert(); // 이것도 한번 설계해보자!!
    }    
    public void CreateBtn(string str)
    {
        Singleton.singleton.OnCreatePlayer(str);
        CreateAlert();
    }    
    public void AddCoinBtn() // Please Text Create!! Alert String Change!!
    {
        Singleton.singleton.AddCoin();        
    }
    public void MoveSecen(int idx)
    {
        if (!Singleton.singleton.isLogin) return;
        if (!Singleton.singleton.CheckCoin()) return;
        Singleton.singleton.ConsumCoin();
        Singleton.singleton.sceneNum = (SceneNum)idx;
        Singleton.singleton.Fade_In(Singleton.singleton.sceneNum);        
    }    
    public void CreateSetting()
    {
        if(_tempObject == null)
        {
            _tempObject = Instantiate(setting_prefebs, _transform);
            SetDisableBtn();
        }            
        else
        {
            //RemoveMenu(_tempObject.GetComponent<Setting>());
            Destroy(_tempObject);
            _tempObject = null;
            SetEnableBtn();
        }
    }
    public void SetDisableBtn() // buttons.Add(flappyBtn); buttons.Add(angryBtn); buttons.Add(addcoinBtn); 인증되면 이것들이 이제 풀리는구나;;
    {
        foreach (var button in buttons)
        {
            button.interactable = false;
        }
    }
    public void SetEnableBtn()
    {
        foreach (var button in buttons)
        {
            button.interactable = true;
        }
    }    
    public void CreateAlert()
    {
        Instantiate(alert_prefebs, _transform);
    }    
}
