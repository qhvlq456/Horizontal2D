using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ErrorType 
{ 
    none, success, empty, fail, create, exist, logout 
}
public class ErrorCode : MonoBehaviour
{
    public static ErrorType errorType;
    public static void SetErrorType(ErrorType type)
    {
        switch(type)
        {
            case ErrorType.none : errorType = ErrorType.none;
            break;
            case ErrorType.success : errorType = ErrorType.success;
            break;
            case ErrorType.empty : errorType = ErrorType.empty;
            break;
            case ErrorType.fail : errorType = ErrorType.fail;
            break;
            case ErrorType.create : errorType = ErrorType.create;
            break;
            case ErrorType.exist : errorType = ErrorType.exist;
            break;
            case ErrorType.logout : errorType = ErrorType.logout;
            break;
        }        
    }
}
