using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LinHoweKnifeTail
{
    /// <summary>
    /// B样条曲线
    /// </summary>
    public class BasicSplineCurve
    {
        //计算后曲线
        public List<Vector3> Curve { get; set; }
        private Vector3[] points;
        //防止重复计算多项式系数
        private Dictionary<int, float> helpDic = new Dictionary<int, float>();
        private Dictionary<int, int> factorialDic = new Dictionary<int, int>();
        private int PointNumber;

        public BasicSplineCurve(Vector3[] points, int PointNumber)
        {
            this.points = points;

            this.PointNumber = PointNumber;
            Curve = new List<Vector3>(PointNumber+1);

            float s = 1 / (float)PointNumber;
            for (int i = 0; i <= PointNumber; ++i)
            {
                float t = s * i;
                Curve.Add(PointOnCubicBezier(t));

            }
        }

        private Vector3 PointOnCubicBezier(float t)
        {
           
            Vector3 res = Vector3.zero;
            for (int i = 0; i < points.Length; ++i)
            {
                res += GetF(i,t) * points[i];
            }

            return res;
        }

        private float GetF(int i,float t)
        {
            int f = factorial(points.Length - 1);
            float res = 0;
            for(int j = 0;j < points.Length - i - 1; ++j)
            {
                res += help(j) * Mathf.Pow(-1,j) * Mathf.Pow((t + points.Length - 1- i - j), points.Length - 1);
            }
           
            return res/f;
        }
        /// <summary>
        /// 阶乘
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private int factorial(int i)
        {
            if (factorialDic.ContainsKey(i))
                return factorialDic[i];

            int res = 1;
            for(int j = 2;j <= i;j++)
            {
                res *= j;
            }
            return res;
        }
            /// <summary>
            /// 计算多项式系数
            /// </summary>
            /// <param name="i"></param>
            /// <returns></returns>
        private float help(int i)
        {

            float res = 1;
            if (helpDic.ContainsKey(i))
                return helpDic[i];
            for (int j = 0; j < i; ++j)
            {
                res *= points.Length  - j;
                res /= j + 1;
            }
            helpDic.Add(i, res);
            return res;
        }
    }
}
