using System.Collections.Generic;
using UnityEngine;
using System;

public class ListConverter : MonoBehaviour
{
    public static List<List<T>> GetSeparatedList<T>(IReadOnlyList<T> oldList, int groupSize)
    {
        if (oldList == null)
        {
            throw new ArgumentNullException(nameof(oldList));
        }

        if (groupSize < 1)
        {
            throw new ArgumentException($"{nameof(groupSize)} must be positive.");
        }

        List<List<T>> targetList = new List<List<T>>();

        int i = 0;

        while (i < oldList.Count)
        {
            List<T> list = new List<T>();
            int j = 0;

            while (i < oldList.Count && j < groupSize)
            {
                list.Add(oldList[i]);
                i++;
                j++;
            }

            targetList.Add(list);
        }

        return targetList;
    }
}
