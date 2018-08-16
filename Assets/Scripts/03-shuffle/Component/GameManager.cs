using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyUnityEventDispatcher;
using DG.Tweening;

namespace LinHoweShuffle
{
    public class GameManager : MonoBehaviour
    {

        private Transform[] pukeTransform = new Transform[54];
        private Vector3[] pukesPos = new Vector3[54];
        public GameObject canvas;
        private void Awake()
        {
            for (int i = 0; i < 54; i++)
            {
                pukeTransform[i] = canvas.transform.GetChild(i);
                pukesPos[i] = pukeTransform[i].position;
            }
            NotificationCenter<KeyValuePair<int, int>>.Get().AddEventListener
                ("SwapPuke", SwapPuke);
            NotificationCenter<KeyValuePair<int, int>>.Get().AddEventListener
                ("MovePuke", MovePuke);
            StartCoroutine(ShowSwap());
            StartCoroutine(ShowMove());
        }
        private void Init()
        {
            for (int i = 0; i < 54; i++)
            {
                pukeTransform[i].transform.position = pukesPos[i];
            }
        }
        /// <summary>
        /// 交换扑克牌
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        private void SwapPuke(int i, int j)
        {
            Vector3 p1 = pukeTransform[i].transform.position;
            pukeTransform[i].transform.DOMove(pukeTransform[j].transform.position, 0.2f);
            pukeTransform[j].transform.DOMove(p1, 0.2f);
        }
        private void MovePuke(int move,int to)
        {
            pukeTransform[move].transform.DOMove(pukesPos[to], 0.2f);
        }
        private void SwapPuke(Notification<KeyValuePair<int, int>> notific)
        {
            q1.Enqueue(notific.param);
        }
        private void MovePuke(Notification<KeyValuePair<int, int>> notific)
        {
            q2.Enqueue(notific.param);
        }
        #region 编辑器扩展
        private Queue<KeyValuePair<int,int>> q1 = new Queue<KeyValuePair<int, int>>();
        private Queue<KeyValuePair<int, int>> q2 = new Queue<KeyValuePair<int, int>>();
        IEnumerator ShowSwap()
        {
            if (0 == q1.Count)
            {
                yield return new WaitForSeconds(0.8f);
                StartCoroutine(ShowSwap());

            }
            else
            {
                KeyValuePair<int, int> p = q1.Dequeue();
                SwapPuke(p.Key, p.Value);
                yield return new WaitForSeconds(0.2f);
                StartCoroutine(ShowSwap());
            }
        }
        IEnumerator ShowMove()
        {
            if (0 == q2.Count)
            {
                yield return new WaitForSeconds(0.8f);
                StartCoroutine(ShowMove());

            }
            else
            {
                KeyValuePair<int, int> p = q2.Dequeue();
                MovePuke(p.Key, p.Value);
                yield return new WaitForSeconds(0.02f);
                StartCoroutine(ShowMove());
            }
        }
        [ContextMenu("Fisher_Yates随机换牌算法")]
        public void TestFisher_Yates()
        {
            Init();
            Fisher_Yates.Shuffle(new Pukes(54));
        }
        [ContextMenu("Knuth_Durstenfeld随机换牌算法")]
        public void TestKnuth_Durstenfeld()
        {
            Init();
            Knuth_Durstenfeld.Shuffle(new Pukes(54));
        }
        [ContextMenu("Inside_Out随机换牌算法")]
        public void TestInside_Out()
        {
            Init();
            Inside_Out.Shuffle(new Pukes(54));
        }
        
        [ContextMenu("随机抽牌算法")]
        public void TestDraw()
        {
            Init();
            Draw.Shuffle(new Pukes(54));
        }
        #endregion
    }
}