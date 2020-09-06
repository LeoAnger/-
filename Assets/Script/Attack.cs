using System.Collections;
using System.Collections.Generic;
using Assets.Script;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Collider2D punchColl;
    public static bool PunchIsEnable = true;
    public GameObject Player2;

    void Update()
    {
        // 追随Player2
        // 问题：Collider触发器对象时Player2，不是Attack的gameobject 2020年8月20日17点27分
        //this.gameObject.transform.position = new Vector2(Player2.transform.position.x - 2.76f, Player2.transform.position.y - 1.36f);
    }

    void punch()
    {
        // 1.启用攻击检测
        if (enabled)
        {
            
        }
    }

    public void attack()
    {
        this.gameObject.transform.position = new Vector2(Player2.transform.position.x - 2.76f, Player2.transform.position.y - 1.36f);
    }
}
