using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public static class CustomExtensions {

    public static T RandomItem<T>(this IEnumerable<T> source)
    {
        int count = source.Count();
        if (count == 0)
        {
            return default(T);
        }

        int random = Random.Range(0, count);
        return source.ElementAt(random);
    }

    public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
    {
        T component = gameObject.GetComponent<T>();

        return component == null ? gameObject.AddComponent<T>() : component;
    }
}
