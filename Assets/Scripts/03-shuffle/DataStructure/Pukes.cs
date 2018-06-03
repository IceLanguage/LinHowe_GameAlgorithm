using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyUnityEventDispatcher;
namespace LinHoweShuffle
{
    /// <summary>
    /// 牌库
    /// </summary>
    public class Pukes
    {
        public int[] pukes;
        public Pukes(int size)
        {
            pukes = new int[size];
            InitPukes();
        }

        public void Swap(int i,int j)
        {
            NotificationCenter<KeyValuePair<int, int>>.Get().DispatchEvent
                ("SwapPuke", new KeyValuePair<int, int>(pukes[i], pukes[j]));
            int m = pukes[i];
            pukes[i] = pukes[j];
            pukes[j] = m;
            
        }

        /// <summary>
        /// 抽牌 i小于j
        /// </summary>
        /// <param name="i">被抽的牌</param>
        /// <param name="j">放的位置</param>
        public void Draw(int i,int j)
        {
            int num = pukes[i];
            
            for (int k = i;k < j;++k)
            {
                NotificationCenter<KeyValuePair<int, int>>.Get().DispatchEvent
                ("MovePuke", new KeyValuePair<int, int>(pukes[k+1], k));
                pukes[k] = pukes[k+1];
            }
            NotificationCenter<KeyValuePair<int, int>>.Get().DispatchEvent
               ("MovePuke", new KeyValuePair<int, int>( num, j));
            pukes[j] = num;
            

        }

        private void InitPukes()
        {
            for (int i = 0; i < pukes.Length; ++i)
                pukes[i] = i;
        }
    }
}
