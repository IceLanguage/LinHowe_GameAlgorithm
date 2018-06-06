using System;
using System.Collections.Generic;

namespace TsiU
{
    public class TBTActionPrioritizedSelector : TBTAction
    {
        protected class TBTActionPrioritizedSelectorContext : TBTActionContext
        {
            internal int currentSelectedIndex;
            internal int lastSelectedIndex;

            public TBTActionPrioritizedSelectorContext()
            {
                currentSelectedIndex = -1;
                lastSelectedIndex = -1;
            }
        }
        public TBTActionPrioritizedSelector()
            : base(-1)
        {
        }
        protected override bool onEvaluate(/*in*/TBTWorkingData wData)
        {
            TBTActionPrioritizedSelectorContext thisContext = getContext<TBTActionPrioritizedSelectorContext>(wData);
            thisContext.currentSelectedIndex = -1;
            int childCount = GetChildCount();
            for(int i = 0; i < childCount; ++i) {
                TBTAction node = GetChild<TBTAction>(i);
                if (node.Evaluate(wData)) {
                    thisContext.currentSelectedIndex = i;
                    return true;
                }
            }
            return false;
        }
        protected override int onUpdate(TBTWorkingData wData)
        {
            TBTActionPrioritizedSelectorContext thisContext = getContext<TBTActionPrioritizedSelectorContext>(wData);
            int runningState = TBTRunningStatus.FINISHED;
            if (thisContext.currentSelectedIndex != thisContext.lastSelectedIndex) {
                if (IsIndexValid(thisContext.lastSelectedIndex)) {
                    TBTAction node = GetChild<TBTAction>(thisContext.lastSelectedIndex);
                    node.Transition(wData);
                }
                thisContext.lastSelectedIndex = thisContext.currentSelectedIndex;
            }
            if (IsIndexValid(thisContext.lastSelectedIndex)) {
                TBTAction node = GetChild<TBTAction>(thisContext.lastSelectedIndex);
                runningState = node.Update(wData);
                if (TBTRunningStatus.IsFinished(runningState)) {
                    thisContext.lastSelectedIndex = -1;
                }
            }
            return runningState;
        }
        protected override void onTransition(TBTWorkingData wData)
        {
            TBTActionPrioritizedSelectorContext thisContext = getContext<TBTActionPrioritizedSelectorContext>(wData);
            TBTAction node = GetChild<TBTAction>(thisContext.lastSelectedIndex);
            if (node != null) {
                node.Transition(wData);
            }
            thisContext.lastSelectedIndex = -1;
        }
    }
}
