using System;

namespace TsiU
{
    public class TBTActionLoop : TBTAction
    {
        public const int INFINITY = -1;
        //--------------------------------------------------------
        protected class TBTActionLoopContext : TBTActionContext
        {
            internal int currentCount;

            public TBTActionLoopContext()
            {
                currentCount = 0;
            }
        }
        //--------------------------------------------------------
        private int _loopCount;
        //--------------------------------------------------------
        public TBTActionLoop()
            : base(1)
        {
            _loopCount = INFINITY;
        }
        public TBTActionLoop SetLoopCount(int count)
        {
            _loopCount = count;
            return this;
        }
        //-------------------------------------------------------
        protected override bool onEvaluate(/*in*/TBTWorkingData wData)
        {
            TBTActionLoopContext thisContext = getContext<TBTActionLoopContext>(wData);
            bool checkLoopCount = (_loopCount == INFINITY || thisContext.currentCount < _loopCount);
            if (checkLoopCount == false) {
                return false;
            }
            if (IsIndexValid(0)) {
                TBTAction node = GetChild<TBTAction>(0);
                return node.Evaluate(wData);
            }
            return false;
        }
        protected override int onUpdate(TBTWorkingData wData)
        {
            TBTActionLoopContext thisContext = getContext<TBTActionLoopContext>(wData);
            int runningStatus = TBTRunningStatus.FINISHED;
            if (IsIndexValid(0)) {
                TBTAction node = GetChild<TBTAction>(0);
                runningStatus = node.Update(wData);
                if (TBTRunningStatus.IsFinished(runningStatus)) {
                    thisContext.currentCount++;
                    if (thisContext.currentCount < _loopCount || _loopCount == INFINITY) {
                        runningStatus = TBTRunningStatus.EXECUTING;
                    }
                }
            }
            return runningStatus;
        }
        protected override void onTransition(TBTWorkingData wData)
        {
            TBTActionLoopContext thisContext = getContext<TBTActionLoopContext>(wData);
            if (IsIndexValid(0)) {
                TBTAction node = GetChild<TBTAction>(0);
                node.Transition(wData);
            }
            thisContext.currentCount = 0;
        }
    }
}
