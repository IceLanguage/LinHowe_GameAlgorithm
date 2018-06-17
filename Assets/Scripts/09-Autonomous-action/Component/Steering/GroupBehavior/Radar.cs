using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LinHoweAutonomousAction
{
    /// <summary>
    /// 雷达
    /// </summary>
    public class Radar:MonoBehaviour
    {
        private float timer = 0;

        //检测的时间间隔
        public float checkInterval = 0.3f;

        //邻域半径
        public float Checkradius = 1;

        public LayerMask layerMask;
        private List<GameObject> neighbors = new List<GameObject>();
        private Collider[] colliders;

        public List<GameObject> Neighbors
        {
            get
            {
                return neighbors;
            }

            set
            {
                neighbors = value;
            }
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if(timer>checkInterval)
            {
                Neighbors.Clear();
                colliders = Physics.OverlapSphere(transform.position, Checkradius, layerMask);

                foreach (var e in colliders)
                    if (e.GetComponent<Vehicle>())
                        Neighbors.Add(e.gameObject);

                timer = 0;


            }
        }
    }
}
