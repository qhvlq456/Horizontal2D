using UnityEngine;
using UnityEngine.UI;
using HorizontalGame;
using System;
using System.Collections;
public class AlertUI : MonoBehaviour
{
    [SerializeField]
    Button comfirmBtn;
    Text titleText,bodyText;
    Animator animator;
    int kindLength;
    private void Awake() {
        SetAlert();
        comfirmBtn.onClick.AddListener(OnClickCloseButton);
    }   
    private void Start() {
        InitString();
    }
    void SetAlert()
    {
        animator = GetComponent<Animator>();
        titleText = GameObject.Find("AlertTitleText").GetComponent<Text>();
        bodyText = GameObject.Find("AlertBodyText").GetComponent<Text>();
    }
    void InitString()
    {  
        titleText.text = ErrorCode.errorType.ToString();

        if(ErrorCode.errorType == ErrorType.success)
            bodyText.text = "Please Enter Game!!";
        else if(ErrorCode.errorType == ErrorType.empty)
            bodyText.text = "Please Check Input Field..";
        else if(ErrorCode.errorType == ErrorType.fail)
        {
            bodyText.text = "Please Check Input Field..";
        }
        else if (ErrorCode.errorType == ErrorType.create)
        {
            bodyText.text = "Create your ID..!!";
        }
        else if (ErrorCode.errorType == ErrorType.exist)
        {
            bodyText.text = "The ID that already exists!!";
        }
        else if (ErrorCode.errorType == ErrorType.logout)
        {
            bodyText.text = "Success Logout!!";
        }
    }
    
    public void OnClickCloseButton()
    {
        StartCoroutine(CloseAfterDelay());
    }
    IEnumerator CloseAfterDelay()
    {
        animator.SetTrigger("Close");
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
        animator.ResetTrigger("Close");
    }

}
