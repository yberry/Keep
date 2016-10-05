using UnityEngine;
using System.Collections.Generic;

public class FilsVerticaux : Module {

    public FilVertical[] prefabsFils;

    private int nbFils;
    private List<FilVertical> fils;

    private bool Complet
    {
        get
        {
            foreach (FilVertical fil in fils)
            {
                if (!fil.Complet)
                {
                    return false;
                }
            }
            return true;
        }
    }

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
                prefabsFils[i].SetModule(this);
                fils.Add(prefabsFils[i]);
                plein--;
            }
            else if (vide > 0)
            {
                prefabsFils[i].Desaffiche();
                vide--;
            }
            else
            {
                i--;
            }
        }

        while (Complet)
        {
            foreach (FilVertical fil in fils)
            {
                fil.Restart();
            }
        }
    }

    public void Verif()
    {
        if (Complet)
        {
            Resolu();
        }
    }
}
