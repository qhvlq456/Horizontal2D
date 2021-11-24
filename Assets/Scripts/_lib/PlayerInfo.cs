using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HorizontalGame
{
    [System.Serializable]
    public class PlayerInfo
    {
        public string playerName;
        public string PlayerName 
        { 
            get
            {
                if(playerName != null)
                    return playerName;
                else
                    return null;
            }
            set
            {
                if(!string.IsNullOrEmpty(value)) 
                {
                    playerName = value;
                }
            }
        }
        public List<int> playerScore = new List<int>();
        public List<int> PlayerScore
        {
            get
            {
                List<int> deepCopy = playerScore.ToList();
                return deepCopy;
            }
            set 
            {
                for(int i = 0; i < value.Count; i++)
                {                    
                    playerScore.Add(value[i]);
                    PlayerPrefs.SetInt(playerName + i,value[i]);
                }
            }
        }
        public int GetSelectPlayerScore(int index)
        {
            return playerScore[index];
        }
        public void SetSelectPlayerScore(int score)
        {
            if(PlayerPrefs.GetInt(playerName + SceneKind.GetGameScene().ToString(),0) < score)
            {
                playerScore[SceneKind.GetGameScene()] = score;
                PlayerPrefs.SetInt(playerName + SceneKind.GetGameScene(), score);
            }
        }
        public int playerCoin;
        public int PlayerCoin
        { 
            get
            {
                return PlayerPrefs.GetInt(playerName + "coin",0);
            }
            set 
            {
                playerCoin = value;
                PlayerPrefs.SetInt(playerName + "coin",value);
            }
        }

        public PlayerInfo()
        {
            playerScore = new List<int>();
        }
        public PlayerInfo(string playerName, int coin, params int[] scores)
        {            
            this.playerName = playerName; // 저장할 필요가 없습

            this.playerCoin = coin;
            PlayerPrefs.SetInt(playerName + "coin",coin);

            for(int i = 0; i < scores.Length; i++)
            {
                playerScore.Add(scores[i]);
                PlayerPrefs.SetInt(playerName + i,scores[i]);
            }
        }

        public PlayerInfo DeepCopy()
        {
            PlayerInfo player = new PlayerInfo();
            player.playerName = playerName;
            player.playerCoin = playerCoin;
            player.playerScore = playerScore;

            return player;
        }
    }
}
