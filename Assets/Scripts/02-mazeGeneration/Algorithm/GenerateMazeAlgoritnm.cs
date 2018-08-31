using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinHoweMazeGenerate
{
    public class GenerateMazeAlgoritnm
    {
        protected static MazeWall mazeWall;

        /// <summary>
        /// 随机选择一个开始区域
        /// </summary>
        protected static WallArea RandChooseBeginArea()
        {
            int RandomRowIndex = Random.Range(0, mazeWall.RowLength);
            int RandomColIndex = Random.Range(0, mazeWall.ColLength);
            
            return new WallArea(RandomRowIndex, RandomColIndex);
        }
        protected static List<WallArea> GetNearbyArea(WallArea area)
        {
            //获得未访问的邻接区域
            List<WallArea> nerabyAreas = new List<WallArea>();
            
            if (area.rowLength > 0)
                if (!mazeWall[area.rowLength - 1, area.colLength])
                    nerabyAreas.Add(new WallArea(area.rowLength - 1, area.colLength));
            if (area.rowLength < mazeWall.RowLength - 1)
                if (!mazeWall[area.rowLength + 1, area.colLength])
                    nerabyAreas.Add(new WallArea(area.rowLength + 1, area.colLength));
            if (area.colLength > 0)
                if (!mazeWall[area.rowLength, area.colLength - 1])
                    nerabyAreas.Add(new WallArea(area.rowLength, area.colLength - 1));
            if (area.colLength < mazeWall.ColLength - 1)
                if (!mazeWall[area.rowLength, area.colLength + 1])
                    nerabyAreas.Add(new WallArea(area.rowLength, area.colLength + 1));

            return nerabyAreas;
        }

        /// <summary>
        /// 判断区域是否被打通
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        protected static bool checkArea(WallArea area)
        {
            return mazeWall[area.rowLength, area.colLength];
        }

        /// <summary>
        /// 检测墙是否需要打通
        /// </summary>
        /// <param name="wall"></param>
        /// <returns></returns>
        protected static bool checkWall(KeyValuePair<WallArea, WallArea> wall)
        {
            bool Conduction1 = checkArea(wall.Key);
            bool Conduction2 = checkArea(wall.Value);

            return !Conduction1 || !Conduction2;
        }

    }
}
