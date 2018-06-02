using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinHoweMazeGenerate
{
    public class Kruskal :GenerateMazeAlgoritnm
    {
        //全部墙
        private static List<KeyValuePair<WallArea, WallArea>> walls = 
            new List<KeyValuePair<WallArea, WallArea>>();
    
        public static MazeWall Generate(MazeWall wall)
        {
            mazeWall = wall;
            walls.Clear();

            //封闭全部墙壁
            mazeWall.ClosedAllWall();

            //初始化并查集
            UnionFindSet set = new UnionFindSet
                (mazeWall.RowLength * mazeWall.ColLength);

            Init();


            while (walls.Count > 0)
            {
                int randomIndex = Random.Range(0, walls.Count);
                var _wall = walls[randomIndex];



                if (set.Union(
                    GetUnionSetIndex(_wall.Key),
                    GetUnionSetIndex(_wall.Value)))
                {
                    mazeWall.OpenArea(_wall.Key, _wall.Value);
                }
                
                walls.RemoveAt(randomIndex);

            }
            


            //随机选择迷宫起点终点
            mazeWall.RandomOpenStartAndPoint();
            mazeWall.RandomOpenStartAndPoint();

            return mazeWall;
        }

        /// <summary>
        /// 计算并查集的索引
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        private static int GetUnionSetIndex(WallArea area)
        {
            return area.colLength * mazeWall.ColLength +
                area.rowLength;
        }

        private static void Init()
        {
            //获得所有墙
            int pre = 0;
            for (int i = 1; i < mazeWall.RowLength; ++i)
            {
                for(int j = 0; j < mazeWall.ColLength; ++j)
                {
                    walls.Add(new KeyValuePair<WallArea, WallArea>
                        (new WallArea(pre, j),
                         new WallArea(i, j)));
                    
                }
                pre = i;
            }
            
            for (int i = 0; i < mazeWall.RowLength ; ++i)
            {
                pre = 0;
                for (int j = 1; j < mazeWall.ColLength ; ++j)
                {
                    walls.Add(new KeyValuePair<WallArea, WallArea>
                        (new WallArea(i, pre),
                         new WallArea(i, j)));
                    pre = j;
                }
            }
        }
    }
}
