using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace LinHoweGameTree
{
    public class piece:MonoBehaviour
    {
        private Image image;
        private int i, j;
        private void Awake()
        {
            image = GetComponent<Image>();
            image.enabled = false;
            GetComponent<Button>().onClick.AddListener(Chess);
        }
        public void Record(int i,int j)
        {
            this.i = i;
            this.j = j;
        }
        /// <summary>
        /// 下棋
        /// </summary>
        public void Chess()
        {
            if(!image.enabled)
            {
                image.enabled = true;
                if(1 == GameManager.Instance.Around)
                {
                    image.sprite = GameManager.Instance.Black;
                    GameManager.Instance.Around = 0;
                    GameManager.Instance.lazis[i, j] = -1;
                }
                else
                {
                    image.sprite = GameManager.Instance.White;
                    GameManager.Instance.Around = 1;
                    GameManager.Instance.lazis[i, j] = 1;
                }

            }
        }
    }
}
