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
        public List<AABB> HasCheckedAABB = new List<AABB>();
        public List<SphereComponent> HasCheckedSphere = new List<SphereComponent>();
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
            Gizmos.DrawSphere(transform.position, r);

        }
        private void OnDestroy()
        {
            GameManager.Instance.spheres.Remove(this);
        }
        
        IEnumerator removeChecked()
        {
            yield return new WaitForSeconds(0.5f);
            HasCheckedAABB.Clear();
            HasCheckedSphere.Clear();
        }
    }
}

