using UnityEngine;
using UnityEngine.UI;

public class LogoManager : MonoBehaviour
{
    Text title_Text, subtitle_Text,version_Text;    
    Transform _transform;
    void Start()
    {
        SetInit();
        Singleton.singleton.Fade_Out(_transform);
    }
    
    void Update()
    {
        string input = Input.inputString;
        if (!string.IsNullOrEmpty(input) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) 
        { Singleton.singleton.Fade_In(SceneNum.menu); }
    }

    void SetInit()
    {        
        title_Text = GameObject.Find("Title_Text").GetComponent<Text>();
        subtitle_Text = GameObject.Find("SubTitle_Text").GetComponent<Text>();
        version_Text = GameObject.Find("Version_Text").GetComponent<Text>();
        _transform = GameObject.Find("Main_cns").transform;
        

        title_Text.text = $"{StaticVariable.logoTitle}";
        subtitle_Text.text = $"Press On your key";
        version_Text.text = $"{StaticVariable.gameVersion}";
    }    

}
