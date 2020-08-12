using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Player2 : MonoBehaviour
{
    public Animator player2Animator;
    AnimatorStateInfo animatorInfo;
    public Rigidbody2D rb;
    public Collider2D coll;
    public LayerMask ground;
    
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
        if (player2Animator.GetBool("jumping") || player2Animator.GetBool("falling")
            || player2Animator.GetBool("isPunch"))
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
                player2Animator.SetBool("isPunch", true);
                player2Animator.SetBool("idle", false);
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
    void AnimStop_Punch()
    {
        Debug.Log("攻击结束");
        player2Animator.SetBool("isPunch", false);
        player2Animator.SetBool("idle", true);
        /*
         * 日期：2020年8月10日
         * 问题：攻击动画过渡到Idle时，位置不同步..
         * 解决：ps调整图片解决.
         * 原则：美术可以解决的，尽量不用程序.
       */
    }
}
