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
        private List<List<int>> possibleList;
        public static readonly Color defaultColor = new Color(157 / 255f, 255 / 255f, 77 / 255f, 1);
        private void OnEnable()
        {
            EightQueensImage = new Image[8, 8];
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
            TestDFS();
            StartCoroutine(ShowEightQueens());
        }
        IEnumerator ShowEightQueens()
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
                EightQueensImage[i, arr[i]].color = defaultColor;
            }
            StartCoroutine(ShowEightQueens());
        }

        [ContextMenu("回溯递归解法")]
        public void TestDFS()
        {
            possibleList = new DFS().PossibleList;
            Debug.Log(possibleList.Count);
        }
        [ContextMenu("对角线检查")]
        public void TestDC()
        {
            possibleList = new DiagonalCheck().PossibleList;
            Debug.Log(possibleList.Count);
        }
        
    }
}
