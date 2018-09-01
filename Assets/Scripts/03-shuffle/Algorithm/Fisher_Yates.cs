using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LinHoweShuffle
{
    /// <summary>
    /// 高纳德置乱算法
    /// </summary>
    public class Fisher_Yates
    {        
        public static void Shuffle(Pukes pukes)
        {
            //取两个列表，一个是洗牌前的序列A{ 1,2….54)，一个用来放洗牌后的序列B，B初始为空
            //while A不为空
            //  随机从A取一张牌加入B末尾

            List<int> list = new List<int>(pukes.pukes);
            List<int> newlist = new List<int>(list.Count);
            for (int i = 0; i < pukes.pukes.Length; ++i)
            {
                int randomIndex = Random.Range(0, list.Count);
                int r = list[randomIndex];
                newlist.Add(r);
                list.RemoveAt(randomIndex);
            }
            pukes.ResetPuke(newlist.ToArray());
            
        }
    }
}

