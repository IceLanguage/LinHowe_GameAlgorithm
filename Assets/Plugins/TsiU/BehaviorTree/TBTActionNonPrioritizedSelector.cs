using System;
using System.Collections.Generic;

namespace TsiU
{
    public class TBTActionNonPrioritizedSelector : TBTActionPrioritizedSelector
    {
        public TBTActionNonPrioritizedSelector()
            : base()
        {
        }
        protected override bool onEvaluate(/*in*/TBTWorkingData wData)
        {
            TBTActionPrioritizedSelector.TBTActionPrioritizedSelectorContext thisContext = 
                getContext<TBTActionPrioritizedSelector.TBTActionPrioritizedSelectorContext>(wData);
            //check last node first
            if (IsIndexValid(thisContext.currentSelectedIndex)) {
                TBTAction node = GetChild<TBTAction>(thisContext.currentSelectedIndex);
                if (node.Evaluate(wData)) {
                    return true;
                }
            }
            return base.onEvaluate(wData);
        }
    }
}
