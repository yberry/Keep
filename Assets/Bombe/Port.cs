using UnityEngine;
using System.Collections.Generic;

public class Port : MonoBehaviour {

    public enum Type
    {
        DVI_D,
        Parralele,
        PS_2,
        RJ_45,
        Serie,
        Stereo_RCA
    }

    private static List<Type> pris;

    private Type nom;
    public Type Nom
    {
        get
        {
            return nom;
        }
    }

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
	
	// Update is called once per frame
	void Update () {
	
	}

    public static void Reset()
    {
        pris = new List<Type>();
    }
}
