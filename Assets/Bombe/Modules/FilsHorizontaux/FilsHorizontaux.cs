using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class FilsHorizontaux : Module {

    [SerializeField]
    private FilHorizontal[] prefabsFils;

    private int nbFils;
    private List<FilHorizontal> fils;

	void Start ()
    {
        nbFils = Random.Range(3, 7);
        RemplirListe();
        DefinirObjectif();
	}

    void RemplirListe()
    {
        fils = new List<FilHorizontal>();

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
                prefabsFils[i].gameObject.SetActive(false);
                vide--;
            }
            else
            {
                i--;
            }
        }
    }

    void DefinirObjectif()
    {
        switch (nbFils)
        {
            case 3:
                if (NbCouleurs(Color.red) == 0)
                {
                    fils[1].Objectif();
                }
                else if (fils[nbFils - 1].Couleur == Color.white)
                {
                    fils[nbFils - 1].Objectif();
                }
                else if (NbCouleurs(Color.blue) > 1)
                {
                    fils.Last(f => f.Couleur == Color.blue).Objectif();
                }
                else
                {
                    fils[nbFils - 1].Objectif();
                }
                return;

            case 4:
                if (NbCouleurs(Color.red) > 1 && !Serial.instance.NumPair)
                {
                    fils.Last(f => f.Couleur == Color.red).Objectif();
                }
                else if (fils[nbFils - 1].Couleur == Color.yellow && NbCouleurs(Color.red) == 0)
                {
                    fils[0].Objectif();
                }
                else if (NbCouleurs(Color.blue) == 1)
                {
                    fils[0].Objectif();
                }
                else if (NbCouleurs(Color.yellow) > 1)
                {
                    fils[nbFils - 1].Objectif();
                }
                else
                {
                    fils[1].Objectif();
                }
                return;

            case 5:
                if (fils[nbFils - 1].Couleur == Color.black && !Serial.instance.NumPair)
                {
                    fils[3].Objectif();
                }
                else if (NbCouleurs(Color.red) == 1 && NbCouleurs(Color.yellow) > 1)
                {
                    fils[0].Objectif();
                }
                else if (NbCouleurs(Color.black) == 0)
                {
                    fils[1].Objectif();
                }
                else
                {
                    fils[0].Objectif();
                }
                return;

            case 6:
                if (NbCouleurs(Color.yellow) == 0 && !Serial.instance.NumPair)
                {
                    fils[2].Objectif();
                }
                else if (NbCouleurs(Color.yellow) == 1 && NbCouleurs(Color.white) > 1)
                {
                    fils[3].Objectif();
                }
                else if (NbCouleurs(Color.red) == 0)
                {
                    fils[nbFils - 1].Objectif();
                }
                else
                {
                    fils[3].Objectif();
                }
                return;
        }
    }

    int NbCouleurs(Color c)
    {
        return fils.Count(f => f.Couleur == c);
    }
}
