using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace LinHoweFindPath
{
    
    
    /// <summary>
    /// 格子
    /// </summary>
    public class GridMap : MonoBehaviour
    {

        #region 格子属性
        //是否是障碍
        public bool Obstacle = false;

        //节点消耗
        public int cost = 1;
        public int Cost
        {
            get
            {
                if (Obstacle) return int.MaxValue;
                return cost;
            }
        }

        //格子宽度
        private const int MapSize = 1;
        private Node? selfNode;
        public Node? SelfNode
        {
            get
            {
                if(null == selfNode)
                {
                    int x = (int)transform.position.x;
                    int z = (int)transform.position.z;
                    selfNode = new Node(x, z);
                }
                return selfNode;
                
            }
        }

        #endregion
        #region 其他属性
        //用于编辑地形格子的数量
        public int BuildGridCount = 1;

       
        #endregion
        private void OnEnable()
        {
            
            
            Node node = (Node)SelfNode;
            GridMapManager.Instance.GridMapDict.Add(node, this);
            if (SelfNode == GridMapManager.Instance.start)
            {
                Show(GridMapManager.Instance.YellowItem);
                return;
            }
            if (SelfNode == GridMapManager.Instance.end)
            {
                Show(GridMapManager.Instance.PurpleItem);
                return;
            }
            if (Obstacle)
            {
                Show(GridMapManager.Instance.RedItem);
                return;
            }
        }
        /// <summary>
        /// 更换颜色
        /// </summary>
        /// <param name="texture"></param>
        public void Show(Texture texture)
        {
            var mr = GetComponent<MeshRenderer>();
            Material m = mr.material;
            m.SetTexture("_MainTex", texture);
        }
        #region 编辑器扩展
        /// <summary>
        /// 设置格子
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void SetGridGameObject(int x, int y, int z)
        {
            selfNode = new Node(x, z);     
            transform.position = new Vector3(x, y, z);
            var grid = GetComponent<GridMap>();
            name = "Tile";

        }
        #endregion

    }

}