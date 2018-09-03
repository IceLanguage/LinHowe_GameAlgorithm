using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace LinHoweKnifeTail
{
    /// <summary>
    /// 贝塞尔曲线
    /// </summary>
    public class BezierCurve
    {
        //计算后曲线
        public List<Vector3> Curve { get; set; }
        private Vector3[] points;

        //防止重复计算多项式系数
        private Dictionary<int, float> helpDic = new Dictionary<int, float>();

        private int PointNumber;
        public BezierCurve(Vector3[] points,int PointNumber)
        {
           
            this.points = points;
            
            this.PointNumber = PointNumber;
            Curve = new List<Vector3>(PointNumber+1);

            float s = 1 / (float)PointNumber;
            for(int i = 0;i <= PointNumber; ++i)
            {
                float t = s * i;
                Curve.Add(PointOnCubicBezier(t));

            }
        }

        private Vector3 PointOnCubicBezier(float t)
        {
           
            Vector3 res = Vector3.zero;
            for(int i = 0;i<points.Length;++i)
            {
                //计算多项式系数
                float p = help(i);
                float q = Mathf.Pow(t, i);
                float m = Mathf.Pow(1 - t, points.Length - i - 1);
                res += p * points[i] * q * m;
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
            for (int j = 0;j < i;++j)
            {
                res *= points.Length - 1 - j ;
                res /= j + 1;
            }
            helpDic.Add(i, res);
            return res;
        }

    }
}
