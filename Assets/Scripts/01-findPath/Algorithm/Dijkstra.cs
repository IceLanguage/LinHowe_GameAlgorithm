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
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="nodesMap"></param>
        /// <param name="NodeCount"></param>
        /// <returns></returns>
        public static Queue<Node> FindWay(
            Node start,
            Node end,
            Dictionary<Node, int> nodesMap)
        {
            //Init
            Init(nodesMap);

            //First Node
            canGetMinNodelist.Add(start);
            int curcount = 1;
            CostDict[start] = 0;
            RoadDict[start].Enqueue(start);
            Node? curnode = null;
            Node cur = start;
            Func<Node, double> GetMin = node => CostDict[node];
            //Find
            while (curcount < NodeCount && cur != end)
            {
                //查找访问过节点中cost最小的节点
                curnode = GetMinNodeFromArr(canGetMinNodelist, end, GetMin);

                //无法到达终点
                if (null == curnode)
                    break;

                //查找curnode下一个节点中距离终点最近的点
                cur = (Node)curnode;
                Node? minnode = GetMinNode(cur, end, GetMin);

                if (null == minnode)
                {
                    //当前节点无法到达终点
                    canGetMinNodelist.Remove(cur);
                    continue;
                }
                else
                {


                    Node _min = (Node)minnode;

                    //标记为已访问
                    visit[_min] = true;
                    curcount++;
                    canGetMinNodelist.Add(_min);

                    //更新路径
                    CostDict[_min] = CostDict[cur] + NodesMap[_min];
                    RoadDict[_min].Clear();
                    foreach (var e in RoadDict[cur])
                    {
                        RoadDict[_min].Enqueue(e);
                    }
                    RoadDict[_min].Enqueue(_min);
                }
            }

            return RoadDict[end];
        }
        

    }
}