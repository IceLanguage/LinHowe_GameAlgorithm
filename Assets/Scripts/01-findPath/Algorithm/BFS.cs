using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinHoweFindPath
{
    public class BFS : FindPathAlgorithm
    {
        private static Dictionary<Node, int> reachCostLeave = 
            new Dictionary<Node, int>();
        /// <summary>
        /// 战棋游戏人物可到达位置计算
        /// </summary>
        /// <param name="center"></param>
        /// <param name="cost"></param>
        /// <returns></returns>
        public static List<Node> GetReachList(
            Node center, int cost, Dictionary<Node, int> nodesMap)
        {
            //Init
            Init(nodesMap);

            //First Node
            canGetMinNodelist.Add(center);
            reachCostLeave[center] = cost;

            //Find
            while (canGetMinNodelist.Count > 0)
            {
                //查找访问过节点中距离终点最近的点
                Node cur = canGetMinNodelist.First();

                if(reachCostLeave[cur]<0)
                {
                    canGetMinNodelist.Remove(cur);
                    continue;
                }

                //查找cur所能到达的节点列表
                List<Node> arr = GetNearbyNode(cur, false);

                //遍历列表里的所有的节点
                for (int i = 0; i < arr.Count; i++)
                {

                    Node n = arr[i];

                    //设置为已访问
                    if (reachCostLeave[n]<0)
                    {
                        int newleave = reachCostLeave[cur] - nodesMap[n];
                        reachCostLeave[n] = newleave;
                        if(newleave>=0)
                            canGetMinNodelist.Add(n);
                    }

                }
                canGetMinNodelist.Remove(cur);
            }

            //获取结果
            List<Node> res = new List<Node>();

            var enumerator = reachCostLeave.GetEnumerator();
            try
            {
                while(enumerator.MoveNext())
                {
                    if (enumerator.Current.Value >= 0)
                        res.Add(enumerator.Current.Key);
                }
            }
            finally
            {
                enumerator.Dispose();
            }

            return res;
        }

       
        protected new static void Init(Dictionary<Node, int> nodesMap)
        {
            CostDict.Clear();
            NodesMap = nodesMap;
            NodeCount = NodesMap.Count;
            reachCostLeave = new Dictionary<Node, int>();
            canGetMinNodelist = new List<Node>();

            var enumerator = NodesMap.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    var current = enumerator.Current.Key;
                    reachCostLeave[current] = -1;
                }
            }
            finally
            {
                enumerator.Dispose();
            }
        }
    }
}
