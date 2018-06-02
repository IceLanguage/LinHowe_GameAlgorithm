using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace LinHoweFindPath
{
    /// <summary>
    /// 格子管理类
    /// </summary>
    public class GridMapManager : UnityComponentSingleton<GridMapManager>
    {
        public Node start;
        public Node end;
        public int reachCost;
        public Dictionary<Node, GridMap> GridMapDict = new Dictionary<Node, GridMap>(); 
        #region View
        private Texture blueTexture;
        private List<Node> road;
        public Texture BlueItem
        {
            get
            {
                if(null == blueTexture)
                {
                    blueTexture = Resources.Load<Texture>("01-findPath/collectable_blue");
                }
                return blueTexture;
            }
        }
        private Texture redTexture;
        public Texture RedItem
        {
            get
            {
                if (null == redTexture)
                {
                    redTexture = Resources.Load<Texture>("01-findPath/collectable_red");
                }
                return redTexture;
            }
        }
        private Texture yellowTexture;
        public Texture YellowItem
        {
            get
            {
                if (null == yellowTexture)
                {
                    yellowTexture = Resources.Load<Texture>("01-findPath/collectable_yellow");
                }
                return yellowTexture;
            }
        }
        private Texture purpleTexture;
        public Texture PurpleItem
        {
            get
            {
                if (null == purpleTexture)
                {
                    purpleTexture = Resources.Load<Texture>("01-findPath/collectable_purple");
                }
                return purpleTexture;
            }
        }
        private Texture greenTexture;
        public Texture GreenItem
        {
            get
            {
                if (null == greenTexture)
                {
                    greenTexture = Resources.Load<Texture>("01-findPath/collectable_green");
                }
                return greenTexture;
            }
        }
        private void ShowRoad(Queue<Node> road)
        {
            
            ShowRoad(road.ToList());
        }
        private void ShowRoad(List<Node> road)
        {
            CleanRoad();
            this.road = road;
            if (null != road)
                foreach (Node e in road)
                    if (start != e && end != e)
                        GridMapDict[e].Show(GreenItem);
            GridMapDict[start].Show(YellowItem);
            GridMapDict[end].Show(PurpleItem);
        }

        private void OnDrawGizmos()
        {
            Node pre = start;
            Node cur = start;
            if(null!=road)
            {
                foreach(var e in road)
                {
                    pre = cur;
                    cur = e;
                    if(null!=pre&&null!=cur)
                        Debug.DrawLine(
                            GridMapDict[pre].transform.position,
                            GridMapDict[cur].transform.position,
                            Color.red);
                }
            }
        }
        #endregion
        #region 编辑器扩展
        public int size = 10;
        public GameObject prefab;

        [ContextMenu("生成格子")]
        public void BuildMap()
        {
            for (int i = -size/2; i <= size/2; ++i)
            {
                for (int j = -size / 2; j <= size / 2; ++j)
                {
                    GameObject go = Instantiate(prefab, transform);
                    GridMap gm = go.GetComponent<GridMap>();
                    gm.SetGridGameObject(i, 0, j);
                }
                
            }

        }
        [ContextMenu("清空路径")]
        public void CleanRoad()
        {
            if (null != this.road)
                foreach (Node e in this.road)
                    if (start != e && end != e)
                        GridMapDict[e].Show(BlueItem);

        }
       
        [ContextMenu("Dijkstra算法")]
        public void TestBFSGetMinCostRoad()
        {
            var map = BuildGridMap();
            var way = Dijkstra.FindWay(start, end, map);
            if (null != way)
                ShowRoad(way);
        }
        [ContextMenu("Astar+Geedy(距离)算法")]
        public void TestBFSWithGreedyMinDistanceRoad()
        {
            var map = BuildGridMap();
            var way = Astar.FindWayGreedy(start, end, map);
            if (null != way)
                ShowRoad(way);
        }
        [ContextMenu("Astar算法")]
        public void TestAstarGetMinCostRoad()
        {
            var map = BuildGridMap();
            var way = Astar.FindWay(start, end, map);
            if (null != way)
                ShowRoad(way);
        }
        [ContextMenu("Astar算法:不使用closelist")]
        public void TestDijkstraGreedy()
        {
            var map = BuildGridMap();
            var way = Astar.FindWay2(start, end, map);
            if (null != way)
                ShowRoad(way);
        }
        [ContextMenu("战棋可到达位置检测")]
        public void TestGetReach()
        {
            var map = BuildGridMap();
            var way = BFS.GetReachList(start, reachCost, map);
            if (null != way)
                ShowRoad(way);
        }
        #endregion

        private void InitGridMapDict()
        {
            int count = transform.childCount;
            for (int i = 0; i < count; i++)
            {
                GridMap gm = transform.GetChild(i).GetComponent<GridMap>();
                if (null != gm)
                {
                    GridMapDict[(Node)gm.SelfNode] = gm;
                }
            }
        }
        
        
        private Dictionary<Node, int> BuildGridMap()
        {
            Dictionary<Node, int> map = new Dictionary<Node, int>();
            InitGridMapDict();
            var enumerator = GridMapDict.GetEnumerator();

            try
            {
                while(enumerator.MoveNext())
                {
                    var cur = enumerator.Current;
                    map.Add(cur.Key, cur.Value.Cost);
                }
            }
            finally
            {
                enumerator.Dispose();
            }

            return map;
        }
    }
}

