using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LinHoweMazeGenerate
{

    /// <summary>
    /// 递归回溯
    /// </summary>
    public class RecursiveBacktracking
    {
        private static MazeWall mazeWall;

        //计数打通的区域
        private static int count = 0;

        //记录上一个区域
        private static Stack<WallArea> _queue = new Stack<WallArea>();
       
        public static MazeWall Generate(MazeWall wall)
        {
            mazeWall = wall;
            count = 0;
            _queue.Clear();

            //封闭全部墙壁
            mazeWall.ClosedAllWall();

            int maxCount = mazeWall.RowLength * mazeWall.ColLength;

            //随机选择一个开始区域
            WallArea fistArea = RandChooseBeginArea();
            _queue.Push(fistArea);

           WallArea? curArea = fistArea;
            while(count< maxCount)
            {
                if(null == curArea)
                {
                    //退回上一个区域
                    _queue.Pop();
                    curArea = _queue.Peek();
                    continue;
                }
                curArea = CheckNearby((WallArea)curArea);
            }

            //随机选择迷宫起点终点
            mazeWall.RandomOpenStartAndPoint();
            mazeWall.RandomOpenStartAndPoint();

            return mazeWall;
        }
        
        /// <summary>
        /// 随机选择一个开始区域
        /// </summary>
        private static WallArea RandChooseBeginArea()
        {
            int RandomRowIndex = Random.Range(0, mazeWall.RowLength);
            int RandomColIndex = Random.Range(0, mazeWall.ColLength);
            ++count;
            return new WallArea(RandomRowIndex, RandomColIndex);
        }

        /// <summary>
        /// 检测附近区域是否访问过
        /// </summary>
        private static WallArea? CheckNearby(WallArea area)
        {
            //获得未访问的邻接区域
            List<WallArea> nerabyAreas = new List<WallArea>();
            if (area.rowLength > 0)
                if (!mazeWall[area.rowLength - 1, area.colLength])
                    nerabyAreas.Add(new WallArea(area.rowLength - 1, area.colLength));
            if (area.rowLength < mazeWall.RowLength-1)
                if (!mazeWall[area.rowLength + 1, area.colLength])
                    nerabyAreas.Add(new WallArea(area.rowLength + 1, area.colLength));
            if (area.colLength > 0)
                if (!mazeWall[area.rowLength, area.colLength - 1])
                    nerabyAreas.Add(new WallArea(area.rowLength, area.colLength - 1));
            if (area.colLength < mazeWall.ColLength - 1)
                if (!mazeWall[area.rowLength, area.colLength + 1])
                    nerabyAreas.Add(new WallArea(area.rowLength, area.colLength + 1));

            if (0 == nerabyAreas.Count) return null;

            //打通的新区域
            ++count;
            WallArea newarea = nerabyAreas[Random.Range(0, nerabyAreas.Count - 1)];
            mazeWall.OpenArea(area, newarea);
            _queue.Push(newarea);

            return newarea;
        }
    }
}

