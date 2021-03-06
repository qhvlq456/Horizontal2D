using UnityEngine;
using UnityEngine.UI;

namespace HorizontalGame
{  
    public abstract class Menu : GameBaseUI
    {
        protected PlayerInfo player;
        public Text titleText { protected get; set; }
        public Text bodyText { protected get; set; }
        public bool isLogin { get; private set; }

        public Menu()
        {
            player = null;
            isLogin = false;
        }
        public void IsLogin(PlayerInfo _player)
        {
            if (_player.playerName == null || _player.playerName == "") return;
            player = _player;
            isLogin = true;
        }
        public void IsLogOut()
        {
            player = null;
            isLogin = false;
        }
        public abstract void UpdateMenu();
    }
}
