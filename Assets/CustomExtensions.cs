using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public static class CustomExtensions {

    public static T RandomItem<T>(this IEnumerable<T> source)
    {
        int count = source.Count();
        if (count == 0)
        {
            throw new System.ArgumentException();
        }

        int random = Random.Range(0, count);
        return source.ElementAt(random);
    }

    public static void RemoveRandom<T>(this IList<T> source)
    {
        int index = Random.Range(0, source.Count);
        source.RemoveAt(index);
    }

    public static IEnumerable<T> RandomList<T>(this IEnumerable<T> source)
    {
        List<T> list = new List<T>(source);

        int n = list.Count;
        while (n > 0)
        {
            int k = Random.Range(0, n--);
            yield return list[k];
            list.RemoveAt(k);
        }
    }

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 0)
        {
            int k = Random.Range(0, n--);
            (list[n], list[k]) = (list[k], list[n]);
        }
    }

    public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
    {
        if (gameObject.TryGetComponent(out T component))
        {
            return component;
        }

        return gameObject.AddComponent<T>();
    }

    public static T GetOrAddComponent<T>(this Component component) where T : Component
    {
        return component.gameObject.GetOrAddComponent<T>();
    }
}
