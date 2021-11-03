using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HorizontalGame;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class FlappyBird : MonoBehaviour
{
    public FlappyGameManager GameManager { private get; set; }
    [Header("BlinkTime")]
    [Range(0, 10)]
    public float blinkTime;
    [Header("Physics Force")]
    [Range(0,300)]
    public float _force;
    float leftTime;
    bool isBlink;

    SpriteRenderer _renderer;    
    PolygonCollider2D _collider;
    Animator _anim;
    Rigidbody2D _rigid;
    void Start()
    {
        SetGame();        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameState != GameState.start) return;

        if (Input.GetMouseButtonDown(0))
            IsButtonDown();
        BirdBlink();
    }
    void OnTriggerEnter2D(Collider2D other)
    {        
        if (isBlink) return; // 깜빡임 충돌 방지
        if (other.tag != "Column") return; // add Score
        GameManager.UpdateLife();
        isBlink = true;
        if (GameManager.life <= 0)// 목숨이 하나 남은상태[0]에서 장애물에 부딪혔으니 GameOver
        {
            EndObject(StaticVariable.destroyTime);
            GameManager.EndCycle();
        }
    }
    public void SetGame()
    {        
        leftTime = 0;
        blinkTime = StaticVariable.blinkTime;
        _force = 200f;
        isBlink = false;

        _rigid = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();        
        _collider = GetComponent<PolygonCollider2D>();
        _anim = GetComponent<Animator>();

        GameManager = GameObject.Find("GameManager").GetComponent<FlappyGameManager>();

        _rigid.isKinematic = true;
        _collider.isTrigger = true;

        Invoke("StartObject", StaticVariable.introTime);
    }        
    public void IsButtonDown()
    {        
        _rigid.velocity = Vector2.zero;
        _rigid.AddForce(Vector2.up * _force);
        _anim.SetTrigger("SetFlap");
    }
    public void StartObject()
    {        
        _rigid.isKinematic = false;        
    }
    public void EndObject(float _time)
    {
        _anim.SetTrigger("SetDie");
        _collider.isTrigger = false;
        Destroy(gameObject, _time);
    }
    public void BirdBlink()
    {
        if (!isBlink) return; // 깜빡일 때만 들어올 수 있다
        _renderer.color = Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), Mathf.PingPong(Time.time * 2, 1));
        leftTime += Time.deltaTime;
        if (leftTime / blinkTime > 1)
        {
            isBlink = false; _renderer.color = Color.white; leftTime = 0;
        }
    }
}
