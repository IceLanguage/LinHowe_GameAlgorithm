using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LinHoweAutonomousAction
{
    public class BirdController: AILocomotion
    {
        protected override void Update()
        {
            base.Update();
            animator.SetBool("flying", true);

            animator.SetFloat("flyingDirectionX",
                Vector3.SignedAngle(velocity, transform.forward,Vector3.up));
            
        }
    }
}
