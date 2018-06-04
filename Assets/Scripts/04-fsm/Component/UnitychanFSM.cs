using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinHoweFSM
{
    public class UnitychanFSM : FSM
    {
        
        public override void AddStates()
        {
            AddState<PlayerIdleState>();
            AddState<PlayerSayHelloState>();
            AddState<PlayerDanceState>();
            AddState<PlayerFallState>();

            SetCurrentState<PlayerSayHelloState>();
        }

        
    }
}
