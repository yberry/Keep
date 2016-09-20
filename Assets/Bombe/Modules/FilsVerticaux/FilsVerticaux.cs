using UnityEngine;
using System.Collections.Generic;

public class FilsVerticaux : Module {

    public FilVertical[] prefabsFils;

    private int nbFils;
    private List<FilVertical> fils;

	// Use this for initialization
	void Start () {
        nbFils = Random.Range(3, 7);
        RemplirListe();
	}

    void RemplirListe()
    {
        fils = new List<FilVertical>();

        int plein = nbFils;
        int vide = 6 - nbFils;

        for (int i = 0; i < 6; i++)
        {
            if (plein > 0 && Random.Range(0, 2) == 0)
            {
                fils.Add(prefabsFils[i]);
                plein--;
            }
            else if (vide > 0)
            {
                prefabsFils[i].gameObject.SetActive(false);
                vide--;
            }
            else
            {
                i--;
            }
        }
    }

    public void Verif()
    {
        bool complet = true;
        foreach (FilVertical fil in fils)
        {
            complet &= fil.Complet();
        }
        if (complet)
        {
            Resolu();
        }
    }
}
