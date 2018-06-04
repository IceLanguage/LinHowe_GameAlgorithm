using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LinHoweFSM
{
    public class PlayerSayHelloState : State
    {
        private Animator animator;
        private AnimatorStateInfo animatorStateInfo;
        public override void Initialize()
        {
            AddTransition<PlayerIdleState>();
        }

        public override void Enter()
        {
            Debug.Log("PlayerSayHelloState Enter");
            animator = Machine.GetComponent<Animator>();

            animator.CrossFade("smile1@unitychan", 0);
            animator.Play("WAIT03");
        }

        public override void Execute()
        {
            if (null == animator) return;
            animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (animatorStateInfo.normalizedTime >= 1.0f &&
                animatorStateInfo.IsName("WAIT03"))//normalizedTime: 范围0 -- 1,  0是动作开始，1是动作结束  
            {
                Machine.ChangeState<PlayerIdleState>();
            }
        }

        public override void Exit()
        {
            Debug.Log("PlayerSayHelloState Exit");
        }
    }
}
