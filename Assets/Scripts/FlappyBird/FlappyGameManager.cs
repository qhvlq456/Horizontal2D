using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FlappyGameManager : HorizontalGame.HorizontalGame
{        
    public override void Awake() {
        SetGame();    
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
        canvas = GameObject.Find("Main_cns").GetComponent<Canvas>().transform;

        introCount = (int)StaticVariable.introTime;
    }
    public override void EndCycle()
    {
        NextGameState();
        base.EndCycle();
        CreateResult();
    }



}
