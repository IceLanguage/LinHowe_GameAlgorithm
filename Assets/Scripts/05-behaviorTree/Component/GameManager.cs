using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TsiU;
using UnityEngine;

namespace LinHowBehaviorTree
{

    public class GameManager : MonoBehaviour
    {
        
        private void Awake()
        {
            TAIToolkit.Init();
            GameTimer.instance.Init();
        }

        private void Update()
        {
            GameTimer.instance.UpdateTime();
        }

        private void OnDestroy()
        {
            TAIToolkit.Uninit();
        }
    }
}
