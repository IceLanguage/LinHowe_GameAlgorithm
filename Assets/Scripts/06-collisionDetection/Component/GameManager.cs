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
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                Vector3 d = Vector3.Normalize(ray.direction);
                d.y = 0;
                GameObject ball = Instantiate(BallPrefab);
                ball.transform.position = LaunchPoint.transform.position;

                ball.GetComponent<Rigidbody>().AddForce(d * 300);

            }


            for(int i = 0;i< spheres.Count;++i)
            {
                for (int j = 0; j < aabbs.Count; ++j)
                {
                    Vector3 closestPt;
                    if(IntersectionTest.Check_Sphere_AABB(spheres[i].sphere,aabbs[j],out closestPt))
                    {
                        Debug.Log(closestPt);
                    }
                }
                for (int k = 0; k < spheres.Count; ++k)
                {
                    if (k == i) continue;
                    if (IntersectionTest.Check_Sphere_Sphere(spheres[i].sphere, spheres[k].sphere))
                    {
                        Debug.Log("p");
                    }
                }
            }
        }
    }
}

