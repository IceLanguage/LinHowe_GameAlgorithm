using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LinhowePerceptualSystem
{
    public class GameManager : MonoBehaviour
    {
        private void Update()
        {
            PerceptualSystemManager.Update();
        }
    }
}
