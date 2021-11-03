using UnityEngine;

namespace HorizontalGame
{
    public class PlayerInfo
    {
        public string nickName { get; set; }
        public int FlappybestScore { get; set; }
        public int AngrybestScore { get; set; }
        public int coin { get; set; }

        public PlayerInfo()
        {
        }
        public PlayerInfo(string nick, int score, int score2, string cir)
        {
            nickName = nick; FlappybestScore = score; AngrybestScore = score2; coin = int.Parse(cir);
        }

        public PlayerInfo DeepCopy()
        {
            PlayerInfo player = new PlayerInfo();
            player.nickName = nickName;
            player.FlappybestScore = FlappybestScore;
            player.AngrybestScore = AngrybestScore;
            player.coin = coin;

            return player;
        }
    }
}
