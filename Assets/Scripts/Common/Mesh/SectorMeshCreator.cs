using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

//来源https://my.csdn.net/awnuxcvbn
public class SectorMeshCreator
{
    private float radius;
    private float angleDegree;
    private int segments;

    private Mesh cacheMesh;

    /// <summary>  
    /// 创建一个扇形Mesh  
    /// </summary>  
    /// <param name="radius">扇形半价</param>  
    /// <param name="angleDegree">扇形角度</param>  
    /// <param name="segments">扇形弧线分段数</param>  
    /// <param name="angleDegreePrecision">扇形角度精度（在满足精度范围内，认为是同个角度）</param>  
    /// <param name="radiusPrecision">  
    /// <pre>  
    /// 扇形半价精度（在满足半价精度范围内，被认为是同个半价）。  
    /// 比如：半价精度为1000，则：1.001和1.002不被认为是同个半径。因为放大1000倍之后不相等。  
    /// 如果半价精度设置为100，则1.001和1.002可认为是相等的。  
    /// </pre>  
    /// </param>  
    /// <returns></returns>  
    public Mesh CreateMesh(float radius, float angleDegree, int segments, int angleDegreePrecision, int radiusPrecision)
    {
        if (checkDiff(radius, angleDegree, segments, angleDegreePrecision, radiusPrecision))
        {
            Mesh newMesh = Create(radius, angleDegree, segments);
            if (newMesh != null)
            {
                cacheMesh = newMesh;
                this.radius = radius;
                this.angleDegree = angleDegree;
                this.segments = segments;
            }
        }
        return cacheMesh;
    }

    private Mesh Create(float radius, float angleDegree, int segments)
    {

        if (segments == 0)
        {
            segments = 1;
#if UNITY_EDITOR
            Debug.Log("segments must be larger than zero.");
#endif
        }

        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[3 + segments - 1];
        vertices[0] = new Vector3(0, 0, 0);

        float angle = Mathf.Deg2Rad * angleDegree;
        float currAngle = angle / 2;
        float deltaAngle = angle / segments;
        for (int i = 1; i < vertices.Length; i++)
        {
            vertices[i] = new Vector3(Mathf.Sin(currAngle) * radius, 0, Mathf.Cos(currAngle) * radius);
            currAngle -= deltaAngle;
        }

        int[] triangles = new int[segments * 3];
        for (int i = 0, vi = 1; i < triangles.Length; i += 3, vi++)
        {
            triangles[i] = 0;
            triangles[i + 1] = vi;
            triangles[i + 2] = vi + 1;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        Vector2[] uvs = new Vector2[vertices.Length];
        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(vertices[i].x, vertices[i].z);
        }
        mesh.uv = uvs;

        return mesh;
    }

    private bool checkDiff(float radius, float angleDegree, int segments, int angleDegreePrecision, int radiusPrecision)
    {
        return segments != this.segments || (int)((angleDegree - this.angleDegree) * angleDegreePrecision) != 0 ||
               (int)((radius - this.radius) * radiusPrecision) != 0;
    }
}
