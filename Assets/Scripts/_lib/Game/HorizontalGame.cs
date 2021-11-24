using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace HorizontalGame
{
    public class HorizontalGame : GameBase
    {
        public GameState gameState { get; protected set; }
        public string gameName;
        int maxStateIdx;
        protected int currentStateIdx { get; private set; }

        public HorizontalGame()
        {
            maxStateIdx = Enum.GetValues(typeof(GameState)).Length;
            gameState = 0; // intro state
            currentStateIdx = 0;
            introStart = true;
        }
        public virtual void LifeCycle() // intro빼고 updateScore 넣어야 됨 결국은 bundle로 묶어야 되나
        {
            if (isGameOver) return;

            if (gameState == GameState.intro)
            {
                if(!introStart) return;
                IntroCycle();
                Invoke("NextGameState",introCount);
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
        public virtual void NextGameState()
        {
            currentStateIdx = (int)gameState;
            currentStateIdx = (currentStateIdx + 1) % maxStateIdx;
            gameState = (GameState)currentStateIdx;
        }
        public virtual void IntroCycle() 
        {
            StartIntro();
        }
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

