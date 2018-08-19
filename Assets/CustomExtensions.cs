using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public static class CustomExtensions {

    public static T RandomItem<T>(this IEnumerable<T> source)
    {
        if (source == null)
        {
            throw new System.ArgumentNullException();
        }

        int count = source.Count();
        if (count == 0)
        {
            throw new System.ArgumentException();
        }

        int random = Random.Range(0, count);
        return source.ElementAt(random);
    }

    public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
    {
        T comp = gameObject.GetComponent<T>();

        return comp ?? gameObject.AddComponent<T>();
    }

    public static T GetOrAddComponent<T>(this Component component) where T : Component
    {
        T comp = component.GetComponent<T>();

        return comp ?? component.gameObject.AddComponent<T>();
    }
}
