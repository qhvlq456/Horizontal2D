using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using HorizontalGame;
public class FlappyGameManager : HorizontalGame.HorizontalGame
{        
    void Start()
    {
        SetGame();
        Invoke("NextState", StaticVariable.introTime);
        Singleton.singleton.Fade_Out(_transform);
    }
    
    void Update()
    {
        LifeCycle();
        CurrentText();
    }

    void SetGame()
    {        
        gameName = "FlappyBird";        
        scoreText = GameObject.Find("score_Text").GetComponent<Text>();
        timeText = GameObject.Find("time_Text").GetComponent<Text>();
        lifeText = GameObject.Find("life_Text").GetComponent<Text>();
        _transform = GameObject.Find("Main_cns").GetComponent<Canvas>().transform;

        introCount = (int)StaticVariable.introTime;        
        _intro = Resources.Load("Intro") as GameObject;
        _result = Resources.Load("Result_Panel") as GameObject;
    }
    public override void EndCycle()
    {
        NextState();
        base.EndCycle();
        CreateResult();
    }



}
