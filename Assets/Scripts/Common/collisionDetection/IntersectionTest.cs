using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CollisionDetection
{
    /// <summary>
    /// 相交性测试
    /// </summary>
    public static class IntersectionTest
    {
        public static bool Check_AABB_AABB(AABB a,AABB b)
        {
            if (Math.Abs(a.center.x - b.center.x) > a.radius.x + b.radius.x)
                return false;
            if (Math.Abs(a.center.y - b.center.y) > a.radius.y + b.radius.y)
                return false;
            if (Math.Abs(a.center.z - b.center.z) > a.radius.z + b.radius.z)
                return false;
            return true;
        }   

        public static bool Check_Sphere_Sphere(Sphere a,Sphere b)
        {
           
            Vector3 v = a.center - b.center;
            float dis2 = Vector3.Dot(v, v);
            float radiusSum = a.r + b.r;
            return dis2 <= radiusSum * radiusSum;
        }

        public static bool Check_OBB_OBB(OBB a,OBB b)
        {
            //轴半径
            float ra = 0, rb = 0;

            //旋转矩阵
            float[,] R = new float[3, 3];
            float[,] ABSR = new float[3, 3];

            //计算A到B的旋转矩阵
            for (int i = 0; i < 3; ++i)
                for (int j = 0; i < 3; ++j)
                    R[i,j] = Vector3.Dot(a.axes[i], b.axes[j]);
            Vector3 t = a.center - b.center;

            //将t转入a的坐标系
            t = new Vector3(
                Vector3.Dot(t, a.axes[0]),
                Vector3.Dot(t, a.axes[1]),
                Vector3.Dot(t, a.axes[2]));

            for (int i = 0; i < 3; ++i)
                for (int j = 0; i < 3; ++j)
                    ABSR[i, j] = Math.Abs(R[i, j]) + float.Epsilon;

            for (int i = 0; i < 3; ++i)
            {
                ra = a.radius[i];
                rb = b.radius[0] * ABSR[i, 0] +
                     b.radius[1] * ABSR[i, 1] +
                     b.radius[2] * ABSR[i, 2];
                if (Math.Abs(t[i]) > ra + rb) return false;
            }

            for (int i = 0; i < 3; ++i)
            {
                rb = b.radius[i];
                ra = a.radius[0] * ABSR[0, i] +
                     a.radius[1] * ABSR[1, i] +
                     a.radius[2] * ABSR[2, i];
                if (Math.Abs(t[0] * R[0,i] +
                             t[1] * R[1,i] +
                             t[2] * R[2,i]) > ra + rb)
                    return false;
            }

            ra = a.radius[1] * ABSR[2, 0] + a.radius[2] * ABSR[1, 0];
            rb = b.radius[1] * ABSR[0, 2] + b.radius[2] * ABSR[0, 1];
            if (Math.Abs(t[2] * R[1, 0] - t[1] * R[2, 0]) > ra + rb)
                return false;

            ra = a.radius[1] * ABSR[2, 1] + a.radius[2] * ABSR[1, 1];
            rb = b.radius[0] * ABSR[0, 2] + b.radius[2] * ABSR[0, 0];
            if (Math.Abs(t[2] * R[1, 1] - t[1] * R[2, 1]) > ra + rb)
                return false;

            ra = a.radius[1] * ABSR[2, 2] + a.radius[2] * ABSR[1, 2];
            rb = b.radius[0] * ABSR[0, 1] + b.radius[1] * ABSR[0, 0];
            if (Math.Abs(t[2] * R[1, 2] - t[1] * R[2, 2]) > ra + rb)
                return false;

            ra = a.radius[0] * ABSR[2, 0] + a.radius[2] * ABSR[0, 0];
            rb = b.radius[1] * ABSR[1, 2] + b.radius[2] * ABSR[1, 1];
            if (Math.Abs(t[0] * R[2, 0] - t[2] * R[0, 0]) > ra + rb)
                return false;

            ra = a.radius[0] * ABSR[2, 1] + a.radius[2] * ABSR[0, 1];
            rb = b.radius[0] * ABSR[1, 2] + b.radius[2] * ABSR[1, 0];
            if (Math.Abs(t[0] * R[2, 1] - t[2] * R[0, 1]) > ra + rb)
                return false;

            ra = a.radius[0] * ABSR[2, 2] + a.radius[2] * ABSR[0, 2];
            rb = b.radius[0] * ABSR[1, 1] + b.radius[1] * ABSR[1, 0];
            if (Math.Abs(t[0] * R[2, 2] - t[2] * R[0, 2]) > ra + rb)
                return false;

            ra = a.radius[0] * ABSR[1, 0] + a.radius[1] * ABSR[0, 0];
            rb = b.radius[1] * ABSR[2, 2] + b.radius[2] * ABSR[2, 1];
            if (Math.Abs(t[1] * R[0, 0] - t[0] * R[1, 0]) > ra + rb)
                return false;

            ra = a.radius[0] * ABSR[1, 1] + a.radius[1] * ABSR[0, 1];
            rb = b.radius[0] * ABSR[2, 2] + b.radius[2] * ABSR[2, 0];
            if (Math.Abs(t[1] * R[0, 1] - t[0] * R[1, 1]) > ra + rb)
                return false;

            ra = a.radius[0] * ABSR[1, 2] + a.radius[1] * ABSR[0, 2];
            rb = b.radius[0] * ABSR[2, 1] + b.radius[1] * ABSR[2, 0];
            if (Math.Abs(t[1] * R[0, 2] - t[0] * R[1, 2]) > ra + rb)
                return false;

            return true;
        }

        public static bool Check_Sphere_Capsule(Sphere a,Capsule b)
        {
            float dis2 = BVMath.Square_Distance_Line_Point(b.Line,a.center);
            float r = a.r + b.r;
            return dis2 <= r * r;
        }

        public static bool Check_Capsule_Capsule(Capsule a, Capsule b)
        {
            Vector3 p1 = Vector3.zero, p2 = Vector3.zero;
            float dis2 = BVMath.MinPoints_Line_line(
                new Line(a.a, a.b),
                new Line(b.a, b.b),out p1,out p2);
            float r = a.r + b.r;
            
            return dis2 <= r * r;
        }

        public static bool Check_Sphere_Plane(Sphere sphere,Plane plane)
        {
            float dist = Vector3.Dot(sphere.center, plane.normal) - plane.d;
            return Math.Abs(dist) <= sphere.r;
        }

        public static bool Check_OBB_Plane(OBB obb,Plane plane)
        {
            float r =
                obb.radius[0] * Math.Abs(Vector3.Dot(plane.normal, obb.axes[0])) +
                obb.radius[1] * Math.Abs(Vector3.Dot(plane.normal, obb.axes[1])) +
                obb.radius[2] * Math.Abs(Vector3.Dot(plane.normal, obb.axes[2]));

            float s = Vector3.Dot(plane.normal, obb.center) - plane.d;
            return Math.Abs(s) <= r;
        }

        public static bool Check_AABB_Plane(AABB aabb,Plane plane)
        {
            float r =
                aabb.radius[0] * Math.Abs(plane.normal[0]) +
                aabb.radius[1] * Math.Abs(plane.normal[1]) +
                aabb.radius[2] * Math.Abs(plane.normal[2]);

            float s = Vector3.Dot(plane.normal, aabb.center) - plane.d;
            return Math.Abs(s) <= r;
        }

        public static bool Check_Sphere_AABB(Sphere sphere,AABB aabb,out Vector3 closestPoint)
        {
            closestPoint = BVMath.ClosestPt_AABB_Point(aabb, sphere.center);
            
            Vector3 v = closestPoint - sphere.center;
            return Vector3.Dot(v, v) <= sphere.r * sphere.r;
        }

        public static bool Check_Sphere_OBB(Sphere sphere, OBB obb, out Vector3 closestPoint)
        {
            closestPoint = BVMath.ClosetPt_OBB_Point(obb, sphere.center);

            Vector3 v = closestPoint - sphere.center;

            return Vector3.Dot(v, v) <= sphere.r * sphere.r;
        }

        /// <summary>
        /// 线段和平面相交性检测
        /// </summary>
        /// <param name="line"></param>
        /// <param name="plane"></param>
        /// <param name="intersectPoint"></param>
        /// <returns></returns>
        public static bool Check_Line_Plane(Line line,Plane plane,ref Vector3 intersectPoint)
        {
            Vector3 ab = line.b - line.a;
            float t = (plane.d - Vector3.Dot(plane.normal, line.a)) / Vector3.Dot(plane.normal, ab);
            if(t>=0.0f&&t<=1.0f)
            {
                intersectPoint = line.a + t * ab;

                return true;
            }
            return false;
        }

        /// <summary>
        /// 射线和平面相交性检测
        /// </summary>
        /// <param name="ray"></param>
        /// <param name="sphere"></param>
        /// <param name="intersectPoint"></param>
        /// <returns></returns>
        public static bool Check_Ray_Sphere(Line ray,Sphere sphere, ref Vector3 intersectPoint)
        {
            Vector3 m = ray.a - sphere.center;
            Vector3 d = ray.a - ray.b;
            float b = Vector3.Dot(m, d);
            float c = Vector3.Dot(m, m) - sphere.r * sphere.r;
            if (c > 0.0f && b > 0.0f) return false;
            float dis = b * b - c;
            if (dis < 0.0f) return false;
            float t = -b - Mathf.Sqrt(dis);
            if (t < 0.0f) t = 0.0f;
            intersectPoint = ray.a + t * d;
            return true;
        }

        /// <summary>
        /// 射线和AABB相交性检测
        /// </summary>
        /// <param name="ray"></param>
        /// <param name="aabb"></param>
        /// <param name="intersectPoint"></param>
        /// <returns></returns>
        public static bool Check_Ray_AABB(Line ray,AABB aabb, ref Vector3 intersectPoint)
        {
            Vector3 d = ray.a - ray.b;
            Vector3 min = aabb.Min;
            Vector3 max = aabb.Max;

            float tmin = 0.0f;
            float tmax = float.MaxValue;
            for (int i = 0; i < 3; ++i)
            {
                if (Math.Abs(d[i]) <= float.Epsilon)
                    if (ray.a[i] < min[i] || ray.a[i] > max[i])
                        return false;

                float ood = 1.0f / d[i];
                float t1 = (min[i] - ray.a[i]) * ood;
                float t2 = (max[i] - ray.a[i]) * ood;
                if (t1 > t2)
                {
                    float t3 = t1;
                    t1 = t2;
                    t2 = t3;
                }
                if (t1 > tmin) tmin = t1;
                if (t2 > tmin) tmax = t2;
                if (tmin > tmax) return false;
            }

            intersectPoint = ray.a + d * tmin;
            return true;
        }
    }
}
