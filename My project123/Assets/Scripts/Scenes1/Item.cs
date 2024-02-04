using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string type;
    Rigidbody2D rigid;


    // Start is called before the first frame update
    void Awake()
    {
        rigid =GetComponent<Rigidbody2D>();
    }
    void OnEnable()
    {
        rigid.velocity = Vector2.down*1.5f;
    }

}
