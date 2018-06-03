using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinHoweShuffle
{
    /// <summary>
    /// 抽牌法
    /// </summary>
    public class Draw
    {
        public static void Shuffle(Pukes pukes)
        {
           
            for (int i = 0; i < pukes.pukes.Length - 1; ++i)
            {
                int randomIndex = Random.Range(0, pukes.pukes.Length - i - 1);
                pukes.Draw(randomIndex, pukes.pukes.Length - i - 1);
            }
        }
    }
}
