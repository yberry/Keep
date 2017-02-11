using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class FilsHorizontaux : Module {

    public FilHorizontal[] prefabsFils;

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
                else if (fils[nbFils - 1].couleur == Color.white)
                {
                    fils[nbFils - 1].Objectif();
                }
                else if (NbCouleurs(Color.blue) > 1)
                {
                    for (int i = nbFils - 1; i >= 0; i--)
                    {
                        if (fils[i].couleur == Color.blue)
                        {
                            fils[i].Objectif();
                            return;
                        }
                    }
                }
                else
                {
                    fils[nbFils - 1].Objectif();
                }
                return;

            case 4:
                if (NbCouleurs(Color.red) > 1 && !Bombe.instance.numPair)
                {
                    for (int i = nbFils - 1; i >= 0; i--)
                    {
                        if (fils[i].couleur == Color.red)
                        {
                            fils[i].Objectif();
                            return;
                        }
                    }
                }
                else if (fils[nbFils - 1].couleur == Color.yellow && NbCouleurs(Color.red) == 0)
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
                if (fils[nbFils - 1].couleur == Color.black && !Bombe.instance.numPair)
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
                if (NbCouleurs(Color.yellow) == 0 && !Bombe.instance.numPair)
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
        return fils.Count(f => f.couleur == c);
    }
}
