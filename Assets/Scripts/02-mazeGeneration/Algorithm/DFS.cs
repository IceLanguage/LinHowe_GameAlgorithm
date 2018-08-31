using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LinHoweMazeGenerate
{

    /// <summary>
    /// 递归回溯
    /// </summary>
    public class DFS:GenerateMazeAlgoritnm
    {

        //计数打通的区域
        private static int count = 0;

        //记录上一个区域
        private static Stack<WallArea> _stack= new Stack<WallArea>();

        public static MazeWall Generate(MazeWall wall)
        {
            mazeWall = wall;
            count = 0;
            _stack.Clear();

            //封闭全部墙壁
            mazeWall.ClosedAllWall();

            int maxCount = mazeWall.RowLength * mazeWall.ColLength;

            //随机选择一个开始区域
            WallArea fistArea = RandChooseBeginArea();
            ++count;
            _stack.Push(fistArea);

            WallArea? curArea = fistArea;
            while (count < maxCount)
            {
                if (null == curArea)
                {
                    //退回上一个区域
                    _stack.Pop();
                    curArea = _stack.Peek();
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
        /// 检测附近区域是否访问过,并随机选择其中一个附件区域打通
        /// </summary>
        private static WallArea? CheckNearby(WallArea area)
        {
            //获得未访问的邻接区域
            List<WallArea> nerabyAreas = GetNearbyArea(area);

            if (0 == nerabyAreas.Count) return null;

            //打通的新区域
            ++count;
            WallArea newarea = nerabyAreas[Random.Range(0, nerabyAreas.Count - 1)];
            mazeWall.OpenArea(area, newarea);
            _stack.Push(newarea);

            return newarea;
        }
    }
}

