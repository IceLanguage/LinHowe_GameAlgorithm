using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LinHoweMazeGenerate
{
    public class MazeManager : MonoBehaviour
    {
        //挡板
        public GameObject rowPrefab;
        public GameObject colPrefab;

        //原点
        private Vector3 ZeroPos = new Vector3(-5f, -5f, 0);
        //终点
        private Vector3 MaxPos = new Vector3(5f, 5f, 0);

        //行间隔
        private float rowPur;
        //列间隔
        private float colPur;

        //原点行挡板位置
        private Vector3 ZeroRowPos;
        //原点列挡板位置
        private Vector3 ZeroColPos;

        //迷宫分块数
        public int Size;

        //行挡板
        bool[,] row;
        GameObject[,] rowGo;

        //列挡板
        bool[,] col;
        GameObject[,] colGo;

        private void OnEnable()
        {
            Init();
            GenerateMaze();
        }
        private void Init()
        {
            if (Size < 1)
                Size = 1;

            row = new bool[Size, Size + 1];
            col = new bool[Size, Size + 1];
            rowGo = new GameObject[Size, Size + 1];
            colGo = new GameObject[Size, Size + 1];

            rowPur = (MaxPos.x - ZeroPos.x) / Size;
            colPur = (MaxPos.y - ZeroPos.y) / Size;

            ZeroRowPos = new Vector3(ZeroPos.x + rowPur / 2, 0, ZeroPos.y);
            ZeroColPos = new Vector3(ZeroPos.x, 0, ZeroPos.y + colPur / 2);

            //迷宫外壁填充
            //for (int i = 0; i < Size; i++)
            //{
            //    row[i, 0] = true;
            //    row[i, Size] = true;
            //    col[i, 0] = true;
            //    col[i, Size] = true;
            //}

            //挡板全部填充
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j <= Size; j++)
                {
                    row[i, j] = true;
                    col[i, j] = true;
                }
            }

        }
        /// <summary>
        /// 迷宫生成
        /// </summary>
        private void GenerateMaze()
        {

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j <= Size; j++)
                {
                    //Destroy
                    if (null != rowGo[i, j])
                    {
                        DestroyImmediate(rowGo[i, j]);
                    }
                    if (null != colGo[i, j])
                    {
                        DestroyImmediate(colGo[i, j]);
                    }

                    //Build
                    if (row[i, j])
                    {
                        rowGo[i, j] = Instantiate(rowPrefab);
                        rowGo[i, j].transform.position = new Vector3(
                            ZeroRowPos.x + i * rowPur, 0, ZeroRowPos.z + j * colPur);
                        rowGo[i, j].transform.localScale =
                            new Vector3(rowGo[i, j].transform.localScale.x / Size,
                            rowGo[i, j].transform.localScale.y,
                            rowGo[i, j].transform.localScale.z);
                        rowGo[i, j].transform.parent = transform;
                    }
                    if (col[i, j])
                    {
                        colGo[i, j] = Instantiate(colPrefab);
                        colGo[i, j].transform.position = new Vector3(
                            ZeroColPos.x + j * colPur, 0, ZeroColPos.z + i * rowPur);
                        colGo[i, j].transform.localScale =
                            new Vector3(colGo[i, j].transform.localScale.x / Size,
                            colGo[i, j].transform.localScale.y,
                            colGo[i, j].transform.localScale.z);
                        colGo[i, j].transform.parent = transform;
                    }
                }
            }
        }
        #region 编辑器扩展

        [ContextMenu("递归回溯算法生成迷宫")]
        public void RB()
        {
            MazeWall mw = RecursiveBacktracking.Generate(new MazeWall(Size));
            row = mw.row;
            col = mw.col;
            GenerateMaze();
        }
        #endregion
    }
}