﻿using HorizontalGame;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AngryGameManager : HorizontalGame.HorizontalGame
{
    AngryObstacle obstacle;
    ObjectCamera _objectCamera;
    
    Transform startBallpos;

    GameObject ball_Prefebs, _ball;

    float leftTime;
    public int _birdCount, tempScore;
    float[] gameStateArr = new float[] 
    {StaticVariable.introTime, StaticVariable.startTime, StaticVariable.endTime };
    void Start()
    {
        SetGame();
        Singleton.singleton.Fade_Out(_transform);
    }

    void Update()
    {        
        LifeCycle();
        CurrentText();
        NextGameState();
    }

    void SetGame()
    {
        gameName = "AngryBird";
        startBallpos = GameObject.Find("CatapultPivot").gameObject.transform;
        _transform = GameObject.Find("Main_cns").GetComponent<Canvas>().transform;
        scoreText = GameObject.Find("score_Text").GetComponent<Text>();
        timeText = GameObject.Find("time_Text").GetComponent<Text>();
        lifeText = GameObject.Find("life_Text").GetComponent<Text>();
        _objectCamera = GameObject.Find("Main Camera").GetComponent<ObjectCamera>();
        introCount = (int)StaticVariable.introTime;

        obstacle = GetComponent<AngryObstacle>();

        _intro = Resources.Load("Intro") as GameObject;
        _result = Resources.Load("Result_Panel") as GameObject;
        ball_Prefebs = Resources.Load("Angry/Ball") as GameObject;

        _birdCount = StaticVariable.birdCount;
        _ball = null;
    }

    public override void IntroCycle()
    {
        _objectCamera.SetInit();
        ObstacleBundle();
        CreateBall();
    }
    public override void EndCycle()
    {
        base.EndCycle();
        if (isGameOver) CreateResult();
        else
        {
            InitGame();
            DeleteBall();
        }
    }
    public void MinusLife()
    {
        if (_birdCount > 0) UpdateLife();
    }
    public void AddScore(int _score)
    {
        _birdCount--;
        if (_birdCount <= 0)
        {
            UpdateScore(tempScore);
        }
        else
            tempScore += _score * (_birdCount + 1);
    }
    void InitGame()
    {
        if (leftTime == 0)
            MinusLife();
        isIntro = true;
    }
    void NextGameState()
    {
        leftTime += Time.deltaTime;
        if (leftTime < gameStateArr[currentStateIdx]) return;
        NextState();
        leftTime = 0f;
    }
    public void OnPassStart()
    {
        NextState();
        leftTime = 0f;
    }
    void CreateBall()
    {
        if (_ball != null) return; // 이미 존재한다면            
        _ball = Instantiate(ball_Prefebs, startBallpos);
    }
    public void DeleteBall()
    {
        if (_ball == null) return; // ball을 찾지못하면 리턴 예외처리
        Destroy(_ball);
        _ball = null;
    }
    void ObstacleBundle()
    {
        obstacle.StartObstacle();
        _birdCount = StaticVariable.birdCount;
        tempScore = 0;
    }
}
