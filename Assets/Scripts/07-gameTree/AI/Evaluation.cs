using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LinHoweGameTree
{
    /// <summary>
    /// 评估类
    /// </summary>
    public class Evaluation
    {
        //检查越界
        Func<int, int, bool> outOfRange =
          (x, y) =>
          x < 0 || x >= 15 || y < 0 || y >= 15;

        //四个方向计数 水平、垂直、左斜、右斜 
        public readonly static Vector2Int[][] direction = new[]
        {
            new[]{ new Vector2Int(-1, 0), new Vector2Int(1, 0) },
            new[]{ new Vector2Int(0 ,-1), new Vector2Int(0, 1) },
            new[]{ new Vector2Int(-1, 1), new Vector2Int(1,-1) },
            new[]{ new Vector2Int(-1,-1), new Vector2Int(1, 1) }
        };

        //棋盘信息
        private int[,] _board;

        //AI是否胜利
        public bool win = false;
        public bool lose = false;

        public int scoreSum;
        private List<List<int>> allLine;
        public int Evaluate(int[,] board)
        {
            //Init
            _board = board;
            win = false;
            lose = false;
            scoreSum = 0;

            allLine = ReadLine();

            int aiscore = GetScore(-1);
            int playscore = GetScore(1);
            scoreSum = aiscore - playscore;
            return scoreSum;
        }
        /// <summary>
        /// 读取所有可能形成5字的行
        /// </summary>
        /// <returns></returns>
        private List<List<int>> ReadLine()
        {
            allLine = new List<List<int>>();
            var ReadStep = new[]{
                new { start = new Vector2Int(0, -1),step = new Vector2Int(0,1),len=15},
                new { start = new Vector2Int(-1, 0),step = new Vector2Int(1,1),len=15 },
                new { start = new Vector2Int(-1, 14),step = new Vector2Int(2,1),len=15},
                new { start = new Vector2Int(0, -1),step = new Vector2Int(2,1),len=14 },
                new { start = new Vector2Int(0, -1),step = new Vector2Int(3,1),len=15 },
                new { start = new Vector2Int(14, -1),step = new Vector2Int(3,0),len=14 }
            };

            for (int t = 0; t < ReadStep.Length; ++t)
            {
                var read = ReadStep[t];
                var step = direction[read.step.x][read.step.y];
                for (int i = 0; i < 15; ++i)
                {
                    var start = new Vector2Int(
                    -1 == read.start.x ? i : read.start.x,
                    -1 == read.start.y ? i : read.start.y);
                    List<int> line = new List<int>();
                    for (int j = 0; j < read.len; ++j)
                    {
                        Vector2Int lazi = start + step * j;
                        if (outOfRange(lazi.x, lazi.y)) break;
                        line.Add(
                            _board[lazi.x, lazi.y]);
                    }
                    allLine.Add(line);

                }
            }
            return allLine;
        }
        /// <summary>
        /// 计算ai或玩家的总分值
        /// </summary>
        /// <param name="play"></param>
        /// <returns></returns>
        private int GetScore(int play)
        {
            int sum = 0;
            foreach (var line in allLine)
            {
                for (int i = 0; i < line.Count; i++)
                {
                    int count = 0; // 连子数
                    int block = 0; // 封闭数
                    int value = 0;  //分数

                    if (play == line[i])
                    { // 发现第一个己方棋子
                        count = 1;
                        block = 0;
                        if (i == 0) block = 1;
                        else if (line[i - 1] != 0) block = 1;
                        for (i = i + 1; i < line.Count; i++)
                        {
                            if (play == line[i] ) count++;
                            else break;
                        }
                        if (i == line.Count || line[i] != 0) block++;
                        value += score(count, block,play);
                    }
                    sum += value;
                }
                
            }
            return sum;
        }
        /// <summary>
        /// 根据该行棋型评估分值
        /// </summary>
        /// <param name="count"></param>
        /// <param name="block"></param>
        /// <returns></returns>
        private int score(int count,int block,int play) {

            if (count >= 5)
            {
                if (-1 == play) win = true;
                else if (1 == play) lose = true;
                return 10000000;
            }
            if(block == 0) {
                switch(count) {
                    case 1: return 10;
                    case 2: return 100;
                    case 3: return 1000;
                    case 4: return 100000;
                }
            }

            if(block == 1) {
                switch(count) {
                    case 1: return 1;
                    case 2: return 10;
                    case 3: return 100;
                    case 4: return 10000;
                }
            }

            return 0;
        }


        
    }
}
