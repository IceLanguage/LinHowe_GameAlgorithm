using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LinHoweFindPath
{
    
    public class Astar:FindPathAlgorithm
    {
        //开启列表
        private static List<Node> open_List = new List<Node>();

        //关闭列表
        private static List<Node> close_List = new List<Node>();

        private static Node end;


        /// <summary>
        /// Astar算法
        /// 时间复杂度：O(n^2)
        /// 空间复杂度 O(n)
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="nodesMap"></param>
        /// <returns></returns>
        public static Queue<Node> FindWay
        (
            Node start,Node end,
            Dictionary<Node, int> nodesMap)
        {
            Astar.end = end;
            Init(nodesMap);

            //f(n) = g(n) + h(n)，其中f(n)是节点n从初始点到目标点的估价函数
            Func<Node, double> F = node => G(node) + H(node, end);

            //First Node
            CostDict[start] = 0;
            RoadDict[start].Enqueue(start);
            //在开启列表加入节点
            open_List.Add(start);
            
            Node? curnode = null;
            while (!(IsInOpenList(end))) //当终点存在开启列表中就意味着寻路结束了
            {
                //查找开启列表中cost最小的节点
                curnode = GetMinNodeFromArr(open_List, end, F);

                if(null == curnode)
                {
                    Debug.LogError("意外的null节点");
                    break;
                }
                else
                {
                    Node node = (Node)curnode;
                    open_List.Remove(node);

                    //在关闭列表中加入已访问的节点
                    close_List.Add(node);

                    //检查附近的节点
                    CheckNodeNearby(node);
                }
                
                
            }
            return RoadDict[end];
        }

        protected new static void Init(Dictionary<Node, int> nodesMap)
        {
            NodesMap = nodesMap;
            NodeCount = NodesMap.Count;
            CostDict = new Dictionary<Node, int>(NodeCount);
            RoadDict = new Dictionary<Node, Queue<Node>>(NodeCount);
            open_List = new List<Node>(NodeCount);
            close_List = new List<Node>(NodeCount);
            foreach (var e in NodesMap)
            {
                Node current = e.Key;
                CostDict[current] = int.MaxValue;
                RoadDict[current] = new Queue<Node>(NodeCount);
            }
        }
        /// <summary>
        /// 判断开启列表是否包含一个坐标的点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static bool IsInOpenList(Node node)
        {

            if (open_List.Contains(node)) return true;
            return false;
        }

        /// <summary>
        /// 判断关闭列表是否包含一个坐标的点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static bool IsInCloseList(Node node)
        {
            if (close_List.Contains(node)) return true;
            return false;
        }

        /// <summary>
        /// 检测节点附近的节点,将附近的节点加入OpenList
        /// </summary>
        private static void CheckNodeNearby(Node cur)
        {
            //查找附近的节点
            List<Node> nearby = GetNearbyNode(cur,false);

            for(int i = 0; i < nearby.Count ; ++i )
            {
                Node node = nearby[i];
                if (IsInCloseList(node)) continue;
                int cost = NodesMap[node];

                //更新路径
                if (cost + CostDict[cur] < CostDict[node])
                {
                    CostDict[node] = cost + CostDict[cur];
                    RoadDict[node] = new Queue<Node>(RoadDict[cur]);
                    RoadDict[node].Enqueue(node);
                }

                if (!IsInOpenList(cur))
                {

                    open_List.Add(node);

                }
               
            }
        }

        

   
    }
}
