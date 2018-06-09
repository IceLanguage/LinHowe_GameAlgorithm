using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LinHoweGameTree
{
    public class GameManager : UnityComponentSingleton<GameManager>
    {
        public GameObject piecePrefab;
        public Sprite Black, White;
        
        private const float xoffset = 52.5f, yoffset = 39.5f;
        private piece[,] allPiece = new piece[15, 15];
        public int[,] lazis = new int[15, 15];
        //回合， 1是我方回合，0是AI回合
        public int Around = 1;
        private void Awake()
        {
            allPiece[0, 0] = piecePrefab.GetComponent<piece>();
            Vector2 zero= piecePrefab.GetComponent<RectTransform>().anchoredPosition;
            for(int i =0;i<15;++i)
            {
                for(int j=0;j<15;++j)
                {
                    if (null == allPiece[i, j])
                    {
                        GameObject newpiece = Instantiate(piecePrefab,transform);
                        allPiece[i, j] = newpiece.GetComponent<piece>();
                        allPiece[i, j].Record(i, j);
                        newpiece.GetComponent<RectTransform>().anchoredPosition =
                            new Vector2(zero.x + xoffset * i, zero.y - yoffset * j);
                    }

                    lazis[i, j] = 0;
                }
            }
        }

        private void Update()
        {
            AI();
        }

        private void AI()
        {
            if (1 == Around) return;

            
            Lazi res = gobangAI.AILazi(lazis);
            allPiece[res.x, res.y].Chess();
        }
    }
}

