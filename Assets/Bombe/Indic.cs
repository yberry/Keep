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

    public string Mention { get; private set; }

    public Light lumiere;

	// Use this for initialization
	void Start () {
        do
        {
            Mention = ind.RandomItem();
        }
        while (pris.Contains(Mention));

        lumiere.enabled = Bombe.HeadsOrTails;
        pris.Add(Mention);
	}

    public static void Reset()
    {
        pris = new List<string>();
    }
}
