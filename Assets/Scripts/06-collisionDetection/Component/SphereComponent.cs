using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CollisionDetection;
namespace LinHoweCollisionDetection
{
    public class SphereComponent : MonoBehaviour
    {
        public Sphere sphere;
        public float r;
        private new Rigidbody rigidbody;
        /// <summary>
        /// 已经检测过的AABB
        /// </summary>
        public AABB HasCheckedAABB ;
        public SphereComponent HasCheckedSphere ;
        public Rigidbody Rigidbody
        {
            get
            {
                if(null == rigidbody)
                    rigidbody = GetComponent<Rigidbody>();
                return rigidbody;
            }
        }
        private void Awake()
        {
            
            sphere = new Sphere(transform.position, r);
            GameManager.Instance.spheres.Add(this);
        }
        private void Update()
        {
            sphere = new Sphere(transform.position, r);
        }
        public void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(0.4f, 0.9f, 0.4f);
            Gizmos.DrawWireSphere(transform.position, r);

        }
        private void OnDestroy()
        {
            GameManager.Instance.spheres.Remove(this);
        }
        
      
    }
}

