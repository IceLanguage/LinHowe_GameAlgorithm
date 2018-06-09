using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CollisionDetection;
namespace LinHoweCollisionDetection
{
    public class GameManager : UnityComponentSingleton<GameManager>
    {
        enum CollisionDetectionType
        {
            Simple,
            OctTree,
        }
        private CollisionDetectionType cdtype = CollisionDetectionType.Simple;

        /// <summary>
        /// 发射点
        /// </summary>
        public Transform LaunchPoint;
        public GameObject BallPrefab;
        public List<AABB> aabbs = new List<AABB>();
        public List<SphereComponent> spheres = new List<SphereComponent>();
        private void Start()
        {
            //更新AABB八叉树
            for (int i = 0; i < aabbs.Count; ++i)
                OctTreeComponent.Instance.tree.Insert(aabbs[i]);
        }
        private void Update()
        {
            ////更新八叉树
            //OctTreeComponent.Instance.tree.SphereOctTree.Clear();
            //for (int i = 0; i < spheres.Count; ++i)
            //    OctTreeComponent.Instance.tree.Insert(spheres[i].sphere);

            //发射小球
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                Vector3 d = Vector3.Normalize(ray.direction);
                d.y = 0;
                GameObject ball = Instantiate(BallPrefab);
                ball.transform.position = LaunchPoint.transform.position;

                ball.GetComponent<Rigidbody>().AddForce(d * 300);

            }

            //碰撞检测
            CheckCollisionDetection();



        }

        private void CheckCollisionDetection()
        {
            switch(cdtype)
            {
                case CollisionDetectionType.Simple:
                    SimpleCollisionDetection();
                    break;
                case CollisionDetectionType.OctTree:
                    OctTreeCollisionDetection();
                    break;
                default:
                    break;
            }
            
        }
        private void SimpleCollisionDetection()
        {
            for (int i = 0; i < spheres.Count; ++i)
            {
                for (int j = 0; j < aabbs.Count; ++j)
                {
                    if (spheres[i].HasCheckedAABB == aabbs[j]) continue;
                    Vector3 closestPt;
                    if (IntersectionTest.Check_Sphere_AABB(spheres[i].sphere, aabbs[j], out closestPt))
                    {
                        CollisionDetection.Plane p = aabbs[j].GetClosestPlane(spheres[i].sphere.center);
                        Vector3 noraml = p.normal;
                        Vector3 v = spheres[i].Rigidbody.velocity;
                        v = Vector3.Reflect(v, noraml);
                        spheres[i].Rigidbody.velocity = v * 0.9f;
                        spheres[i].HasCheckedAABB = aabbs[j];
                    }
                }
                for (int k = 0; k < spheres.Count; ++k)
                {
                    if (k == i) continue;
                    if (spheres[i].HasCheckedSphere == spheres[k]) continue;
                    if (IntersectionTest.Check_Sphere_Sphere(spheres[i].sphere, spheres[k].sphere))
                    {
                        spheres[i].Rigidbody.velocity = -spheres[i].Rigidbody.velocity * 0.9f;
                        spheres[i].HasCheckedSphere = spheres[k];
                    }
                }
            }
        }
        private void OctTreeCollisionDetection()
        {

        }
        //#region 编辑器扩展
        //[ContextMenu("遍历所有包围体，两辆检测碰撞")]
        //public void TestSimpleCheckCollisionDetection()
        //{
        //    cdtype = CollisionDetectionType.Simple;
        //}
        //[ContextMenu("八叉树检测")]
        //public void TestOctTree()
        //{
        //    cdtype = CollisionDetectionType.OctTree;
        //}
        //#endregion
    }
}

