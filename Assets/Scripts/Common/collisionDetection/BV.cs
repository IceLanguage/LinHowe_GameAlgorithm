using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CollisionDetection
{
    /// <summary>
    /// 轴对齐包围盒
    /// </summary>
    public struct AABB
    {

        /// <summary>
        /// 中心点
        /// </summary>
        public readonly Vector3 center;
        /// <summary>
        /// 半径
        /// </summary>
        public readonly Vector3 radius;

        public Vector3 Min
        {
            get
            {
                return new Vector3(
                    center.x - Math.Abs(radius.x),
                    center.y - Math.Abs(radius.y),
                    center.z - Math.Abs(radius.z));
            }
        }
        public Vector3 Max
        {
            get
            {
                return new Vector3(
                    center.x + Math.Abs(radius.x),
                    center.y + Math.Abs(radius.y),
                    center.z + Math.Abs(radius.z));
            }
        }
        /// <summary>
        /// 获取所有面
        /// </summary>
        /// <returns></returns>
        public List<Plane> getPlanes()
        {
            List<Plane> planes = new List<Plane>();
            Vector3 normal = new Vector3(1, 0, 0);
            Vector3 p = center + new Vector3(radius.x, 0, 0);
            planes.Add(new Plane(Vector3.Dot(p, normal), normal));
            normal = new Vector3(-1, 0, 0);
            p = center + new Vector3(-radius.x, 0, 0);
            planes.Add(new Plane(Vector3.Dot(p, normal), normal));
            normal = new Vector3(0, 1, 0);
            p = center + new Vector3(0, radius.y, 0);
            planes.Add(new Plane(Vector3.Dot(p, normal), normal));
            normal = new Vector3(0, -1, 0);
            p = center + new Vector3(0, -radius.y, 0);
            planes.Add(new Plane(Vector3.Dot(p, normal), normal));
            normal = new Vector3(0, 0, 1);
            p = center + new Vector3(0, 0, radius.z);
            planes.Add(new Plane(Vector3.Dot(p, normal), normal));
            normal = new Vector3(0, 0, -1);
            p = center + new Vector3(0, 0, -radius.z);
            planes.Add(new Plane(Vector3.Dot(p, normal), normal));
            return planes;
        }

        //获取最近的面
        public Plane GetClosestPlane(Vector3 point)
        {
            List<Plane> planes = getPlanes();
            Plane res = planes[0];
            float minDis =Math.Abs( BVMath.Distance_Plane_Point(planes[0], point));
            for(int i =1;i<6;++i)
            {
                float Dis = Math.Abs(BVMath.Distance_Plane_Point(planes[i], point));
                if(minDis>Dis)
                {
                    res = planes[i];
                    minDis = Dis;
                }
            }
            return res;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="center">中心坐标</param>
        /// <param name="r">半径</param>
        public AABB(Vector3 center,Vector3 radius)
        {
            this.center = center;
            this.radius = new Vector3(Math.Abs(radius.x), Math.Abs(radius.y), Math.Abs(radius.z));
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if ((obj.GetType().Equals(this.GetType())) == false)
            {
                return false;
            }

            AABB temp = (AABB)obj;

            return this.center.Equals(temp.center) && this.radius.Equals(temp.radius);
        }
        
        public override int GetHashCode()
        {
            return this.center.GetHashCode() ^ this.radius.GetHashCode();
        }

        public static bool operator ==(AABB leftHandSide, AABB rightHandSide)
        {
            return (leftHandSide.Equals(rightHandSide));
        }
        public static bool operator !=(AABB leftHandSide, AABB rightHandSide)
        {
            return !(leftHandSide == rightHandSide);
        }
    }

    /// <summary>
    /// 球体
    /// </summary>
    public struct Sphere
    {
        /// <summary>
        /// 中心点
        /// </summary>
        public readonly Vector3 center;
        public readonly float r;

        public Sphere(Vector3 center,float radius)
        {
            this.center = center;
            r = radius;
        }                                                
    }

    /// <summary>
    /// 方向包围盒
    /// </summary>
    public struct OBB
    {
        /// <summary>
        /// 中心点
        /// </summary>
        public readonly Vector3 center;
        /// <summary>
        /// 半径
        /// </summary>
        public readonly Vector3 radius;

        /// <summary>
        /// 轴
        /// </summary>
        public readonly Vector3[] axes;
        public OBB(AABB a,Vector3 axesX, Vector3 axesY, Vector3 axesZ)
        {
            center = a.center;
            radius = a.center;
            axes = new Vector3[3];
            axes[0] = axesX;
            axes[1] = axesY;
            axes[2] = axesZ;
        }
    }

    /// <summary>
    /// 球扫掠体(圆柱体)
    /// </summary>
    public struct Capsule
    {
        public readonly Vector3 a, b;
        public readonly float r;
        public Line Line
        {
            get
            {
                return new Line(a, b);
            }
        }
        public Capsule(Vector3 a,Vector3 b,float r)
        {
            this.a = a;
            this.b = b;
            this.r = r;
        }
    }

    /// <summary>
    /// 线段
    /// </summary>
    public struct Line
    {
        public readonly Vector3 a, b;
        public Line(Vector3 a,Vector3 b)
        {
            this.a = a;
            this.b = b;
        }
    }

    /// <summary>
    /// 平面
    /// </summary>
    public struct Plane
    {
        //normal * x = d;
        public readonly float d;
        public readonly Vector3 normal;
        public Plane(float d,Vector3 normal)
        {
            this.d = d;
            this.normal = Vector3.Normalize(normal);
        }
    }
}
