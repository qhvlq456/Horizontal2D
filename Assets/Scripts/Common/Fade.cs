using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Fade : MonoBehaviour
{
    delegate void FadeType();
    FadeType fade;
    public float coolTime { protected get; set; }
    protected float leftTime;
    public Image img { protected get; set; }

    void Start()
    {        
        coolTime = StaticVariable.fadeTime;
    }
    void Update()
    {
        fade();
    }
    void SetInit()
    {
        leftTime = 0f;
        img = GetComponent<Image>();
    }
    public void SetFadeType(int type)
    {
        SetInit();
        if (type == 0)
        {
            fade = FadeIn;
            img.color = new Color(0, 0, 0, 0);
        }
        else
        {
            fade = FadeOut;
            img.color = new Color(0, 0, 0, 1);
        }
    }

    void FadeIn()
    {
        leftTime += Time.deltaTime;
        if (leftTime / coolTime < 1)
        {
            img.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, 1), leftTime / coolTime);
        }
        else
        {
            NextScene();
        }


    }

    void FadeOut()
    {
        leftTime += Time.deltaTime;
        if (leftTime / coolTime < 1)
        {
            img.color = Color.Lerp(new Color(0, 0, 0, 1), new Color(0, 0, 0, 0), leftTime / coolTime);
        }        
    }
    void NextScene()
    {
        SceneManager.LoadScene((int)Singleton.singleton.sceneNum);
    }
}
