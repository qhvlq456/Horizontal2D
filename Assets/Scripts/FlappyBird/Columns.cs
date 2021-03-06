using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Columns : MonoBehaviour
{
    [Header("Add Score Range")]
    [Range(0,100)]
    public int score;
    BoxCollider2D _box;
    FlappyGameManager GameManager;
    void Start()
    {
        _box = GetComponent<BoxCollider2D>();
        _box.isTrigger = true;
        GameManager = GameObject.Find("GameManager").GetComponent<FlappyGameManager>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<FlappyBird>() != null) GameManager.UpdateScore(score);
    }
}
