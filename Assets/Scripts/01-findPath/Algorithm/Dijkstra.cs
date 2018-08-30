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
        /// Dijkstra
        /// 时间复杂度：O(n^2)
        /// 空间复杂度 O(n)
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
            Node cur = start;

            bool reachEnd = false;
            while(curcount < NodeCount && !reachEnd) 
            {
                int size = canGetMinNodelist.Count;
                for(int j = 0;j < size;++j)
                {
                    cur = canGetMinNodelist[0];
                    List<Node> nearbynodes = GetNearbyNode(cur, false);

                    visit[cur] = true;
                    curcount++;
                    canGetMinNodelist.Remove(cur);
                    for (int i = 0; i < nearbynodes.Count; ++i)
                    {
                        Node next = nearbynodes[i];
                        if (next == end&&!reachEnd)
                            reachEnd = true;
                        int newcost = CostDict[cur] + NodesMap[next];

                        //cost更小的情况
                        if (newcost < CostDict[next])
                        {
                            RoadDict[next].Clear();
                            CostDict[next] = newcost;

                            //更新路径
                            RoadDict[next] = new Queue<Node>(RoadDict[cur]);
                            RoadDict[next].Enqueue(next);

                            if (!canGetMinNodelist.Contains(next))
                                canGetMinNodelist.Add(next);
                        }

                    }
                }
            }

            return RoadDict[end];
        }
        

    }
}