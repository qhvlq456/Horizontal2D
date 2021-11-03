using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticVariable : MonoBehaviour
{    
    // 변수들만 사용하는 것이 좋다
    public static string gameVersion = "1.0.0";
    public static string logoTitle = "Lee yong hee JJang";    
    
    // Flappy Bird
    public static float blinkTime = 3f;
    public static float destroyTime = 2.5f;
    // Angry Bird
    public static float startTime = 10f;
    public static float endTime = 3.5f;
    public static int birdCount = 3;
    // common
    public static float introTime = 3f;
    public static float speed = 13.6f;
    public static float fadeTime = 2.0f;
    
}
