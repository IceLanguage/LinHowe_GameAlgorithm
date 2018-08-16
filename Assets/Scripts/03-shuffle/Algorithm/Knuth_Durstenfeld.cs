using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinHoweShuffle
{
    /// <summary>
    /// Knuth 和Durstenfeld 在Fisher 等人的基础上对算法进行了改进。
    /// 每次从未处理的数据中随机取出一个数字，然后把该数字放在数组的尾部，
    /// 即数组尾部存放的是已经处理过的数字 。
    /// 这是一个原地打乱顺序的算法，算法时间复杂度也从Fisher算法的 O ( n 2 )提升到了 O ( n )。
    /// </summary>
    class Knuth_Durstenfeld
    {
        public static void Shuffle(Pukes pukes)
        {
            for (int i = pukes.pukes.Length - 1; i >=0; --i)
            {
                int randomIndex = Random.Range(0, i + 1);
                pukes.Swap(i, randomIndex);
            }
        }
    }
}
