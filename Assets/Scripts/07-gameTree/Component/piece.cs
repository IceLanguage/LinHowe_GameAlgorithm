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
        private Lazi lazi;
        private void Awake()
        {

            image = GetComponent<Image>();
            
            UnShow();
            GetComponent<Button>().onClick.AddListener(Chess);
        }
        public void UnShow()
        {
            image.sprite = GameManager.Instance.Simple;
        }
        public void Record(int i,int j)
        {
            lazi = new Lazi(i, j);
        }
        /// <summary>
        /// 下棋
        /// </summary>
        public void Chess()
        {
            if(GameManager.Instance.Simple == image.sprite)
            {
                image.enabled = true;
                if(Around.我方 == GameManager.Instance.Around)
                {
                    image.sprite = GameManager.Instance.Black;
                    
                    GameManager.Instance.lazis[lazi.x, lazi.y] = 1;
                    //GameManager.Instance.CheckGameOver(lazi,-1);
                    GameManager.Instance.TurnAround();
                }
                else
                {
                    image.sprite = GameManager.Instance.White;
                    
                    GameManager.Instance.lazis[lazi.x, lazi.y] = -1;
                    //GameManager.Instance.CheckGameOver(lazi, 1);
                    GameManager.Instance.TurnAround();
                }

                
            }
        }
    }
}
