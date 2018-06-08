using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LinHoweCollisionDetection
{
    public class AutoDestroy : MonoBehaviour
    {

        private void Awake()
        {
            StartCoroutine(DestroyThis());
        }
        IEnumerator DestroyThis()
        {
            yield return new WaitForSeconds(10);
            DestroyObject(gameObject);
        }
    }
}

