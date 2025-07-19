using UnityEngine;
using System.Collections.Generic;

public class FilsVerticaux : Module {

    [SerializeField]
    private FilVertical[] prefabsFils;

    private int nbFils;
    private List<FilVertical> fils;

    private bool Complet
    {
        get
        {
            return fils.TrueForAll(f => f.Complet);
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
            fils.ForEach(f => f.Restart());
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
