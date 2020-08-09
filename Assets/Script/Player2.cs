using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    public Animator player2Animator;
    AnimatorStateInfo animatorInfo;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animatorInfo = player2Animator.GetCurrentAnimatorStateInfo(0);
        
        if(Input.GetKeyDown(KeyCode.J))
        {
            /*
             * 1.站立未攻击
             * 2.攻击中...
             * 3.攻击结束...
             */
            print("J...攻击...");
            if (!player2Animator.GetBool("isPunch") && animatorInfo.IsName("Idle"))
            {
                print("Idle --> Attack...");
                player2Animator.SetBool("isPunch", true);
                return;
            }
        }
        
        //normalizedTime的值为0~1，0为开始，1为结束。
        /*if (animatorInfo.normalizedTime >= 1.0f && animatorInfo.IsName("Punch"))
        {
            Debug.Log("攻击结束");
            player2Animator.SetBool("isPunch", false);
        }*/
    }

    // 动画事件
    void AnimStop_Punch()
    {
        Debug.Log("攻击结束");
        player2Animator.SetBool("isPunch", false);
        /*
         * 日期：2020年8月10日
         * 问题：攻击动画过渡到Idle时，位置不同步..
         * 解决：ps调整图片解决.
         * 原则：美术可以解决的，尽量不用程序.
       */
    }
}
