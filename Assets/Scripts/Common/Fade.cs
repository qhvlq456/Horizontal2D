using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Fade : MonoBehaviour
{
    Animator animator;
    private void Awake() {
        animator = GetComponent<Animator>();
        StartCoroutine(SetFadeOut());
    }
    IEnumerator SetFadeOut()
    {        
        while(!animator.GetCurrentAnimatorStateInfo(0).IsName("Fade_in"))
            yield return null;
        Invoke("NextScene",1f);
        Destroy(gameObject,1f);
    }
    void NextScene()
    {
        SceneManager.LoadScene((int)SceneKind.sceneNum);
    }
}
