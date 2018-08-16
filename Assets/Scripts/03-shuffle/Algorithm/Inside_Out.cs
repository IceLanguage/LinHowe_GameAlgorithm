using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinHoweShuffle
{
    /// <summary>
    ///  Inside-Out Algorithm 算法的基本思想是设一游标i从前向后扫描原始数据的拷贝，
    ///  在[0, i]之间随机一个下标j，然后用位置j的元素替换掉位置i的数字，
    ///  再用原始数据位置i的元素替换掉拷贝数据位置j的元素。
    ///  其作用相当于在拷贝数据中交换i与j位置处的值
    /// </summary>
    class Inside_Out
    {
        public static void Shuffle(Pukes pukes)
        {
            int len = pukes.pukes.Length;
            for (int i = 0; i < len; ++i)
            {
                int randomIndex = Random.Range(0, len);
                pukes.Swap(i, randomIndex);
            }
        }
    }
}
