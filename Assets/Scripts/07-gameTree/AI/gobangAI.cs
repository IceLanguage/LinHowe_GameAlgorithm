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
    }
    public class gobangAI
    {
        
        /// <summary>
        /// AI计算落子
        /// </summary>
        /// <param name="lazis"></param>
        /// <returns></returns>
        public static Lazi AILazi(int [,] lazis)
        {
            //获取可以下棋的位置
            List<Lazi> openList = new List<Lazi>();
            for (int i = 0; i < 15; ++i)
            {
                for (int j = 0; j < 15; ++j)
                {
                    if (0 == lazis[i, j])
                    {
                        openList.Add(new Lazi(i, j));
                    }
                }
            }


            return new Lazi(0, 0);
        }
    }
}
