using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
//来源http://www.unity.5helpyou.com/3095.html
namespace LHCoroutine
{
    internal class CoroutineManagerMonoBehaviour : MonoBehaviour
    {
    }

    public class CoroutineManager
    {
        private static CoroutineManagerMonoBehaviour _CoroutineManagerMonoBehaviour;

        static CoroutineManager()
        {
            Init();
        }

        public static void DoCoroutine(IEnumerator routine)
        {
            _CoroutineManagerMonoBehaviour.StartCoroutine(routine);
        }

        private static void Init()
        {
            var go = new GameObject();
            go.name = "CoroutineManager";
            _CoroutineManagerMonoBehaviour = go.AddComponent<CoroutineManagerMonoBehaviour>();
            GameObject.DontDestroyOnLoad(go);
        }
    }

}
