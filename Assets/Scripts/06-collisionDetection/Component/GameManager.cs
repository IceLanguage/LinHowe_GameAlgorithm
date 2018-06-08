using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CollisionDetection;
namespace LinHoweCollisionDetection
{
    public class GameManager : UnityComponentSingleton<GameManager>
    {
        /// <summary>
        /// 发射点
        /// </summary>
        public Transform LaunchPoint;
        public GameObject BallPrefab;
        public List<AABB> aabbs = new List<AABB>();
        public List<SphereComponent> spheres = new List<SphereComponent>();
        private void Update()
        {
            //发射小球
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                Vector3 d = Vector3.Normalize(ray.direction);
                d.y = 0;
                GameObject ball = Instantiate(BallPrefab);
                ball.transform.position = LaunchPoint.transform.position;

                ball.GetComponent<Rigidbody>().AddForce(d * 300);

            }

            //碰撞检测
            for(int i = 0;i< spheres.Count;++i)
            {
                for (int j = 0; j < aabbs.Count; ++j)
                {
                    if (spheres[i].HasCheckedAABB.Contains(aabbs[j])) continue;
                    Vector3 closestPt;
                    if(IntersectionTest.Check_Sphere_AABB(spheres[i].sphere,aabbs[j],out closestPt))
                    {
                        CollisionDetection.Plane p = aabbs[j].GetClosestPlane(spheres[i].sphere.center);
                        Vector3 noraml = p.normal;
                        Debug.Log(noraml);
                        //Vector3 v = aabbs[j].center - closestPt;
                        Vector3 v = spheres[i].Rigidbody.velocity;
                        v = Vector3.Reflect(v, noraml);
                        spheres[i].Rigidbody.velocity = v;
                        spheres[i].HasCheckedAABB.Add(aabbs[j]);
                    }
                }
                for (int k = 0; k < spheres.Count; ++k)
                {
                    if (k == i) continue;
                    if (spheres[i].HasCheckedSphere.Contains(spheres[k])) continue;
                    if (IntersectionTest.Check_Sphere_Sphere(spheres[i].sphere, spheres[k].sphere))
                    {
                        spheres[i].Rigidbody.velocity = -spheres[i].Rigidbody.velocity * 0.9f;
                        spheres[i].HasCheckedSphere.Add(spheres[k]);
                    }
                }
            }
        }
    }
}

