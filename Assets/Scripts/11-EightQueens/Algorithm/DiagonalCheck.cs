using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinHoweEightQueens
{
    /// <summary>
    /// 对角线检查
    /// </summary>
    public class DiagonalCheck
    {
        private int[] EightQueens = new int[8];

        //记录列，左对角线，右对角线信息
        private bool[] b, c, d;
        private int ans = 0;
        private List<List<int>> possibleList = new List<List<int>>();
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
        public DiagonalCheck()
        {
            b = new bool[20];
            c = new bool[20];
            d = new bool[20];
            sou(0);
        }
        
        private void sou(int x)
        {
            if (x >= 8)
            {
                ans++;
                List<int> arr = new List<int>();
                arr.AddRange(EightQueens);
                PossibleList.Add(arr);
                return;
            }
            for (int i = 0; i < 8; i++) if (!(b[i] || c[x + i] || d[x - i + 8]))
                {
                    b[i] = c[x + i] = d[x - i + 8] = true;
                    EightQueens[x] = i;
                    sou(x + 1);
                    b[i] = c[x + i] = d[x - i + 8] = false;
                }
        }
    }
}
