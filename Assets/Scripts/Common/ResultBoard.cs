using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HorizontalGame;
public class ResultBoard : MonoBehaviour
{
    GameObject rank_Text;
    HorizontalGame.HorizontalGame GameManager;
    GameObject retryBtn, mainBtn;
    
    void Start()
    {
        SetResult();
        SetButtonEvent();
        CreateResult();
        SetRendererRtyBtn();
    }    
    void SetResult()
    {
        rank_Text = Resources.Load("Rank_Text") as GameObject;        

        retryBtn = gameObject.transform.Find("Retry_btn").gameObject;
        mainBtn = gameObject.transform.Find("Main_btn").gameObject;
        GameManager = GameObject.Find("GameManager").GetComponent<HorizontalGame.HorizontalGame>();        

        Singleton.singleton.UpdateScore(GameManager.score);
        Singleton.singleton.RankSort();
    }
    public void CreateResult()
    {
        int count,score;        
        float height = 210f;
        float margin = 50f;

        List<PlayerInfo> players = Singleton.singleton.GetPlayers(); // players copy

        count = players.Count > 10 ? 10 : players.Count;
        

        for (int i = 0; i < count; i++)
        {
            Text _rank = rank_Text.GetComponent<Text>();
            score = players[i].GetSelectPlayerScore(SceneKind.GetGameScene());
            Debug.Log("Result = " + SceneKind.GetGameScene());
            _rank.text = $"Rank : {i + 1}   Name : {players[i].PlayerName}   Score : {score}";
            _rank.GetComponent<RectTransform>().anchoredPosition = new Vector2(_rank.GetComponent<RectTransform>().anchoredPosition.x, height);
            Instantiate(rank_Text, gameObject.transform);
            height -= margin;
        }
    }
    void SetRendererRtyBtn()
    {
        if (!Singleton.singleton.CheckCoin())
        {
            Button btn = retryBtn.GetComponent<Button>();
            btn.interactable = false;
            Image sprite = retryBtn.GetComponent<Image>();
            sprite.color = new Color(1, 1, 1, 0.5f);
        }

    }
    void SetButtonEvent()
    {
        Button _retry, _main;
        Button[] buttons = new Button[2];
        
        _retry = retryBtn.GetComponent<Button>();
        _main = mainBtn.GetComponent<Button>();
                
        _main.onClick.AddListener(MainBtn);

        _retry.onClick.AddListener(ReTryBtn);
    }

    void ReTryBtn()
    {
        if (Singleton.singleton.CheckCoin())
        {
            Singleton.singleton.ConsumCoin();
            GameManager.CreateFade("in");            
            Destroy(gameObject);
        }
    }

    void MainBtn()
    {
        SceneKind.sceneNum = ESceneKind.menu;
        GameManager.CreateFade("in");
        Destroy(gameObject);
    }    
}
