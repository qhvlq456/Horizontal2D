using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBaseUI : MonoBehaviour
{
    protected Transform canvas;
    protected bool introStart;
    protected float introCount;
    [SerializeField]
    GameObject fade;
    [SerializeField]
    GameObject result;
    [SerializeField]
    GameObject intro;

    public virtual void Awake() {
        introCount = StaticVariable.introTime;
    }
    public virtual void Start() {
        CreateFade();
    }
    IEnumerator CoStartIntro()
    {
        introStart = false;
        while(introCount > 0)
        {
            GameObject _intro;
            _intro = Instantiate(intro,canvas.transform);
            _intro.GetComponent<Text>().text = introCount.ToString();
            yield return new WaitForSeconds(1f);
            introCount--;
        }
        Instantiate(intro,canvas.transform).GetComponent<Text>().text = "Go";
        introCount = StaticVariable.introTime;
    }
    public void CreateFade(string triggerName = "")
    {
        if(GameObject.Find(fade.name + "(Clone)" )!= null)
        {
            Animator animator = GameObject.Find(fade.name + "(Clone)").GetComponent<Animator>();
            animator.SetTrigger(triggerName);
        }
        else Instantiate(fade,canvas);
    }
    public virtual void StartIntro()
    {
        StartCoroutine(CoStartIntro());
    }
    public virtual void CreateResult()
    {
        Instantiate(result,canvas.transform);
    }
}
