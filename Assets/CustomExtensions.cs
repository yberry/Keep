using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public static class CustomExtensions {

    public static T RandomItem<T>(this IEnumerable<T> source)
    {
        int count = source.Count();
        if (count == 0)
        {
            throw new System.ArgumentOutOfRangeException("Source", "Random in empty source");
        }

        int random = Random.Range(0, count);
        return source.ElementAt(random);
    }
}
