using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LinHoweEightQueens
{
    /// <summary>
    /// 遗传算法
    /// </summary>
    public class Genetic
    {
        //private int[] EightQueens = new int[8];
        //private int ans = 0;
        private List<List<int>> possibleList = new List<List<int>>();
        private List<int[]> Population = new List<int[]>();
        private double[] adaptive = new double[8];
        private double[] accumuAdaptive = new double[8];
        private const int BestAdaptive = 28;
        private bool nosucces = true;
        public Genetic()
        {
            SetPopulation();
            while(nosucces)
            {
                Select();
                Crossover();
                Mutate();
            }
            Debug.Log(possibleList);
        }

        public List<List<int>> PossibleList
        {
            get
            {
                return possibleList;
            }
        }


        /// <summary>
        /// 初始化种群
        /// </summary>
        private void SetPopulation()
        {            
            for (int i = 0;i < 8;++i)
            {
                int[] tmpState = new int[8];
                for (int j =0;j<8;++j)
                {
                    tmpState[j] = UnityEngine.Random.Range(0,8);
                }
                Population.Add(tmpState);
                adaptive[i] = CalcuAdaptive(tmpState);
            }
        }

        /// <summary>
        /// 计算适应值
        /// </summary>
        private double CalcuAdaptive(int[] state)
        {
            int conflict = 0;
            for (int i = 0; i <8; ++i)
            {
                for (int j = i + 1; j < 8; ++j)
                {
                    if (state[i] == state[j] || Mathf.Abs(state[i] - state[j]) == j - i)
                        conflict++;                 
                }
            }
            if (conflict == 0)  // 找到最优解
            {
                List<int> arr = new List<int>();
                arr.AddRange(state);
                PossibleList.Add(arr);
                nosucces = false;
            }
            return 1.0 / conflict;
        }

        // 物竞天择
        private void Select()
        {
            List<int[]> newPopulation = new List<int[]>();
            accumuAdaptive[0] = adaptive[0];
            for(int i = 1; i < 8;++i)
            {
                accumuAdaptive[i] = accumuAdaptive[i-1] + adaptive[i];
            }
            double totalAdaptive = accumuAdaptive[8 - 1];

            for(int i =0;i<8;++i)
            {
                int magnifyTotalAdaptive = (int)totalAdaptive * 100000;  // 先乘以十万
                int random = UnityEngine.Random.Range(0, magnifyTotalAdaptive);  // % 运算符要求整数
                double select = (double)random / 100000;               // 再除以十万
                double min = accumuAdaptive.Min(x => x);
                int j = 0;
                for (; j < 8; ++j)
                    if (accumuAdaptive[j] == min)
                        break;
                newPopulation.Add(Population[j]);
            }
            Population = newPopulation;
        }

        private void Crossover()
        {
            for(int i = 0;i<8;i+=2)
            {
                int midA = 8 / 3;
                int midB = 8 * 2 / 3;
                for (int j = midA; j < midB; ++j)
                {
                    int tmp = Population[i][j];
                    Population[i][j] = Population[i + 1][j];
                    Population[i + 1][j] = tmp;
                }
            }
        }

        private void Mutate()
        {
            //单点随机突变
            int mutateSpot = 0;
            for (int i = 0; i < 8; ++i)
            {
                if (UnityEngine.Random.Range(0,2) == 0)
                {
                    mutateSpot = UnityEngine.Random.Range(0, 8);
                    Population[i][mutateSpot] = UnityEngine.Random.Range(0, 8);
                }
                adaptive[i] = CalcuAdaptive(Population[i]);  // 更新适应值
            }
        }
    }
}
