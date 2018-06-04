using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LinHoweFSM
{
    public class PlayerIdleState : State
    {
        private Animator animator;
        public override void Initialize()
        {
            AddTransition<PlayerSayHelloState>();
            AddTransition<PlayerDanceState>();
            AddTransition<PlayerFallState>();
        }

        public override void Enter()
        {
            Debug.Log("PlayerIdleState Enter");
            animator = Machine.GetComponent<Animator>();

            animator.Play("WAIT00");

        }

        public override void Execute()
        {
            
            if(checkKick())
            {
                Machine.ChangeState<PlayerFallState>();
            }
            else if (Input.GetMouseButton(1))
            {
                Machine.ChangeState<PlayerDanceState>();
            }
        }

        public override void Exit()
        {
            Debug.Log("PlayerIdleState Exit");
        }

        bool checkKick()
        {
            RaycastHit hit;//射线投射碰撞信息  
                           // 从鼠标所在的位置发射  
            Vector2 screenPosition = Input.mousePosition;//当前鼠标的位置  
            var ray = Camera.main.ScreenPointToRay(screenPosition);  //从当前屏幕鼠标位置发出一条射线  
            if (Physics.Raycast(ray, out hit) && hit.transform == Machine.transform)//判断是否点击到实例物体上  
                return true;
            return false;
        }
    }
}
