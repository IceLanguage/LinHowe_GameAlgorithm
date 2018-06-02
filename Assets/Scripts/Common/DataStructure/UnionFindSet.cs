using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 并查集
/// </summary>
public class UnionFindSet
{
    int[] parents;

    public UnionFindSet(int size)
    {
        parents = new int[size];

        for (int i = 0; i < parents.Length; i++)
            parents[i] = i;
    }

    public bool Union(int a, int b)
    {
        if (root(a) == root(b))
            return false;
        else
        {
            parents[root(b)] = root(a);
            return true;
        }

    }

    int root(int a)
    {
        if (parents[a] == a)
            return a;
        else
            return parents[a] = root(parents[a]);
    }
}
