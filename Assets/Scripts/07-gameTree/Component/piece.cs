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
        public Image image;
        private Vector2Int lazi;
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
            lazi = new Vector2Int(i, j);
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
                    image.sprite = GameManager.Instance.PlaySprite;
                    
                    GameManager.Instance.lazis[lazi.x, lazi.y] = 1;
                    GameManager.Instance.TurnAround(lazi);
                }
                else
                {
                    image.sprite = GameManager.Instance.AISprite;
                    
                    GameManager.Instance.lazis[lazi.x, lazi.y] = -1;
                    GameManager.Instance.TurnAround(lazi);
                }

                
            }
        }
    }
}
