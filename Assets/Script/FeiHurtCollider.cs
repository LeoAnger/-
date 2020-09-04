using System.Collections;
using System.Collections.Generic;
using Assets.Script;
using UnityEngine;

public class FeiHurtCollider : MonoBehaviour
{
    public Collider2D Collider2D;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        StartCoroutine(TestWaitForSeconds());
    }

    // Update is called once per frame
    void Update()
    {
    }

    /*
     * 受伤判定
     * 使用情形：一次性攻击（弓箭，子弹...）
     */
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        /*if (gameObject.activeSelf)
        {
            print("Collider2D:" + collider2D.name);
            print("Collider2D:" + collider2D.tag);
        }*/

        // 不可用状态
        if (Player2.Player2State == State.Crouch)
        {
            return;
        }

        print("OnTriggerEnter2D()...");
        print("collider2D.name" + collider2D.name);
        // 可用状态
        switch (collider2D.name)
        {
            case "Attack_Punch":
                print("Attack_Punch:true.");
                TriggerState.Punch = true;
                break;
        }
    }

    /*
     * 受伤判定
     * 使用情形：持续性动作攻击
     */
    void OnTriggerStay2D(Collider2D collider2D)
    {
        //print("停留....");
        //攻击状态
        switch (Player2.Player2State)
        {
            /*case State.Normal:
                break;
            case State.Punch:
                print("持续性动作攻击...");
                if (Attack.PunchIsEnable) punch();
                break;*/
        }
    }

    void OnTriggerExit2D(Collider2D collider2D)
    {
        // 不可用状态
        if (Player2.Player2State == State.Crouch)
        {
            return;
        }

        // 可用状态
        switch (collider2D.name)
        {
            case "Attack_Punch":
                TriggerState.Punch = false;
                break;
        }
    }

    IEnumerator TestWaitForSeconds()
    {
        //3s后执行Debug.Log;
        yield return new WaitForSeconds(1.0f);
        Debug.Log("启动协程3s后");
        Attack.PunchIsEnable = true;
    }


    private void punch()
    {
        if (Player2.PlayerAttackState == PlayerAttackState.Punch)
        {
            print("攻击命中Punch...");
        }
        else
        {
            print("攻击未命中...");
        }
    }
}