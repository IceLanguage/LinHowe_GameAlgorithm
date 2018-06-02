using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LinHoweMazeGenerate
{
    /// <summary>
    /// 迷宫的墙壁
    /// </summary>
    public class MazeWall
    {
        #region 变量
        //true为存在墙壁
        //行
        public bool[,] row;

        //列
        public bool[,] col;

        //行列数
        private readonly WallArea wallarea;

        #endregion
        #region 属性
        //区域是否打通,true为打通
        public bool this[int rowindex,int colindex]
        {
            get
            {
                //检查是否越界
                if (rowindex >= RowLength || colindex >= ColLength)
                    Debug.LogError("越界");
                if (rowindex < 0 || colindex < 0)
                    Debug.LogError("越界");

                //有一面墙不存在就是打通
                return !(row[rowindex, colindex]&&row[rowindex, colindex + 1]&&
                       col[colindex, rowindex]&&col[colindex, rowindex + 1]);
            }
        }
        
        public WallArea WallAreaLenght
        {
            get
            {
                return wallarea;
            }
        }
        public int RowLength
        {
            get
            {
                return WallAreaLenght.rowLength;
            }
        }
        public int ColLength
        {
            get
            {
                return WallAreaLenght.colLength;
            }
        }
        #endregion
        #region 构造函数
        public MazeWall(int rowlength,int collength)
        {
            row = new bool[rowlength, collength + 1];
            col = new bool[collength, rowlength + 1];
            wallarea = new WallArea(rowlength, collength);
            ClosedAllWall();
        }
        public MazeWall(int size)
        {
            row = new bool[size, size + 1];
            col = new bool[size, size + 1];
            wallarea = new WallArea(size, size);
            ClosedAllWall();
        }
        #endregion
        /// <summary>
        /// 封闭全部墙壁
        /// </summary>
        public void ClosedAllWall()
        {
            for (int i = 0; i < RowLength; i++)
            {
                for (int j = 0; j < ColLength+1; j++)
                {
                    row[i, j] = true;                 
                }
            }
            for (int i = 0; i < ColLength; i++)
            {
                for (int j = 0; j < RowLength+1; j++)
                {
                    col[i, j] = true;
                }
            }
        }

        /// <summary>
        /// 打通区域
        /// </summary>
        public void OpenArea(WallArea area1, WallArea area2)
        {
            if(area1.rowLength == area2.rowLength)
            {
                row[area1.rowLength, Mathf.Max(area1.colLength, area2.colLength)] = false;
                return;
            }

            if (area1.colLength == area2.colLength)
            {
                col[area1.colLength, Mathf.Max(area1.rowLength, area2.rowLength)] = false;
            }
        }

        /// <summary>
        /// 打通起点终点的边界墙，起点终点随机
        /// </summary>
        public void RandomOpenStartAndPoint()
        {
            if(Random.Range(0,2)>1)
                if(Random.Range(0,2)<1)
                {
                    int index = Random.Range(0, RowLength - 1);
                    while(!row[index,0])
                        index = Random.Range(0, RowLength - 1);
                    row[index, 0] = false;
                }
                else
                {
                    int index = Random.Range(0, RowLength - 1);
                    while (!row[index, RowLength])
                        index = Random.Range(0, RowLength - 1);
                    row[index, ColLength] = false;
                }
            else
                if (Random.Range(0, 2) < 1)
                {
                    int index = Random.Range(0, ColLength - 1);
                    while (!row[index, 0])
                        index = Random.Range(0, ColLength - 1);
                    col[index, 0] = false;
                }
                else
                {
                    int index = Random.Range(0, ColLength - 1);
                    while (!row[index, RowLength])
                        index = Random.Range(0, ColLength - 1);
                    col[index, RowLength] = false;
                }
        }
    }

    /// <summary>
    /// 用来表示行列数或迷宫区域
    /// </summary>
    public struct WallArea
    {
        public int rowLength;
        public int colLength;
        public WallArea(int row,int col)
        {
            rowLength = row;
            colLength = col;
        }
    }

}

