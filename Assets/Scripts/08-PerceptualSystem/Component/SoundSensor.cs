using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LinhowePerceptualSystem
{
    /// <summary>
    /// 听觉感知器
    /// </summary>
    public class SoundSensor:BaseSensor
    {
        private int updateAction;
        protected override void Awake()
        {
            PerceptualTyep = PerceptualEnum.Sound;
            animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody>();
            base.Awake();
        }

        public override void Notify(BaseTraigger traigger)
        {
            updateAction = 0;
            print(name + "hear sound at" + traigger.transform.position);
            Vector3 direction = traigger.transform.position - transform.position;
            Vector3.Normalize(direction);
            if (Vector3.Distance(traigger.transform.position, transform.position) > 1.5f)
            {
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("walk"))
                    animator.Play("walk");
                _rigidbody.velocity = direction / 100;



            }
            else
            {

                if (Vector3.Angle(direction, transform.forward) >= 10)
                    transform.LookAt(traigger.transform.position);
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
                    animator.Play("attack");
            }
        }

        private void Update()
        {
            updateAction++;
            if (updateAction > 40)
            {
                animator.Play("idle");
                float randomY = UnityEngine.Random.Range(-90, 90);
                _rigidbody.velocity = Vector3.zero;
                transform.DORotate(new Vector3(0, randomY, 0), 0.3f);

            }
        }

        
    }
}
