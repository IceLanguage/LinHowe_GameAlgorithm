using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace LinHoweEightQueens
{
    public class GameManager : MonoBehaviour
    {
        public Image prefabImage;
        private Image[,] EightQueensImage;
        private int[] EightQueens;
        private int ans = 0;
        private List<List<int>> possibleList;
        private void OnEnable()
        {
            EightQueensImage = new Image[8, 8];
            EightQueens = new int[8];
            EightQueensImage[0, 0] = prefabImage;
            for(int i=0;i<8;++i)
            {
                for(int j=0;j<8;++j)
                {
                    if (null != EightQueensImage[i,j])
                        continue;
                    GameObject go = Instantiate(prefabImage.gameObject);
                    go.transform.position = new Vector3(
                        prefabImage.transform.position.x + i*50,
                        prefabImage.transform.position.y - j * 50,
                        prefabImage.transform.position.z);
                    go.transform.SetParent(transform);
                    EightQueensImage[i, j] = go.GetComponent<Image>();
                }
            }
            TestCSP();
            StartCoroutine(showEightQueens());
        }
        IEnumerator showEightQueens()
        {
            int j = Random.Range(0, possibleList.Count);
            var arr = possibleList[j];
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < 8; ++i)
            {
                EightQueensImage[i, arr[i]].color = Color.red;
            }
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < 8; ++i)
            {
                EightQueensImage[i, arr[i]].color = new Color(157 / 255f, 255 / 255f, 77 / 255f, 1);
            }
            StartCoroutine(showEightQueens());
        }

        [ContextMenu("回溯递归解法")]
        public void TestDFS()
        {
            possibleList = new DFS().PossibleList;
        }
        [ContextMenu("对角线检查")]
        public void TestDC()
        {
            possibleList = new DiagonalCheck().PossibleList;
        }
        [ContextMenu("遗传算法")]
        public void TestGenetic()
        {
            possibleList = new Genetic().PossibleList;
        }
        [ContextMenu("CSP最小冲突法")]
        public void TestCSP()
        {
            possibleList =new List<List<int>>() { new MinConflict().chessBoard.ToList()};
        }
    }
}
