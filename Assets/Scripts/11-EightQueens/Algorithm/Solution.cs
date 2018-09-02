using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinHoweEightQueens
{
    class Solution
    {
        /// <summary>
        /// 数组的第i个数据代表第i行皇后所在的列数
        /// </summary>
        protected int[] EightQueens = new int[8];
        protected int ans = 0;
        protected List<List<int>> possibleList = new List<List<int>>();
        public List<List<int>> PossibleList
        {
            get
            {
                return possibleList;
            }
        }

        public int Ans
        {
            get
            {
                return ans;
            }
        }
    }
}
