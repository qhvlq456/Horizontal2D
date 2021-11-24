using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HorizontalGame;
public class Singleton : MonoBehaviour
{
    public static Singleton singleton = null;
    
    [Range(0,150)]
    [ContextMenuItem("Default Value","SetDefaultValue")]
    public int CONSUM_COIN, ADD_COIN, DEFAULT_COIN;
    public int playerIdx; //{ get; private set; }
    public bool isLogin; //  { get; private set; }
    public PlayerInfo player;
    public List<PlayerInfo> players = new List<PlayerInfo>();

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
        SetLoadManager();
        LoadPlayers();
    }
    void SetLoadManager()
    {
        isLogin = false;
    }
    // Load players info
    public void LoadPlayers()
    {
        string str = PlayerPrefs.GetString("Player", "");
        
        string[] playersName = null;
        Debug.Log($"str = {str}");

        if (str != "") playersName = str.Split(new char[] { '/' });

        if (playersName == null) return;

        foreach (var playerName in playersName)
        {
            if(playerName == "" || playerName == null) continue;

            PlayerInfo newPlayer = new PlayerInfo();
            newPlayer.PlayerName = playerName;
            newPlayer.PlayerCoin = PlayerPrefs.GetInt(playerName + "coin");

            List<int> newPlayerScore = new List<int>();
            for(int i = 0; i < SceneKind.gameKindNum; i++)
            {
                newPlayerScore.Add(PlayerPrefs.GetInt(playerName + i,0));
            }
            newPlayer.PlayerScore = newPlayerScore.ToList();

            players.Add(newPlayer);
        }   
    }
    // load myPlayer info
    bool LoadPlayer(string input)
    {
        if (players.Count <= 0) return false; // false일수 밖에 없음 데이터가 아에 없다는 얘기니깐

        foreach (var _player in players)
        {
            if(_player.PlayerName == input)
            {
                PlayerInfo newPlayer = _player;
                player = newPlayer.DeepCopy();
                return true;
            }
        }
        return false;
    }
    public void RankSort()
    {
        players.Sort((a, b) => 
        (a.PlayerScore[SceneKind.GetGameScene()] > b.PlayerScore[SceneKind.GetGameScene()] ? -1 : 1));
        GetPlayerIndex();
    }
    // verify or create player serarch
    public void SearchPlayer(string input)
    {
        if (isLogin) return;
        if (LoadPlayer(input))
        {
            isLogin = true;
            //loginCode = ELoginErrorType.success;
        }
        else
        {
            // if (string.IsNullOrEmpty(input)) //loginCode = ELoginErrorType.empty;
            // else loginCode = ELoginErrorType.fail;
        }
    }    
    // create player
    public bool CreatePlayer(string input) // 수정 필요
    {
        if(string.IsNullOrEmpty(input)) return false;

        var _player = players.Select(x => x.PlayerName == input) as PlayerInfo;
        Debug.Log($"player name = {_player.playerName}");

        if(players.Contains(_player)) return false;

        PlayerInfo newPlayer = new PlayerInfo(input,DEFAULT_COIN,0,0,0,0,0,0);
        SetPlayersPrefs(newPlayer);
        return true;
    }

    // Score update
    public void UpdateScore(int score) // Score 갱신되었을때이다.. 즉 bestscore가 바뀌어 Rank가 바뀌게 되는 순간이다 ,,,, 많이 건들어야됨
    {
        GetPlayerIndex();

        players[playerIdx].SetSelectPlayerScore(score);
        //playerScore[(int)SceneKind.sceneNum] = score;
        //Debug.Log($"set : Flappy bird : {player.FlappybestScore}   Angry bird  : {player.AngrybestScore}");
    }
    // coin update    
    public void ConsumCoin()
    {        
        player.PlayerCoin -= CONSUM_COIN;
    }
    public void AddCoin()
    {
        if (!isLogin) return;

        player.PlayerCoin += ADD_COIN;
    }
    public bool CheckCoin() // Coin 갱신되었을때이다.. 즉 bestscore가 바뀌어 Rank가 바뀌게 되는 순간이다 ,, dim처리를 위해 bool선언
    {
        if (!isLogin) return false;
        if (player.PlayerCoin < CONSUM_COIN) return false;
        return true;
    }
    public List<PlayerInfo> GetPlayers()
    {
        List<PlayerInfo> list = players.ToList();
        return list;
    }
    public PlayerInfo GetPlayer()
    {
        PlayerInfo newplayer = new PlayerInfo();
        newplayer = player.DeepCopy();
        return newplayer;
    }
    void GetPlayerIndex()
    {
        playerIdx = players.FindIndex(a => a.PlayerName == player.PlayerName);
    }    

    void SetPlayersPrefs(PlayerInfo newPlayer)
    {        
        players.Add(newPlayer);

        string str = "";
        for (int i = 0; i < players.Count - 1; i++)
            str += players[i].PlayerName == "" ? "" : players[i].PlayerName + '/';
        PlayerPrefs.SetString("Player", str);
    }
    public void SavePlayer()
    {
        player.PlayerCoin = player.PlayerCoin;

        List<int> scores = new List<int>();
        for(int i = 0; i < player.PlayerScore.Count; i++)
        {
            scores.Add(player.PlayerScore[i]);
        }
        player.PlayerScore = scores.ToList();
    }
    public void Logout()
    {
        if (!isLogin)
        {
            return;
        }
        SavePlayer();

        player = null;
        SetLoadManager();
        LoadPlayers();
        // loginCode = ELoginErrorType.logout;
    }    
    

}
