using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LinHoweShuffle
{
    public class Fisher_Yates
    {        
        public static void Shuffle(Pukes pukes)
        {
            for(int i = 0 ; i < pukes.pukes.Length ; ++i)
            {
                int randomIndex = Random.Range(0, pukes.pukes.Length);
                pukes.Swap(i, randomIndex);
            }
        }
    }
}

