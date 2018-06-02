using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LinHoweFindPath
{
    public class Dijkstra: FindPathAlgorithm
    {
        

        /// <summary>
        /// 获得保证最小消耗的路径(需要访问所有节点)BFS
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="nodesMap"></param>
        /// <returns></returns>
        public static Queue<Node> FindWay(
            Node start,
            Node end,
            Dictionary<Node, int> nodesMap
            )
        {
            //Init
            Init(nodesMap);

            //First Node
            canGetMinNodelist.Add(start);
            int curcount = 1;
            CostDict[start] = 0;
            RoadDict[start].Enqueue(start);

            //Find
            while (curcount < NodeCount && canGetMinNodelist.Count > 0)
            {
                //查找访问过节点中距离终点最近的点
                Node cur = canGetMinNodelist.First();

                //查找cur所能到达的节点列表
                List<Node> arr = GetNearbyNode(cur, false);

                //遍历列表里的所有的节点
                for (int i = 0; i < arr.Count; i++)
                {

                    Node n = arr[i];

                    //设置为已访问
                    if (!visit[n])
                    {

                        canGetMinNodelist.Add(n);
                        visit[n] = true;
                        curcount++;
                    }


                    //如果新的路径更短，更新路径
                    if (CostDict[n] > CostDict[cur] + NodesMap[n])
                    {
                        CostDict[n] = CostDict[cur] + NodesMap[n];

                        RoadDict[n].Clear();
                        foreach (var e in RoadDict[cur])
                        {
                            RoadDict[n].Enqueue(e);
                        }
                        RoadDict[n].Enqueue(n);

                    }

                }
                canGetMinNodelist.Remove(cur);


            }

            return RoadDict[end];
        }

    }
}