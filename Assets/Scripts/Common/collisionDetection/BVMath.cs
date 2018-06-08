using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CollisionDetection
{
    /// <summary>
    /// 图元计算
    /// </summary>
    public static class BVMath
    {

        /// <summary>
        /// 点至线段的最近点
        /// </summary>
        /// <param name="line"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static Vector3 ClosestPt_Line_Point(Line line,Vector3 point)
        {
            Vector3 ab = line.b - line.a;

            float t = Vector3.Dot(point - line.a, ab)/ Vector3.Dot(ab,ab);
            if (t < 0.0f) t = 0.0f;
            if (t > 1.0f) t = 1.0f;

            return line.a + t * ab;
        }
        /// <summary>
        /// 点到线段的距离的平方
        /// </summary>
        /// <param name="line"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static float Square_Distance_Line_Point(Line line,Vector3 p)
        {
            Vector3 ab = line.b - line.a;
            Vector3 ac = p - line.a;
            Vector3 bc = p - line.b;
            float e = Vector3.Dot(ac, ab);
            if(e <= 0) return Vector3.Dot(ac, ac);
            float f = Vector3.Dot(ab, ab);
            if(e >= f) return Vector3.Dot(bc, bc);
            return Vector3.Dot(ac, ac) - e * e / f;
        }


        /// <summary>
        /// 求两线段上的最近点
        /// </summary>
        /// <param name="p">线段p</param>
        /// <param name="q">线段q</param>
        /// <param name="va">返回线段p上的点va</param>
        /// <param name="vb">返回线段q上的点vb</param>
        /// <returns>最近点距离的平方</returns>
        public static float MinPoints_Line_line(
            Line p,Line q,
            out Vector3 va,out Vector3 vb)
        {
            Vector3 d1 = p.b - p.a;
            Vector3 d2 = q.b - q.a;
            Vector3 r = p.a - q.a;

            float a = Vector3.Dot(d1, d1);
            float b = Vector3.Dot(d2, d2);
            float f = Vector3.Dot(d2, r);

            if(a <= float.Epsilon && b <= float.Epsilon)
            {
                va = p.a;
                vb = q.a;
                return Vector2.Dot(va - vb, va - vb);
            }

            if (a <= float.Epsilon)
            {
                va = p.a;
                vb = q.a + d2 * f / b;
                return Vector2.Dot(va - vb, va - vb);
            }

            float c = Vector2.Dot(d1, r);

            if (b <= float.Epsilon)
            {
                va = p.a + d1 * -c / a;
                vb = q.a;
                return Vector2.Dot(va - vb, va - vb);
            }

            float e = Vector2.Dot(d1, d2);
            float denom = a * b - e * e;

            float s;
            if (denom != 0.0f)
                s = (e * f - c * b) / denom;
            else s = 0.0f;

            float t = (e * s + f) / b;
            
            if(t<0.0f)
            {
                t = 0.0f;
                s = -c / a;
            }
            else
            {
                t = 1;
                s = (b - c) / a;
            }

            va = p.a + d1 * s;
            vb = q.a + d2 * t;
            return Vector2.Dot(va - vb, va - vb);
        }

       /// <summary>
       /// 计算平面上距离一个点最近的点
       /// </summary>
       /// <param name="point"></param>
       /// <param name="plane"></param>
       /// <returns></returns>
       public static Vector3 ClosestPt_Point_plane(Vector3 point,Plane plane)
        {
            float t = Distance_Plane_Point(plane, point);
            return point - t * plane.normal;
        }

        /// <summary>
        /// 计算一个平面和一个点间的最短距离
        /// </summary>
        /// <param name="plane"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static float Distance_Plane_Point(Plane plane,Vector3 point)
        {
            float t = Vector3.Dot(plane.normal, point) - plane.d;
            return t;
        }

        /// <summary>
        /// 点至AABB的最近点
        /// </summary>
        /// <param name="aabb"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static Vector3 ClosestPt_AABB_Point(AABB aabb,Vector3 point)
        {
            Vector3 p = Vector3.zero;
            Vector3 min = aabb.Min;
            Vector3 max = aabb.Max;
            for(int i = 0;i < 3;++i)
            {
                float v = point[i];
                if (v < min[i]) v = min[i];
                else if (v > max[i]) v = max[i];
                p[i] = v;
            }
            return p;
        }

        /// <summary>
        /// 点至AABB距离的平方
        /// </summary>
        /// <param name="point"></param>
        /// <param name="aabb"></param>
        /// <returns></returns>
        public static float Square_Distance_Point_AABB(Vector3 point,AABB aabb)
        {
            float sqDis = 0.0f;
            Vector3 min = aabb.Min;
            Vector3 max = aabb.Max;
            for (int i = 0; i < 3; ++i)
            {
                float v = point[i];
                if (v < min[i])
                    sqDis += (min[i] - v) * (min[i] - v);
                else if (v > max[i])
                    sqDis += (max[i] - v) * (max[i] - v);
            }
            return sqDis;
        }

        /// <summary>
        /// 点至OBB的最近点
        /// </summary>
        /// <param name="obb"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static Vector3 ClosetPt_OBB_Point(OBB obb,Vector3 point)
        {
            Vector3 d = point - obb.center;
            Vector3 res = obb.center;
            for(int i = 0;i<3;++i)
            {
                float dist = Vector3.Dot(d, obb.axes[i]);
                if (dist > obb.radius[i]) dist = obb.radius[i];
                if (dist < -obb.radius[i]) dist = -obb.radius[i];
                res += dist * obb.axes[i];
            }
            return res;
        }

        /// <summary>
        /// 点到OBB距离的平方
        /// </summary>
        /// <param name="point"></param>
        /// <param name="obb"></param>
        /// <returns></returns>
        public static float Square_Distance_Point_OBB(Vector3 point,OBB obb)
        {
            Vector3 v = point - obb.center;
            float sqDis = 0.0f;
            for (int i = 0; i < 3; ++i)
            {
                float dist = Vector3.Dot(v, obb.axes[i]);
                float excess = 0.0f;
                if (dist > obb.radius[i]) excess = dist + obb.radius[i];
                if (dist < -obb.radius[i]) excess = dist - obb.radius[i];
                sqDis += excess * excess;
            }
            return sqDis;
        }

    }
}
