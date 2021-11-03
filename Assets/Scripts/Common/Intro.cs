using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    [Header("Intro Delete Time")]
    [Range(0, 300)]
    [ContextMenuItem("DefaultValue", "SetDefaultValue")]    
    public float deleteTime;
    [Header("Intro speed")]
    public float speed;
    [Header("Intro Max Scale")]
    public float maxLocalscale;
    
    void SetDefaultValue()
    {
        maxLocalscale = 5f;
        speed = 300f;
        deleteTime = 1f;
    }
    void Start()
    {        
        SetInitScale();
    }

    void Update()
    {
        StartLocalScale();
    }
    public void SetInitScale()
    {        
        //transform.localScale = new Vector3(maxLocalscale, maxLocalscale);
        Destroy(gameObject, deleteTime);
    }
    public void StartLocalScale()
    {
        //transform.localScale = new Vector3(transform.localScale.x - Time.deltaTime * speed, transform.localScale.y - Time.deltaTime * speed);
        gameObject.GetComponent<Text>().fontSize -= (int)(Time.deltaTime * speed); // font깨짐으로 인한 fontsize변경
    }
}
