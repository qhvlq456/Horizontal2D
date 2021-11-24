using UnityEngine;
using UnityEngine.UI;

namespace HorizontalGame
{
    public enum GameState { intro, start, end }
    public class GameBase : GameBaseUI
    {
        public int life { get; private set; }
        public int score { get; private set; }
        public float time { get; private set; }
        public Text scoreText { private get; set; }
        public Text lifeText { private get; set; }
        public Text timeText { private get; set; }
        public bool isGameOver { get; protected set; }
        public GameBase()
        {
            isGameOver = false;
            life = 1; score = 0;
            time = 0f;
        }
        public void UpdateLife()
        {
            life--;
        }
        public void UpdateScore(int _score)
        {
            score += _score;
        }
        public void UpdateTime()
        {
            time += Time.deltaTime;
        }
        public void CurrentText()
        {
            scoreText.text = $"Score : {score}";
            lifeText.text = $"Life : {life}";
            timeText.text = $"Time : {time:N2}"; // 0.00 N2자리 사용가능
        }
        public void IsGameOver()
        {
            if (life <= 0) isGameOver = true;
        }
    }
}
