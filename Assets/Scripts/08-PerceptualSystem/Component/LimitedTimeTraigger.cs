using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinhowePerceptualSystem
{
    /// <summary>
    /// 存在持续时间的触发器
    /// </summary>
    public class LimitedTimeTraigger:BaseTraigger
    {
        /// <summary>
        /// 持续时间
        /// </summary>
        public int duration;
        public override void UpdateInfo()
        {
            if (--duration <= 0)
                ToBeRemoved = true;
        }
    }
}
