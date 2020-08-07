using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAni : MonoBehaviour
{
    public float speed;
    private bool isflying = false;
    private Transform this_Transform;    // 动态值    失败：实时的指针索引
    private Vector2 this_Vector2;        // 静态值    成功
    
    public Transform movePos;


    void Awake()
    {
        this_Transform = transform;
        this_Vector2 = transform.position;
        // print("v2:" + this_Vector2);
    }

    void Start()
    {
        StartCoroutine(Do()); // 开启协程
    }

    // Update is called once per frame
    void Update()
    {
        if (isflying)
        {
            // transform.position = this_Transform.position;
            transform.position = this_Vector2;
            // print("...v2:" + this_Vector2);
            return;
        }
        else
        {
            fly();
        }
    }

    // fly()：可以在Update（）生效，在协程中Do()无效.    -- 2020年8月7日16点59分
    void fly()
    {
        // 敌人移动
        transform.position = Vector2.MoveTowards(transform.position, movePos.position, speed * Time.deltaTime);
        // 移动判断
        if (Vector2.Distance(transform.position, movePos.position) < 0.1f)
        {    // 到达位置
            isflying = true;
        }
    }
    
    IEnumerator Do()
    {
        while (true) // 还需另外设置跳出循环的条件
        {
            yield return new WaitForSeconds(10.0f);
            isflying = false;
            
            Debug.Log("work");
        }
    }
}





















