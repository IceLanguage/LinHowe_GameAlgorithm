using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace LinHoweEightQueens
{
    public class GameManager : MonoBehaviour
    {
        public Image prefabImage;
        private Image[,] EightQueensImage;
        private int[] EightQueens;
        private int ans = 0;
        private List<List<int>> possibleList = new List<List<int>>();
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
            //dfs();
            Debug.Log(ans);
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

        private bool Check(int i, int v)
        {
            for (int k = 0; k < i; k++)
            {
                if (EightQueens[k] == v) return false;
                if (EightQueens[k] - v == i - k ||
                    EightQueens[k] - v == k - i)
                    return false;
            }
            return true;
        }
        public void dfs(int r = 0)
        {
            if (r >= 8)
            {
                ans++;
                List<int> arr = new List<int>();
                arr.AddRange(EightQueens);
                possibleList.Add(arr);
                return;
            }
            for (int i = 0; i < 8; i++)
            {
                if (Check(r, i))
                {
                    EightQueens[r] = i;
                    dfs(r + 1);
                    EightQueens[r] = 0;
                }
            }
        }
    }
}
