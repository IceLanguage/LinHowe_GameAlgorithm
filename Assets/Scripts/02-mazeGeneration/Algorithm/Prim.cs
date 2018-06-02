using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinHoweMazeGenerate
{
    public class Prim:GenerateMazeAlgoritnm
    {
        //维护的墙列表
        private static List<KeyValuePair<WallArea, WallArea>> walls = new List<KeyValuePair<WallArea, WallArea>>();

        public static MazeWall Generate(MazeWall wall)
        {
            walls.Clear();

            //封闭全部墙壁
            mazeWall.ClosedAllWall();

            //随机选择一个开始区域
            WallArea fistArea = RandChooseBeginArea();
            AddNerabyWall(fistArea);

            while (walls.Count > 0)
            {
                int randomIndex = Random.Range(0, walls.Count);
                var _wall = walls[randomIndex];
                if(checkWall(_wall))
                {
                    
                    if(checkArea(_wall.Key))
                    {
                        AddNerabyWall(_wall.Key);
                    }
                    if (checkArea(_wall.Key))
                    {
                        AddNerabyWall(_wall.Value);
                    }
                    mazeWall.OpenArea(_wall.Key, _wall.Value);
                }
                else
                {
                    walls.RemoveAt(randomIndex);
                }
            }

            //随机选择迷宫起点终点
            mazeWall.RandomOpenStartAndPoint();
            mazeWall.RandomOpenStartAndPoint();

            return mazeWall;
        }

        /// <summary>
        /// 把区域附近未打通的墙加入维护的墙列表
        /// </summary>
        /// <param name="area"></param>
        private static void AddNerabyWall(WallArea area)
        {
            List<WallArea> areas = GetNearbyArea(area);
            for (int i = 0; i < areas.Count; ++i)
                walls.Add(new KeyValuePair<WallArea, WallArea>(
                    area, areas[i]
                    ));
        }

        

        

       
    }

    
}
