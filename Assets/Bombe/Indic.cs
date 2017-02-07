using UnityEngine;
using System.Collections.Generic;

public class Indic : MonoBehaviour {

    private static readonly string[] ind = new string[]
    {
        "SND",
        "CLR",
        "CAR",
        "IND",
        "FRQ",
        "SIG",
        "NSA",
        "MSA",
        "TRN",
        "BOB",
        "FRK"
    };

    private static List<string> pris = new List<string>();

    public string mention { get; private set; }

    public Light lumiere;

	// Use this for initialization
	void Start () {
        mention = ind[Random.Range(0, ind.Length)];
        while (pris.Contains(mention))
        {
            mention = ind[Random.Range(0, ind.Length)];
        }
        lumiere.enabled = Random.Range(0, 2) == 0;
        pris.Add(mention);
	}

    public static void Reset()
    {
        pris = new List<string>();
    }
}
