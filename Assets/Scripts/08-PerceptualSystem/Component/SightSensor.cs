using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using DG.Tweening;

namespace LinhowePerceptualSystem
{
    /// <summary>
    /// 视觉感知器
    /// </summary>
    public class SightSensor:BaseSensor
    {

        //视域范围角度
        public float fieldView = 45f;

        //所能看到的最远距离
        public float viewDistance = 100f;

        private SectorMeshCreator creator = new SectorMeshCreator();
        private MeshFilter meshFilter;
        private Mesh mesh;
        private float lastfieldView, lastviewDistance;
        private int updateAction = 0;
        protected override void Awake()
        {
            meshFilter = GetComponent<MeshFilter>();
            animator = GetComponent<Animator>();
            PerceptualTyep = PerceptualEnum.Sight;
            _rigidbody = GetComponent<Rigidbody>();
            base.Awake();

        }
        private void Update()
        {
            updateAction++;
            if(updateAction>40)
            {
                updateAction = 0;
                animator.Play("idle");
                float randomY = UnityEngine.Random.Range(-90, 90);
                _rigidbody.velocity = Vector3.zero;
                transform.DORotate(new Vector3(0, randomY, 0),0.3f);
                
            }
        }
        public override void Notify(BaseTraigger traigger)
        {
            updateAction = 0;
            Debug.Log(name + " see " + traigger.name + "!");
            Vector3 direction = traigger.transform.position - transform.position;
            Vector3.Normalize(direction);
            if(Vector3.Distance(traigger.transform.position, transform.position)>1.5f)
            {
                if(!animator.GetCurrentAnimatorStateInfo(0).IsName("walk"))
                    animator.Play("walk");
                _rigidbody.velocity = direction / 100;
                


            }
            else
            {
                
                if (Vector3.Angle(direction,transform.forward)>=10)
                    transform.LookAt(traigger.transform.position);
                if(!animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
                    animator.Play("attack");
            }

        }
       
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            DrawMesh();
        }

        private void DrawMesh()
        {
            if (lastfieldView != fieldView || lastviewDistance != viewDistance)
            {
                lastfieldView = fieldView;
                lastviewDistance = viewDistance;
                mesh = creator.CreateMesh
               (viewDistance, fieldView * 2, 30, 1000, 1000);
            }
           
            int[] tris = mesh.triangles;
            for (int i = 0; i < tris.Length; i += 3)
            {
                Gizmos.DrawLine(convert2World(mesh.vertices[tris[i]]), convert2World(mesh.vertices[tris[i + 1]]));
                Gizmos.DrawLine(convert2World(mesh.vertices[tris[i]]), convert2World(mesh.vertices[tris[i + 2]]));
                Gizmos.DrawLine(convert2World(mesh.vertices[tris[i + 1]]), convert2World(mesh.vertices[tris[i + 2]]));
            }
        }

        private Vector3 convert2World(Vector3 src)
        {
            return transform.TransformPoint(src);
        }
    }




}
