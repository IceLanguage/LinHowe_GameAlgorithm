using System.Collections;
using System.Collections.Generic;
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
            Points.Enqueue(new Vector3(v.x, v.y, 0));
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
            //GL.Color(Color.black);
            int count = Points.Count;
            var list = Points.ToArray();
            for(int i = 0;i < count - 1;++i)
            {
                GL.Begin(GL.LINES);
                GL.Vertex(list[i]);
                GL.Vertex(list[i + 1]);
                GL.End();
            }
            if(Points.Count>300)
                Points.Dequeue();
            //GL.Begin(GL.LINES);
            //GL.Vertex3(0, 0, 0);
            //GL.Vertex3(v.x, v.y, 0);
            //GL.End();
            //GL.PopMatrix();//读取之前的Matrix  
        }
    }
}
