using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//参考https://blog.csdn.net/u013390476/article/details/50011261
namespace LinHoweEightQueens
{
    /// <summary>
    /// CSP最小冲突法
    /// </summary>
    public class MinConflict
    {
        public int[] chessBoard = new int[8];
        private int[] columnConflict = new int[8];
        private int[] mainDiaConflict = new int[8*2-1];       // 主对角线方向的映射规则 (i, j) --> m_QueenNum-1-i + j
        private int[] counterDiaConflict = new int[8*2-1];    // 副对角线方向的映射规则 (i, j) --> m_QueenNum-1-i + m_QueenNum-1-j
        public MinConflict()
        {
            //Init
            for (int row = 0; row <8; ++row)
            {
                int column = UnityEngine.Random.Range(0,8); // 随机生成一个种群
                chessBoard[row] = column;
                PutQueen(row, column);
            }

            bool isOptimal = CheckSatus();

            while (!isOptimal)
            {
                for (int row = 0; row < 8; ++row)
                {
                    int minConflict = int.MaxValue;
                    int tmpConflict = int.MaxValue;
                    int minColumn = 0;

                    int curColumn = chessBoard[row];
                    RemoveQueen(row, curColumn);

                    for (int column = 0; column < 8; ++column)
                    {
                        tmpConflict = CalcuConflicts(row, column);
                        if (tmpConflict < minConflict)
                        {
                            minConflict = tmpConflict;  // 找到最小冲突
                            minColumn = column;         // 保存最小冲突时候的列数
                        }
                        else if (tmpConflict == minConflict && UnityEngine.Random.Range(0,2)>0)
                        {// 如果最小冲突值相等的话，有一半的可能移动，防止陷入局部最佳，而不是全局最佳
                            minColumn = column;
                        }
                    }
                    chessBoard[row] = minColumn;
                    PutQueen(row, minColumn);

                    isOptimal = CheckSatus();
                    if (isOptimal)
                        break;

                }
            }
        }

        private void PutQueen(int row,int column)
        {
            columnConflict[column]++;
            mainDiaConflict[8 - 1 - row + column]++;
            counterDiaConflict[2 * 8 - 2 - row - column]++;
        }

        private bool CheckSatus()
        {// 每一列只能有一个，每一个对角线方向不可能有两个。否则不可能是最优解
            for (int column = 0; column <8; ++column)
            {
                if (columnConflict[column] != 1)
                    return false;
            }
            for (int i = 0; i < 8; ++i)
            {
                if (mainDiaConflict[i] >= 2 || counterDiaConflict[i] >= 2)
                    return false;
            }
            return true;
        }

        private void RemoveQueen(int row, int column)
        {
            columnConflict[column]--;
            mainDiaConflict[8 - 1 - row + column]--;
            counterDiaConflict[2 * 8 - 2 - row - column]--;
        }

        private int CalcuConflicts(int row, int column)
        {
            return (columnConflict[column]
                + mainDiaConflict[8 - 1 - row + column]
                + counterDiaConflict[2 * 8 - 2 - row - column]);
        }
    }
}
