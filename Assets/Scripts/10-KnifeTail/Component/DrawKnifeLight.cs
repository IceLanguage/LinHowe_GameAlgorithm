using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LinHoweKnifeTail
{
    public class DrawKnifeLight : MonoBehaviour
    {
        //public Material mat;
        public Transform KnifeTip;

        private Queue<Vector3> Points = new Queue<Vector3>();

        private void Update()
        {
            Vector2 v = Camera.main.WorldToScreenPoint(KnifeTip.position);
            Vector3 newv = new Vector3(v.x, v.y, 0);
            if(0 == Points.Count || newv != Points.Last())
                Points.Enqueue(newv);
            

        }
        
        void OnPostRender()
        {
            //if (!mat)
            //{
            //    Debug.LogError("Please Assign a material on the inspector");
            //    return;
            //}
            //GL.PushMatrix(); //保存当前Matirx  
            //mat.SetPass(0); //刷新当前材质  
            GL.LoadPixelMatrix();//设置pixelMatrix  
            GL.Color(Color.black);
            int count = Points.Count;
           
            while(Points.Count>3)
            {
                Points.Dequeue();
            }
            var bezierCurve =  new BezierCurve(Points.ToArray(), 100);
            for (int i = 0; i < bezierCurve.Curve.Count - 1; ++i)
            {
                GL.Begin(GL.LINES);
                GL.Vertex(bezierCurve.Curve[i]);
                GL.Vertex(bezierCurve.Curve[i + 1]);
                GL.End();
            }

            
            //GL.PopMatrix();//读取之前的Matrix  
        }
    }
}
