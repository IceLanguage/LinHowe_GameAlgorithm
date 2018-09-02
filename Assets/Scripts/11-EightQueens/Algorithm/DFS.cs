using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LinHoweEightQueens
{
    /// <summary>
    /// 回溯递归解法
    /// </summary>
    class DFS : Solution
    {

        public DFS()
        {
            dfs(0);
        }

        private bool Check(int i, int v)
        {
            for (int k = 0; k < i; k++)
            {
                if (EightQueens[k] == v) return false;
                if (EightQueens[k] - v == i - k ||
                    EightQueens[k] - v == k - i)
                    return false;
            }
            return true;
        }
        private void dfs(int r = 0)
        {
            if (r >= 8)
            {
                ans++;
                List<int> arr = new List<int>();
                arr.AddRange(EightQueens);
                PossibleList.Add(arr);
                return;
            }
            for (int i = 0; i < 8; i++)
            {
                if (Check(r, i))
                {
                    EightQueens[r] = i;
                    dfs(r + 1);
                    EightQueens[r] = 0;
                }
            }
        }
    }
}
