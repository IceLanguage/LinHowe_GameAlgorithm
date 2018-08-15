using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinHoweFindPath
{
    public class FindPathAlgorithm
    {
        /// <summary>
        /// 已访问且四周存在未访问节点的列表
        /// </summary>
        protected static List<Node> canGetMinNodelist = new List<Node>();

        protected static int NodeCount;
        /// <summary>
        /// 节点字典
        /// </summary>
        protected static Dictionary<Node, int> NodesMap
            = new Dictionary<Node, int>();

        /// <summary>
        /// 到该节点的消耗
        /// </summary>
        protected static Dictionary<Node, int> CostDict =
            new Dictionary<Node, int>();

        /// <summary>
        /// 到达每个节点的路径
        /// </summary>
        protected static Dictionary<Node, Queue<Node>> RoadDict =
            new Dictionary<Node, Queue<Node>>();

        /// <summary>
        /// 该节点是否已经访问
        /// </summary>
        protected static Dictionary<Node, bool> visit =
            new Dictionary<Node, bool>();

        protected static void Init(Dictionary<Node, int> nodesMap)
        {
            NodesMap = nodesMap;
            NodeCount = NodesMap.Count;
            CostDict = new Dictionary<Node, int>(NodeCount);
            RoadDict = new Dictionary<Node, Queue<Node>>(NodeCount);
            canGetMinNodelist = new List<Node>(NodeCount);
            foreach(var e in NodesMap)
            {
                Node current = e.Key;
                CostDict[current] = int.MaxValue;
                visit[current] = false;
                RoadDict[current] = new Queue<Node>(NodeCount);
            }
            
        }

        /// <summary>
        /// 获取节点附近可访问的节点
        /// </summary>
        /// <param name="cur"></param>
        /// <param name="thinkAboutVisit"></param>
        /// <returns></returns>
        protected static List<Node> GetNearbyNode(Node cur, bool thinkAboutVisit = true)
        {
            List<Node> arr = new List<Node>();
            arr.Add(new Node(cur.x + 1, cur.z));
            arr.Add(new Node(cur.x - 1, cur.z));
            arr.Add(new Node(cur.x, cur.z + 1));
            arr.Add(new Node(cur.x, cur.z - 1));
            for(int i = 3;i>=0;--i)
            {
                Node node = arr[i];
                if(!NodesMap.ContainsKey(node))
                {
                    arr.RemoveAt(i);
                    continue;
                }

                if(thinkAboutVisit)
                {
                    if(visit[node])
                    {
                        arr.RemoveAt(i);
                        continue;
                    }
                }

                if(NodesMap[node] >= int.MaxValue)
                {
                    arr.RemoveAt(i);
                    continue;
                }
            }
            return arr;
        }

        /// <summary>
        /// 查找节点中最小的节点
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        protected static Node? GetMinNodeFromArr(
            List<Node> arr, Node end,Func<Node, int> getMinFunc)
        {
            if (0 == arr.Count) return null;
            if (1 == arr.Count) return arr[0];
            Node res = arr
                .OrderBy(getMinFunc)
                .First();
            return res;
        }
        //node => CostDict[node]
        /// <summary>
        /// 查找start的下一个节点，保证距离end最近
        /// </summary>
        /// <param name="node"></param>
        protected static Node? GetMinNode
        (
            Node start, Node end, Func<Node, int> getMinFunc,bool thinkAboutVisit =true
        )
        {
            List<Node> arr = GetNearbyNode(start);

            arr = arr
              .Where(node => NodesMap.ContainsKey(node)).ToList();

            return GetMinNodeFromVisit(arr, end, getMinFunc, thinkAboutVisit );

        }

        /// <summary>
        /// 查找未访问过节点中距离end最近的节点
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        protected static Node? GetMinNodeFromVisit(
            List<Node> arr, Node end, Func<Node, int> getMinFunc, bool thinkAboutVisit = true)
        {
            if(thinkAboutVisit)
                arr = arr
                  .Where(node => !visit[node]).ToList();

            return GetMinNodeFromArr(arr, end, getMinFunc);
        }

    }
}
