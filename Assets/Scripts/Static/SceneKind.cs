using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum ESceneKind 
{ 
    logo,menu,flappy , angry, airplan, omok, othello, chess 
}
public class SceneKind : MonoBehaviour
{
    public static ESceneKind sceneNum;
    public static int GetGameScene()
    {
        if((int)sceneNum < 2) return (int)sceneNum;
        else
            return (int)sceneNum - (int)ESceneKind.flappy;
    }
    public static int gameKindNum = Enum.GetValues(typeof(ESceneKind)).Length - 2;
}
