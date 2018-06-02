using System;
using System.Collections.Generic;
using UnityEngine;
namespace ScriptableObjectManager
{
    [Serializable]
    public class ScriptableObjectDictionary<TKey, TValue> : ScriptableObject
    {
        [SerializeField]
        [HideInInspector]
        private List<TKey> keys = new List<TKey>();

        [SerializeField]
        public List<TValue> values = new List<TValue>();

        protected Dictionary<TKey, TValue> target;

        public Dictionary<TKey, TValue> Target
        {
            get
            {
                int size = keys.Count;
                if (null == target || 0 == target.Count)
                {
                    target = new Dictionary<TKey, TValue>();
                    for (int i = 0; i < size; i++) target.Add(keys[i], values[i]);
                }

                return target;
            }

            set
            {
                target = value;
                keys = new List<TKey>(target.Keys);
                values = new List<TValue>(target.Values);
            }
        }
    }
}