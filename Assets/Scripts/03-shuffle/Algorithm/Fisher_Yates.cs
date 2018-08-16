using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LinHoweShuffle
{
    public class Fisher_Yates
    {        
        public static void Shuffle(Pukes pukes)
        {
            List<int> list = new List<int>(pukes.pukes);
            for(int i = 0 ; i < pukes.pukes.Length ; ++i)
            {
                int randomIndex = Random.Range(0, list.Count);
                int r = list[randomIndex];
                pukes.Swap(i, r);
                list.RemoveAt(randomIndex);
            }
        }
    }
}

