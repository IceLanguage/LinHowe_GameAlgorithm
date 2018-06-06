using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TsiU;
using UnityEngine;

namespace LinHowBehaviorTree
{
    public class AIEntityWorkingData : TBTWorkingData
    {

        public Transform entityTF { get; set; }
        public Animator entityAnimator { get; set; }
        public Animation entityAnimation { get; set; }
        public float gameTime { get; set; }
        public float deltaTime { get; set; }
    }
}
