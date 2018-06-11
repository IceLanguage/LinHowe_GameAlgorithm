using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinHoweGameTree
{
    /// <summary>
    /// 棋子的位置索引
    /// </summary>
    public struct Lazi
    {
        public readonly int x, y;
        public Lazi(int x,int y)
        {
            this.x = x;
            this.y = y;
        }
        public static Lazi operator +(Lazi c1, Lazi c2)
        {
            return new Lazi(c1.x+c2.x,c1.y+c2.y);
        }
        public static Lazi operator * (int c1, Lazi c2)
        {
            return new Lazi(c1 * c2.x, c1 * c2.y);
        }

    }
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
        public Lazi AILazi(int [,] board)
        {
            
            return new MaxMin().Maxmin(board,2);
        }

     
    }
}
