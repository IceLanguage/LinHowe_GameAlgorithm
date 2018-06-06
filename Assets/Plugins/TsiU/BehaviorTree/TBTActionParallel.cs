using System;
using System.Collections.Generic;

namespace TsiU
{
    public class TBTActionParallel : TBTAction
    {
        public enum ECHILDREN_RELATIONSHIP
        {
            AND, OR
        }
        //-------------------------------------------------------
        protected class TBTActionParallelContext : TBTActionContext
        {
            internal List<bool> evaluationStatus;
            internal List<int> runningStatus;

            public TBTActionParallelContext()
            {
                evaluationStatus = new List<bool>();
                runningStatus = new List<int>();
            }
        }
        //-------------------------------------------------------
        private ECHILDREN_RELATIONSHIP _evaluationRelationship;
        private ECHILDREN_RELATIONSHIP _runningStatusRelationship;
        //-------------------------------------------------------
        public TBTActionParallel()
            : base(-1)
        {
            _evaluationRelationship = ECHILDREN_RELATIONSHIP.AND;
            _runningStatusRelationship = ECHILDREN_RELATIONSHIP.OR;
        }
        public TBTActionParallel SetEvaluationRelationship(ECHILDREN_RELATIONSHIP v)
        {
            _evaluationRelationship = v;
            return this;
        }
        public TBTActionParallel SetRunningStatusRelationship(ECHILDREN_RELATIONSHIP v)
        {
            _runningStatusRelationship = v;
            return this;
        }
        //------------------------------------------------------
        protected override bool onEvaluate(/*in*/TBTWorkingData wData)
        {
            TBTActionParallelContext thisContext = getContext<TBTActionParallelContext>(wData);
            initListTo<bool>(thisContext.evaluationStatus, false);
            bool finalResult = false;
            for (int i = 0; i < GetChildCount(); ++i) {
                TBTAction node = GetChild<TBTAction>(i);
                bool ret = node.Evaluate(wData);
                //early break
                if (_evaluationRelationship == ECHILDREN_RELATIONSHIP.AND && ret == false) {
                    finalResult = false;
                    break;
                }
                if (ret == true){
                    finalResult = true;
                }
                thisContext.evaluationStatus[i] = ret;
            }
            return finalResult;
        }
        protected override int onUpdate(TBTWorkingData wData)
        {
            TBTActionParallelContext thisContext = getContext<TBTActionParallelContext>(wData);
            //first time initialization
            if (thisContext.runningStatus.Count != GetChildCount()) {
                initListTo<int>(thisContext.runningStatus, TBTRunningStatus.EXECUTING);
            }
            bool hasFinished  = false;
            bool hasExecuting = false;
            for (int i = 0; i < GetChildCount(); ++i) {
                if (thisContext.evaluationStatus[i] == false) {
                    continue;
                }
                if (TBTRunningStatus.IsFinished(thisContext.runningStatus[i])) {
                    hasFinished = true;
                    continue;
                }
                TBTAction node = GetChild<TBTAction>(i);
                int runningStatus = node.Update(wData);
                if (TBTRunningStatus.IsFinished(runningStatus)) {
                    hasFinished  = true;
                } else {
                    hasExecuting = true;
                }
                thisContext.runningStatus[i] = runningStatus;
            }
            if (_runningStatusRelationship == ECHILDREN_RELATIONSHIP.OR && hasFinished || _runningStatusRelationship == ECHILDREN_RELATIONSHIP.AND && hasExecuting == false) {
                initListTo<int>(thisContext.runningStatus, TBTRunningStatus.EXECUTING);
                return TBTRunningStatus.FINISHED;
            }
            return TBTRunningStatus.EXECUTING;
        }
        protected override void onTransition(TBTWorkingData wData)
        {
            TBTActionParallelContext thisContext = getContext<TBTActionParallelContext>(wData);
            for (int i = 0; i < GetChildCount(); ++i) {
                TBTAction node = GetChild<TBTAction>(i);
                node.Transition(wData);
            }
            //clear running status
            initListTo<int>(thisContext.runningStatus, TBTRunningStatus.EXECUTING);
        }
        private void initListTo<T>(List<T> list, T value)
        {
            int childCount = GetChildCount();
            if (list.Count != childCount) {
                list.Clear();
                for (int i = 0; i < childCount; ++i) {
                    list.Add(value);
                }
            } else {
                for (int i = 0; i < childCount; ++i) {
                    list[i] = value;
                }
            }
        }
    }
}
