using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LinHoweGameTree
{

    /// <summary>
    /// 五子棋AI
    /// </summary>
    public class gobangAI
    {
        private int[,] historyScore = new int[15, 15];
       
        /// <summary>
        /// AI计算落子
        /// </summary>
        /// <param name="lazis"></param>
        /// <returns></returns>
        public Vector2Int AILazi(int [,] board)
        {
            
            return new MaxMin().Maxmin(board,1);
        }

     
    }
}
