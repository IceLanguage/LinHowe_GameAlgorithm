using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinHoweShuffle
{
    /// <summary>
    ///  Inside-Out Algorithm 算法的基本思想是
    ///  在[0, i]之间随机一个下标j，然后用位置j的元素替换掉位置i的数字
    /// </summary>
    class Inside_Out
    {
        public static void Shuffle(Pukes pukes)
        {
            int len = pukes.pukes.Length;
            for (int i = 0; i < len; ++i)
            {
                int randomIndex = Random.Range(0, i + 1);
                pukes.Swap(i, randomIndex);
            }
        }
    }
}
