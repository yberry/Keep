using UnityEngine;
using System.Collections.Generic;

public class Symboles : Module {

    public enum Type
    {

    }

    public Symbole[] symboles;

    private const int nbColonnes = 6;
    private const int nbSymboles = 7;

    /*private static Symbole.Type[,] colonnes = new Symbole.Type[nbColonnes, nbSymboles]
    {
        { },
        { },
        { },
        { },
        { },
        { },
    };*/

    private static Type[,] colonnes;

	// Use this for initialization
	void Start () {
        int col = Random.Range(0, nbColonnes);

        List<int> numPris = new List<int>();

        for (int i = 0; i < symboles.Length; i++)
        {
            int num = Random.Range(0, nbSymboles);
            while (numPris.Contains(num))
            {
                num = Random.Range(0, nbSymboles);
            }
            //symboles[i].SetSymbole(colonnes[col, num]);
            numPris.Add(num);
        }
	}

    public void Verif()
    {
        bool valide = true;
        foreach (Symbole symbole in symboles)
        {
            valide &= symbole.Appuye;
        }
        if (valide)
        {
            Resolu();
        }
    }
}
