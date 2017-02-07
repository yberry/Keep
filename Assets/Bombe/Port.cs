using UnityEngine;
using System.Collections.Generic;

public class Port : MonoBehaviour {

    public enum Type
    {
        DVI_D,
        Parallele,
        PS_2,
        RJ_45,
        Serie,
        Stereo_RCA
    }

    private static List<Type> pris = new List<Type>();

    public Type nom { get; private set; }

	// Use this for initialization
	void Start () {
        System.Array values = System.Enum.GetValues(typeof(Type));
        nom = (Type)values.GetValue(Random.Range(0, values.Length));
        while (pris.Contains(nom))
        {
            nom = (Type)values.GetValue(Random.Range(0, values.Length));
        }
        pris.Add(nom);
	}

    public static void Reset()
    {
        pris = new List<Type>();
    }
}
