using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace HorizontalGame
{
    public class HorizontalGame : GameBase
    {
        public string gameName;
        public GameState gameState { get; protected set; }
        protected bool isIntro { private get; set; }
        int maxStateIdx;
        protected int currentStateIdx { get; private set; }
        public int introCount { private get; set; }
        public Transform _transform { protected get; set; }
        public GameObject _intro { private get; set; }
        public GameObject _result { private get; set; }
        public HorizontalGame()
        {
            maxStateIdx = Enum.GetValues(typeof(GameState)).Length;
            gameState = 0; // intro state
            currentStateIdx = 0;
            isIntro = true;
        }
        public virtual void LifeCycle() // intro빼고 updateScore 넣어야 됨 결국은 bundle로 묶어야 되나
        {
            if (isGameOver) return;

            if (gameState == GameState.intro)
            {
                if (isIntro)
                {
                    StartCoroutine(StartIntro());
                    isIntro = false;
                }
            }
            else if (gameState == GameState.start)
            {
                StartCycle();
            }
            else // end state
            {
                EndCycle();
            }
        }
        public void NextState()
        {
            currentStateIdx = (int)gameState;
            currentStateIdx = (currentStateIdx + 1) % maxStateIdx;
            gameState = (GameState)currentStateIdx;
        }
        IEnumerator StartIntro()
        {
            IntroCycle();
            for (int i = introCount; i > 0; i--)
            {
                CreateIntroText(i.ToString());
                yield return new WaitForSeconds(1f);
            }
            CreateIntroText("Go!!");
        }
        void CreateIntroText(string _text)
        {
            _intro.GetComponent<Text>().text = _text;
            Instantiate(_intro, _transform);
        }
        public void CreateResult()
        {
            Instantiate(_result, _transform);
        }
        public virtual void IntroCycle() { }
        public virtual void StartCycle()
        {
            UpdateTime();
        }
        public virtual void EndCycle() // flappy는 아에 끝나는거고 angry는 다시 첨으로 가는 것;;
        {
            IsGameOver();
        }
    }
}

