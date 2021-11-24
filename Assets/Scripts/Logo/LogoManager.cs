using UnityEngine;
using UnityEngine.UI;

public class LogoManager : GameBaseUI
{
    Text title_Text, subtitle_Text,version_Text;
    
    public override void Awake() {
        SetLogoManager();
    }
    void Update()
    {
        string input = Input.inputString;
        if (!string.IsNullOrEmpty(input) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) 
        {
            SceneKind.sceneNum = ESceneKind.menu;
            CreateFade("in");
        }
    }

    void SetLogoManager()
    {        
        title_Text = GameObject.Find("Title_Text").GetComponent<Text>();
        subtitle_Text = GameObject.Find("SubTitle_Text").GetComponent<Text>();
        version_Text = GameObject.Find("Version_Text").GetComponent<Text>();

        canvas = GameObject.Find("Canvas").transform;

        title_Text.text = $"{StaticVariable.logoTitle}";
        subtitle_Text.text = $"Press On your key";
        version_Text.text = $"{StaticVariable.gameVersion}";
    }
}
