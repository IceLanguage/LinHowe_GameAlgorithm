using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LinHoweFSM
{
    public class PlayerFallState : State
    {
        private Animator animator;
        private AnimatorStateInfo animatorStateInfo;
        public override void Initialize()
        {
            AddTransition<PlayerIdleState>();
        }

        public override void Enter()
        {
            Debug.Log("PlayerFallStatee Enter");
            animator = Machine.GetComponent<Animator>();

            animator.Play("DAMAGED01");

        }

        public override void Execute()
        {
            if (null == animator) return;
            animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (animatorStateInfo.normalizedTime >= 1.0f &&
                animatorStateInfo.IsName("DAMAGED01"))//normalizedTime: 范围0 -- 1,  0是动作开始，1是动作结束  
            {
                Machine.ChangeState<PlayerIdleState>();
            }
        }

        public override void Exit()
        {
            Debug.Log("PlayerFallState Exit");
        }
    }
}
