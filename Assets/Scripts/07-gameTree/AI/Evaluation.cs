using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LinHoweGameTree
{
    /// <summary>
    /// 棋型
    /// </summary>
    public enum ChessType
    {
        None,
        连五,
        活四,
        冲四,
        活三,
        眠三,
        活二,
        眠二,
        活一,
        眠一
    }
    /// <summary>
    /// 评估类
    /// </summary>
    public class Evaluation
    {
        //检查越界
        Func<int, int, bool> OutOfRange =
          (x, y) =>
          x >= 0 && x < 15 && y >= 0 && y < 15;

        //四个方向计数 水平、垂直、左斜、右斜 
        public readonly static Lazi[][] direction = new[]
        {
            new[]{ new Lazi(-1, 0), new Lazi(1, 0) },
            new[]{ new Lazi(0 ,-1), new Lazi(0, 1) },
            new[]{ new Lazi(-1, 1), new Lazi(1,-1) },
            new[]{ new Lazi(-1,-1), new Lazi(1, 1) }
        };


        //棋盘信息
        private int[,] _board;

        //是否胜利
        public bool win = false;
        public bool lose = false;

        public int scoreSum;

        public int Evaluate(int[,] board)
        {
            //Init
            _board = board;
            win = false;
            lose = false;
            scoreSum = 0;
           
            List<List<int>> graph = new List<List<int>>();
            
            for (int i =0;i<15;++i)
            {
                List<int> line = new List<int>();
                for (int j =0;j<15;++j)
                {
                    
                    line.Add(_board[0 + direction[0][1].x * j, i + direction[0][1].y * j]);
                }
                graph.Add(line);
            }
            for (int i = 0; i < 15; ++i)
            {
                List<int> line = new List<int>();
                for (int j = 0; j < 15; ++j)
                {

                    line.Add(_board[i + direction[1][1].x * j, 0 + direction[1][1].y * j]);
                }
                graph.Add(line);
            }
            for (int i = 0; i < 15; ++i)
            {
                List<int> line = new List<int>();
                for (int j = 0; j < 15; ++j)
                {
                    if (!OutOfRange(i + direction[2][1].x * j, 14 + direction[2][1].y * j)) break;
                    line.Add(_board[i + direction[2][1].x * j, 14 + direction[2][1].y * j]);
                }
                graph.Add(line);
                line = new List<int>();
                for (int j = 0; j < 15; ++j)
                {
                    if (!OutOfRange(0 + direction[2][1].x * j, i + direction[2][1].y * j)) break;
                    line.Add(_board[0 + direction[2][1].x * j, i + direction[2][1].y * j]);
                }
                graph.Add(line);

            }
            for (int i = 0; i < 15; ++i)
            {
                List<int> line = new List<int>();
                for (int j = 0; j < 15; ++j)
                {
                    if (!OutOfRange(i + direction[3][1].x * j, 0 + direction[3][1].y * j)) break;
                    line.Add(_board[i + direction[3][1].x * j, 0 + direction[3][1].y * j]);
                }
                graph.Add(line);
                line = new List<int>();
                for (int j = 0; j < 15; ++j)
                {
                    if (!OutOfRange(0 + direction[3][1].x * j, i + direction[3][1].y * j)) break;
                    line.Add(_board[0 + direction[3][1].x * j, i + direction[3][1].y * j]);
                }
                graph.Add(line);

            }
            foreach(var line in graph)
            {
                for (int i = 0; i < line.Count; i++)
                {
                    int count = 0; // 连子数
                    int block = 0; // 封闭数
                    int value = 0;  //分数

                    if (line[i] == -1)
                    { // 发现第一个己方棋子
                        count = 1;
                        block = 0;
                        if (i == 0) block = 1;
                        else if (line[i - 1] != 0) block = 1;
                        for (i = i + 1; i < line.Count; i++)
                        {
                            if (line[i] == -1) count++;
                            else break;
                        }
                        if (i == line.Count || line[i] != 0) block++;
                        value += score(count, block);
                    }
                    scoreSum += value;
                }
            }
            foreach (var line in graph)
            {
                for (int i = 0; i < line.Count; i++)
                {
                    int count = 0; // 连子数
                    int block = 0; // 封闭数
                    int value = 0;  //分数

                    if (line[i] == 1)
                    { // 发现第一个己方棋子
                        count = 1;
                        block = 0;
                        if (i == 0) block = 1;
                        else if (line[i - 1] != 0) block = 1;
                        for (i = i + 1; i < line.Count; i++)
                        {
                            if (line[i] == 1) count++;
                            else break;
                        }
                        if (i == line.Count || line[i] != 0) block++;
                        value += score(count, block);
                    }
                    scoreSum -= value;
                }
            }
            return scoreSum;
        }

        private int score(int count,int block) {

            if(count >= 5) return 10000000;

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
