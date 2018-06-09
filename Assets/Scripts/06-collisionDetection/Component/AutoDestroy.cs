using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LinHoweCollisionDetection
{
    public class AutoDestroy : MonoBehaviour
    {
        private new Rigidbody rigidbody;
        private void Awake()
        {
            StartCoroutine(DestroyThis());
            rigidbody = GetComponent<Rigidbody>();
        }
        IEnumerator DestroyThis()
        {
            yield return new WaitForSeconds(10);
            DestroyObject(gameObject);
        }

        private void Update()
        {
            if(rigidbody.velocity.magnitude < 0.1f)
                DestroyObject(gameObject);
        }
    }
}

