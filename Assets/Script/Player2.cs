using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Script;
using UnityEngine;
using UnityEngine.Timeline;

public class Player2 : MonoBehaviour
{
    public Animator player2Animator;
    AnimatorStateInfo animatorInfo;
    public Rigidbody2D rb;
    public Collider2D coll;
    public LayerMask ground;
    public GameObject Attack_Punch;

    public static State Player2State = State.Normal;
    public static PlayerAttackState PlayerAttackState = PlayerAttackState.No;
    public float speed;        // 移动速度
    public float jumpforce;    // 跳跃力
    float horizonalmentmove;    // 范围： [-1， 1]    类型：float    用途：速度
    float facedirection;        // 范围： -1， 0， 1  类型：int      用途：方向
    private int Doublejump = 0;

    // Update is called once per frame
    void Update()
    {
        animatorInfo = player2Animator.GetCurrentAnimatorStateInfo(0);

        Attack();
        Movement();     // left, right
        Jump();
        
    }

    void FixedUpdate()
    {
        ChangeAnimState();
    }

    private void ChangeAnimState()
    {
        if (player2Animator.GetBool("jumping"))
        {
            // jump --> fall
            if (rb.velocity.y < 0)
            {
                player2Animator.SetBool("jumping", false);
                player2Animator.SetBool("falling", true);
            }
        }

        if (player2Animator.GetBool("falling"))
        {
            if (coll.IsTouchingLayers(ground))
            {
                print("落地地面.." + Doublejump);
                Doublejump = 0;
                if (player2Animator.GetFloat("running") > 0.1)
                {    // 移动
                    player2Animator.SetBool("falling", false);
                }
                else
                {    // 站立
                    player2Animator.SetBool("falling", false);
                    player2Animator.SetBool("idle", true);
                }
            }
        }
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            // 不可用状态
            if (player2Animator.GetBool("isPunch"))
            {
                return;
            }
            if (player2Animator.GetBool("jumping") || player2Animator.GetBool("falling"))
            {
                if (Doublejump >= 2)
                {
                    return;
                }
            }
            
            // 可用状态
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            player2Animator.SetBool("jumping", true);
            if (player2Animator.GetBool("idle"))
            {
                player2Animator.SetBool("idle", false);
            }
            Player2State = State.Jump;
            Doublejump++;
        }
    }

    private void Attack()
    {
        //normalizedTime的值为0~1，0为开始，1为结束。
        /*if (animatorInfo.normalizedTime >= 1.0f && animatorInfo.IsName("Punch"))
        {
            Debug.Log("攻击结束");
            player2Animator.SetBool("isPunch", false);
        }*/
        // 不可用状态
        if (player2Animator.GetBool("jumping") || 
            player2Animator.GetBool("falling") || 
            player2Animator.GetBool("isPunch"))
        {
            return;
        }
        
        // 可用状态
        if(Input.GetKeyDown(KeyCode.J))
        {
            /*
             * 1.站立未攻击
             * 2.攻击中...
             * 3.攻击结束...
             */
            print("J...攻击...");
            if (animatorInfo.IsName("Idle"))
            {
                print("Idle --> Attack...");
                // 1.播放动画
                player2Animator.SetBool("isPunch", true);
                player2Animator.SetBool("idle", false);
                // 2.重置碰撞器
                Attack_Punch.gameObject.transform.position = new Vector2(this.transform.position.x - 2.76f, 
                    this.transform.position.y - 1.36f);
                
            }
        }
    }

    private void Movement()
    {
        // 不可用状态
        if (player2Animator.GetBool("isPunch"))
        {
            return;
        }
        // 可用状态
        horizonalmentmove = Input.GetAxis("Horizontal");    // 范围： [-1， 1]    类型：float    用途：速度
        facedirection = Input.GetAxisRaw("Horizontal");     // 范围： -1， 0， 1  类型：int      用途：方向
        if(horizonalmentmove != 0)
        {
            // 有位移
            rb.velocity = new Vector2(horizonalmentmove * speed, rb.velocity.y);
            player2Animator.SetFloat("running", Mathf.Abs(facedirection));

        }

        if(facedirection  != 0)
        {
            transform.localScale = new Vector2(-facedirection, 1);
        }
    }
    
    // 动画事件
    void AnimState_Punch_Start()
    {
        // player2Animator.SetBool("isPunch", false);
        // player2Animator.SetBool("idle", true);
        print("State.Punch...");
        Player2State = State.Punch;
        /*
         * 日期：2020年8月10日
         * 问题：攻击动画过渡到Idle时，位置不同步..
         * 解决：ps调整图片解决.
         * 原则：美术可以解决的，尽量不用程序.
       */
    }

    void AnimState_Punch_Stop()
    {
        print("攻击结束/nState.Normal...");
        Player2State = State.Normal;
        global::Attack.PunchIsEnable = false;
    }

    void AnimEvent_Punch_Start()
    {
        print("AnimEvent_Punch_Start");
        PlayerAttackState = PlayerAttackState.Punch;
        // 如果攻击已经命中，则
        print("TriggerState.Punch:" + TriggerState.Punch);
        if (TriggerState.Punch)
        {
            if (global::Attack.PunchIsEnable) punch();
        }
    }
    
    void AnimEvent_Punch_Stop()
    {
        print("AnimEvent_Punch_Stop");
        PlayerAttackState = PlayerAttackState.No;
        global::Attack.PunchIsEnable = false;
        player2Animator.SetBool("isPunch", false);
        player2Animator.SetBool("idle", true);
    }
    
    private void punch()
    {
        print("攻击命中Punch...");
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
