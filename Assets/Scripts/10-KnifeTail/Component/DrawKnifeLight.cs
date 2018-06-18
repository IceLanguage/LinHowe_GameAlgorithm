using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LinHoweKnifeTail
{
    public class DrawKnifeLight : MonoBehaviour
    {
        enum TestCurve
        {
            BasicSplineCurve,
            BezierCurve
        }
        public Material mat;
        public Transform KnifeTip;
        public int PointCount = 5;
        private Queue<Vector3> KnifeTipPoints = new Queue<Vector3>();
        private TestCurve testCurve = TestCurve.BasicSplineCurve;
        private void Update()
        {
            Vector2 v = Camera.main.WorldToScreenPoint(KnifeTip.position);
            Vector3 newv = new Vector3(v.x, v.y, 0);
            if(0 == KnifeTipPoints.Count || newv != KnifeTipPoints.Last())
                KnifeTipPoints.Enqueue(newv);


        }
        
        void OnPostRender()
        {
            if (!mat)
            {
                Debug.LogError("Please Assign a material on the inspector");
                return;
            }
            GL.PushMatrix(); //保存当前Matirx  
            mat.SetPass(0); //刷新当前材质  
            GL.LoadPixelMatrix();//设置pixelMatrix  
           
            while(KnifeTipPoints.Count> PointCount)
            {
                KnifeTipPoints.Dequeue();
            }
            int i = 0;

            List<Vector3> list = new List<Vector3>();
            int count = 1000;
            switch (testCurve)
            {
                case TestCurve.BezierCurve:
                    var KnifeTipbezierCurve1= new BezierCurve(KnifeTipPoints.ToArray(), count);
                    list = KnifeTipbezierCurve1.Curve;
                    break;
                case TestCurve.BasicSplineCurve:
                    var KnifeTipbezierCurve2 = new BasicSplineCurve(KnifeTipPoints.ToArray(), count);
                    list = KnifeTipbezierCurve2.Curve;
                    break;
                default:
                    break;
            }
           
            
            

            for (; i < count - 1; ++i)
            {
                Vector3 v = list[i];
                GL.Begin(GL.LINES);
                GL.Vertex(v);
                GL.Vertex(list[i + 1]);
                GL.End();


            }
           


            GL.PopMatrix();//读取之前的Matrix  
        }
        [ContextMenu("测试B样条曲线")]
        public void TestBasicSplineCurve()
        {
            testCurve = TestCurve.BasicSplineCurve;
        }

        [ContextMenu("测试贝塞尔曲线")]
        public void TestBezierCurve()
        {
            testCurve = TestCurve.BezierCurve;
        }
    }
}
