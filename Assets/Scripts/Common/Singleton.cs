using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HorizontalGame;

public enum SceneNum { logo, menu, flappy, angry }
public class Singleton : MonoBehaviour
{
    public static Singleton singleton = null;
    
    [Range(0,150)]
    [ContextMenuItem("Default Value","SetDefaultValue")]
    public int CONSUM_COIN, ADD_COIN, DEFAULT_COIN;
    public int playerIdx { get; private set; }
    public bool isLogin { get; private set; }
    public bool isFade;
    public SceneNum sceneNum;
    public LoginError loginCode;

    public GameObject fade_Prefebs,_fade;
    public PlayerInfo player;
    List<PlayerInfo> players;

    void SetDefaultValue()
    {
        CONSUM_COIN = 20;
        ADD_COIN = 10;
        DEFAULT_COIN = 100;
    }
    private void Awake()
    {
        if(singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(singleton != this)
        {
            Destroy(gameObject);
        }        
    }
    void Start()
    {
        //Debug.Log($"{((int)SceneNum.flappy).ToString() + '1'}");
        SetInit();
        OnStartGetPlayer();
    }    
    void SetInit()
    {
        fade_Prefebs = Resources.Load("Fade") as GameObject;
        loginCode = LoginError.none;
        sceneNum = 0;
        isLogin = false;
        player = null;
        players = null;
    }

    // verify or create player serarch
    public void SearchMyInfo(string input)
    {
        if (isLogin) return;
        if (GetPlayer(input))
        {
            isLogin = true;
            loginCode = LoginError.success;
        }
        else
        {
            if (string.IsNullOrEmpty(input)) loginCode = LoginError.empty;
            else loginCode = LoginError.fail;
        }
    }
    // create player
    public void OnCreatePlayer(string input)
    {
        if (isLogin) return;
        if (GetPlayer(input))
        {
            loginCode =  LoginError.exist;
        }
        else
        {
            if (string.IsNullOrEmpty(input)) loginCode = LoginError.empty;
            else // create player
            {
                player = new PlayerInfo(input, 0, 0, DEFAULT_COIN.ToString());

                if (players == null) players = new List<PlayerInfo>();

                players.Add(player);
                SetPlayerScore(0, (int)SceneNum.flappy); SetPlayerScore(0, (int)SceneNum.angry);
                SetPalyerCoin(DEFAULT_COIN);
                SetPlayersPrefs(); // players에 붙어주기
                loginCode = LoginError.create;
            }
        }        
    }        
    // Score update
    public void UpdateScore(int score) // Score 갱신되었을때이다.. 즉 bestscore가 바뀌어 Rank가 바뀌게 되는 순간이다 ,,,, 많이 건들어야됨
    {
        GetPlayerIdx();
        switch (sceneNum) // 이 분기가 너무 맘에 안든다....
        {
            case SceneNum.flappy: // staticvariable 사용
                {
                    player.FlappybestScore = player.FlappybestScore < score ? score : player.FlappybestScore; // 나중에 인덱스 처리할거;;
                    players[playerIdx].FlappybestScore = player.FlappybestScore;
                    SetPlayerScore(player.FlappybestScore, (int)sceneNum);
                }
                break;
            case SceneNum.angry:
                {
                    player.AngrybestScore = player.AngrybestScore < score ? score : player.AngrybestScore;
                    players[playerIdx].AngrybestScore = player.AngrybestScore;
                    SetPlayerScore(player.AngrybestScore, (int)sceneNum);
                }
                break;                
        }
        //Debug.Log($"set : Flappy bird : {player.FlappybestScore}   Angry bird  : {player.AngrybestScore}");
    }
    // coin update    
    public void ConsumCoin()
    {        
        player.coin -= CONSUM_COIN;
        SetPalyerCoin(player.coin);
    }
    public void AddCoin()
    {
        if (!isLogin) return;

        player.coin += ADD_COIN;
        SetPalyerCoin(player.coin);
    }
    public bool CheckCoin() // Coin 갱신되었을때이다.. 즉 bestscore가 바뀌어 Rank가 바뀌게 되는 순간이다 ,, dim처리를 위해 bool선언
    {
        if (!isLogin) return false;
        if (player.coin < CONSUM_COIN) return false;        
        return true;
    }
    // Load player info
    void OnStartGetPlayer() // 1번째  .. 살짝 건들어야됨
    {
        string s = PlayerPrefs.GetString("Player", "");
        
        string[] temp = null;

        if (s != "") temp = s.Split(new char[] { '/' });

        if (temp == null) return;

        players = new List<PlayerInfo>();
        foreach (var i in temp)
        {
            if (i != "")
            {
                players.Add(new PlayerInfo(i, PlayerPrefs.GetInt(((int)SceneNum.flappy).ToString() + i, 0),
                    PlayerPrefs.GetInt(((int)SceneNum.angry).ToString() + i, 0),
                    PlayerPrefs.GetString(i, DEFAULT_COIN.ToString()))); // value가 존재하지않는다면 default value를 내가 정한 기본값으로 해준다                            
            }
        }        
    }
    bool GetPlayer(string input)
    {
        if (players == null) return false; // false일수 밖에 없음 데이터가 아에 없다는 얘기니깐

        foreach (var item in players)
        {
            if(item.nickName == input)
            {
                player = null;
                PlayerInfo _player = new PlayerInfo(item.nickName, item.FlappybestScore, item.AngrybestScore, item.coin.ToString());
                player = _player.DeepCopy();
                return true;
            }
        }
        return false;
    }
    public List<PlayerInfo> GetPlayers()
    {
        List<PlayerInfo> list = players.ToList();
        return list;
    }
    void GetPlayerIdx()
    {
        playerIdx = players.FindIndex(a => a.nickName == player.nickName);
    }    

    // save player info
    void SetPlayerScore(int value, int kind)
    {
        PlayerPrefs.SetInt(kind.ToString() + player.nickName, value);        
    }
    void SetPalyerCoin(int value)
    {
        PlayerPrefs.SetString(player.nickName, value.ToString());        
    }
    void SetPlayersPrefs()
    {        
        string s = "";
        for (int i = 0; i < players.Count - 1; i++)
            s += players[i].nickName == "" ? "" : players[i].nickName + '/';
        s += players[players.Count - 1].nickName;
        PlayerPrefs.SetString("Player", s);
        ////
        //PlayerPrefs.SetInt('F' + nickName, FbestScore); PlayerPrefs.SetInt('A' + nickName, AbestScore); PlayerPrefs.SetString(nickName, coin.ToString()); // 계속 바꾸지말고 게임 종료시 바꾸자
        // 내생각엔 각자 할당을 해줘서 그냥 update 파트를 나눠주는게 좋을것 같다고 생각햇뜸
    }
    public void SetRankSort() // 자주 사용해야함 ,, 건들게 없음
    {
        if (sceneNum == SceneNum.flappy) players.Sort((a, b) => (a.FlappybestScore > b.FlappybestScore) ? -1 : 1); // Flappy
        else if (sceneNum == SceneNum.angry) players.Sort((a, b) => (a.AngrybestScore > b.AngrybestScore) ? -1 : 1); // Angry
        GetPlayerIdx();
    }

    public void LogOut()
    {
        if (!isLogin)
        {
            return;
        }
        SetPlayersPrefs();
        SetInit();
        OnStartGetPlayer();
        isLogin = false;
        loginCode = LoginError.logout;
    }

    ////// Fade State
    public void Fade_Out(Transform _transform)
    {
        if (isFade) return;

        _fade = Instantiate(fade_Prefebs, _transform);
        _fade.GetComponent<Fade>().SetFadeType(1);
        Invoke("OnFalseIsFade", StaticVariable.fadeTime);
    }
    public void Fade_In(SceneNum scene)
    {
        if (isFade) return;

        sceneNum = scene;
        _fade.GetComponent<Fade>().SetFadeType(0);
        Invoke("OnFalseIsFade", StaticVariable.fadeTime);
    }

    void OnFalseIsFade()
    {
        isFade = false;
    }
    

}
