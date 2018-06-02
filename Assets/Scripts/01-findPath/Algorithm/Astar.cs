using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace LinHoweFindPath
{
    
    public class Astar:FindPathAlgorithm
    {
        //开启列表
        private static List<Node> open_List = new List<Node>();

        //关闭列表
        private static List<Node> close_List = new List<Node>();

        public static Queue<Node> FindWay
        (
            Node start,Node end,
            Dictionary<Node, int> nodesMap)
        {
            Init(nodesMap);


            //First Node
            CostDict[start] = 0;
            RoadDict[start].Enqueue(start);
            //在开启列表加入节点
            open_List.Add(start);
            
            Node? curnode = null;
            Func<Node, int> GetMin = node => CostDict[node];
            while (!(IsInOpenList(end))) //当终点存在开启列表中就意味着寻路结束了
            {
                //查找开启列表中cost最小的节点
                curnode = GetMinNodeFromArr(open_List, end, GetMin);

                if(null == curnode)
                {
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
            CostDict.Clear();
            RoadDict.Clear();
            open_List.Clear();
            close_List.Clear();
            NodesMap = nodesMap;
            NodeCount = NodesMap.Count;

            var enumerator = NodesMap.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    var current = enumerator.Current.Key;
                    CostDict[current] = int.MaxValue;
                    RoadDict[current] = new Queue<Node>();
                }
            }
            finally
            {
                enumerator.Dispose();
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
        /// 检测节点附近的节点
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
                if (IsInOpenList(cur))
                {
                    

                    //更新路径
                    if (cost+CostDict[cur] < CostDict[node])
                    {
                        CostDict[node] = cost + CostDict[cur];
                        RoadDict[node].Clear();
                        foreach (var e in RoadDict[cur])
                        {
                            RoadDict[node].Enqueue(e);
                        }
                        RoadDict[node].Enqueue(node);
                    }
                }
                else
                {
                    //更新路径
                    CostDict[node] = cost + CostDict[cur];
                    RoadDict[node].Clear();
                    foreach (var e in RoadDict[cur])
                    {
                        RoadDict[node].Enqueue(e);
                    }
                    RoadDict[node].Enqueue(node);
                    open_List.Add(node);
                }
            }
        }

        /// <summary>
        /// 不使用closelist ,算法思想和FindWay没有本质不同
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="nodesMap"></param>
        /// <param name="NodeCount"></param>
        /// <returns></returns>
        public static Queue<Node> FindWay2(
            Node start,
            Node end,
            Dictionary<Node, int> nodesMap)
        {
            //Init
            FindPathAlgorithm.Init(nodesMap);

            //First Node
            canGetMinNodelist.Add(start);
            int curcount = 1;
            CostDict[start] = 0;
            RoadDict[start].Enqueue(start);
            Node? curnode = null;
            Node cur = start;
            Func<Node, int> GetMin = node => CostDict[node];
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

        /// <summary>
        /// 以距离为导向的寻路
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="nodesMap"></param>
        /// <returns></returns>
        public static Queue<Node> FindWayGreedy(
          Node start,
          Node end,
          Dictionary<Node, int> nodesMap
          )
        {
            //Init
           
            FindPathAlgorithm.Init(nodesMap);

            //First Node
            CostDict[start] = 0;
            RoadDict[start].Enqueue(start);

            Func<Node, int> GetMin = node => (int)(Math.Pow(node.x - end.x, 2) + Math.Pow(node.z - end.z, 2));

            canGetMinNodelist.Add(start);
            Node? curnode = null;
            Node cur = start;
            //Find
            while (cur != end)
            {
                //查找访问过节点中距离终点最近的节点
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
