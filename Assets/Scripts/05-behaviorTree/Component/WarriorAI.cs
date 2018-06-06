using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TsiU;
using UnityEngine;

namespace LinHowBehaviorTree
{
    public class WarriorAI: UnityComponentSingleton<WarriorAI>
    {
        private TBTAction _behaviorTree;
        private AIEntityWorkingData _behaviorWorkingData;

        private void Awake()
        {
            _behaviorWorkingData = new AIEntityWorkingData();
            _behaviorWorkingData.entityAnimation = GetComponent<Animation>();
            _behaviorWorkingData.entityTF = transform;
            
            _behaviorTree = new TBTActionPrioritizedSelector();
            _behaviorTree
                 .AddChild(new TBTActionSequence()
                    .SetPrecondition(new TBTPreconditionNOT(new HasReachEnemy()))
                    .AddChild(new LookEnemyAction())
                    .AddChild(new IdleAction()))
               .AddChild(new TBTActionParallel()
                    .AddChild(new LookEnemyAction())
                    .AddChild(new AttackAction()));


        }

        class HasReachEnemy : TBTPreconditionLeaf
        {
            public override bool IsTrue(TBTWorkingData wData)
            {
                AIEntityWorkingData thisData = wData.As<AIEntityWorkingData>();

                return Vector3.Distance(
                    thisData.entityTF.position,
                    ZombieAI.Instance.transform.position) < 2f;

            }
        }

        class LookEnemyAction: TBTActionLeaf
        {
            private int TurnToStatus = TBTRunningStatus.EXECUTING;

            protected override void onEnter(TBTWorkingData wData)
            {
                TurnToStatus = TBTRunningStatus.EXECUTING;
                AIEntityWorkingData thisData = wData.As<AIEntityWorkingData>();
                thisData.entityTF.DOLookAt(ZombieAI.Instance.transform.position, 1.5f)
                                       .OnComplete(() => UpdateStatus());
            }
            protected override bool onEvaluate(TBTWorkingData wData)
            {
                return base.onEvaluate(wData);
            }
            protected override int onExecute(TBTWorkingData wData)
            {
                return TurnToStatus;
            }
            protected override void onExit(TBTWorkingData wData, int runningStatus)
            {

                base.onExit(wData, runningStatus);
            }
            private void UpdateStatus()
            {
                TurnToStatus = TBTRunningStatus.FINISHED;
            }
        }


        class AttackAction : TBTActionLeaf
        {
            private int TurnToStatus = TBTRunningStatus.EXECUTING;

            protected override void onEnter(TBTWorkingData wData)
            {
                TurnToStatus = TBTRunningStatus.EXECUTING;
                AIEntityWorkingData thisData = wData.As<AIEntityWorkingData>();
                thisData.entityAnimation.Play("Attack");
            }
            protected override bool onEvaluate(TBTWorkingData wData)
            {
                return base.onEvaluate(wData);
            }
            protected override int onExecute(TBTWorkingData wData)
            {
                return TurnToStatus;
            }
            protected override void onExit(TBTWorkingData wData, int runningStatus)
            {

                base.onExit(wData, runningStatus);
            }
            private void UpdateStatus()
            {
                TurnToStatus = TBTRunningStatus.FINISHED;
            }
        }

        class IdleAction : TBTActionLeaf
        {

            protected override void onEnter(TBTWorkingData wData)
            {

                AIEntityWorkingData thisData = wData.As<AIEntityWorkingData>();
                thisData.entityAnimation.Play("idle");
            }
            protected override bool onEvaluate(TBTWorkingData wData)
            {
                return base.onEvaluate(wData);
            }
            protected override int onExecute(TBTWorkingData wData)
            {
                return TBTRunningStatus.FINISHED;
            }
            protected override void onExit(TBTWorkingData wData, int runningStatus)
            {

                base.onExit(wData, runningStatus);
            }

        }
        private void Update()
        {
            if (_behaviorTree.Evaluate(_behaviorWorkingData))
            {
                _behaviorTree.Update(_behaviorWorkingData);
            }
            else
            {
                _behaviorTree.Transition(_behaviorWorkingData);
            }
        }
    }
}
