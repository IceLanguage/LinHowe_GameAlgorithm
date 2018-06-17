using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LinHoweKnifeTail
{
    public class SwordController : MonoBehaviour
    {

        private float timer = 0;
        public float RotateInterval = 0.1f;

        private void FixedUpdate()
        {
            timer += Time.fixedDeltaTime;

            if (timer > RotateInterval)
            {
                int RandomIndex = Random.Range(0, 3);
                if (RandomIndex < 1)
                    transform.rotation = Quaternion.AngleAxis(Random.Range(-90, 90), transform.right) * transform.rotation;
                else if (RandomIndex < 2)
                    transform.rotation = Quaternion.AngleAxis(Random.Range(-90, 90), transform.up) * transform.rotation;
                else
                    transform.rotation = Quaternion.AngleAxis(Random.Range(-90, 90), transform.forward) * transform.rotation;
                timer = 0;
            }
        }

        
        
    }
}

