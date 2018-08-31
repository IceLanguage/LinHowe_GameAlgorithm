using System.Collections.Generic;
using UnityEngine;

namespace LinHoweMazeGenerate
{
    /// <summary>
    /// 递归分割
    /// </summary>
    public class RecursiveSegmentation
    {
        private static MazeWall mazeWall;
        
        public static MazeWall Generate(MazeWall wall)
        {
            mazeWall = wall;

            //封闭全部墙壁
            mazeWall.ClosedAllWall();

            //递归分割
            RecursiveDivision(0,mazeWall.RowLength ,0,mazeWall.ColLength );

            //随机选择迷宫起点终点
            mazeWall.RandomOpenStartAndPoint();
            mazeWall.RandomOpenStartAndPoint();

            return mazeWall;
        }

        /// <summary>
        /// 分割
        /// </summary>
        private static void RecursiveDivision(
            int rowstart,
            int rowend,
            int colstart,
            int colend)
        {
            if (rowend < rowstart) return;
            if (colend < colstart) return;

            //条件不允许再分成四块了
            if(1 >= rowend - rowstart)
            {
                
                WallArea preArea = new WallArea(rowstart, colstart);
                for(int i = colstart + 1 ;i < colend; ++i )
                {
                    WallArea curArea = new WallArea(rowstart, i);
                    mazeWall.OpenArea(preArea, curArea);
                    preArea = curArea;
                }
                return;
            }
            if(1 >= colend - colstart)
            {
                WallArea preArea = new WallArea(rowstart, colstart);
                for (int i = rowstart + 1; i < rowend; ++i)
                {
                    WallArea curArea = new WallArea(i, colstart);
                    mazeWall.OpenArea(preArea, curArea);
                    preArea = curArea;
                }
                return;
            }

            //分块
            int randomRow = UnityEngine.Random.Range(rowstart + 1, rowend);
            int randomCol = UnityEngine.Random.Range(colstart + 1, colend);

            
            //随机找到四个分界线上四个洞
            List<KeyValuePair<WallArea,WallArea>> FourHoles = new List<KeyValuePair<WallArea, WallArea>>();
            int index = Random.Range(colstart, randomCol);
            FourHoles.Add(new KeyValuePair<WallArea, WallArea>(
                new WallArea(randomRow - 1,index),
                new WallArea(randomRow, index)
                ));
            index = Random.Range(randomCol, colend);
            FourHoles.Add(new KeyValuePair<WallArea, WallArea>(
                new WallArea(randomRow - 1, index),
                new WallArea(randomRow, index)
                ));
            index = Random.Range(rowstart, randomRow);
            FourHoles.Add(new KeyValuePair<WallArea, WallArea>(
                new WallArea(index, randomCol - 1),
                new WallArea(index, randomCol)
                ));
            index = Random.Range(randomRow, rowend);
            FourHoles.Add(new KeyValuePair<WallArea, WallArea>(
                new WallArea(index, randomCol - 1),
                new WallArea(index, randomCol)
                ));

            //打通其中三个洞
            index = Random.Range(0, 4);
            FourHoles.RemoveAt(index);
            for(int i= 0;i<3;++i)
            {
                mazeWall.OpenArea(FourHoles[i].Key, FourHoles[i].Value);
            }


            //递归分割
            RecursiveDivision(rowstart, randomRow, colstart, randomCol);
            RecursiveDivision(randomRow, rowend, colstart, randomCol);
            RecursiveDivision(rowstart, randomRow, randomCol, colend);
            RecursiveDivision(randomRow, rowend, randomCol, colend);
        }
    }
}
