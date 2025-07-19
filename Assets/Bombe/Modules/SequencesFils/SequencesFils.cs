using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class SequencesFils : Module {

    private static readonly Dictionary<Color, List<int>> tab = new Dictionary<Color, List<int>>()
    {
        { Color.red, new List<int>() { 4, 2, 1, 5, 2, 5, 7, 3, 2 } },
        { Color.blue, new List<int>() { 2, 5, 2, 1, 2, 6, 4, 5, 1 } },
        { Color.black, new List<int>() { 7, 5, 2, 5, 2, 6, 3, 4, 4 } }
    };

    private const int nbPanneaux = 4;

    [SerializeField]
    private Button boutonHaut;
    [SerializeField]
    private Button boutonBas;
    [SerializeField]
    private Light[] succes;
    [SerializeField]
    private Panneau prefabPanneau;

    private int currentLayer = 0;
    private Panneau[] panneaux;

    private void Start()
    {
        foreach (Light light in succes)
        {
            light.color = Color.green;
            light.enabled = false;
        }

        boutonHaut.onClick.AddListener(Haut);
        boutonBas.onClick.AddListener(Bas);

        Restart();
    }

    void Restart()
    {
        Dictionary<Color, int> nbColors = new Dictionary<Color, int>()
        {
            { Color.red, 0 },
            { Color.blue, 0 },
            { Color.black, 0 }
        };

        bool[] haveFil = Enumerable.Repeat(true, 3 * nbPanneaux).ToArray();
        for (int i = 0; i < 3; i++)
        {
            haveFil[i] = false;
        }

        while (!CheckRepartition(haveFil))
        {
            haveFil.Shuffle();
        }

        List<Panneau.Fil?> fils = new List<Panneau.Fil?>(3 * nbPanneaux);
        for (int i = 0; i < fils.Count; i++)
        {
            if (haveFil[i])
            {
                Color col = nbColors.Keys.RandomItem();
                int tar = Random.Range(0, 3);

                fils[i] = new Panneau.Fil
                {
                    color = col,
                    target = tar,
                    mustCut = (tab[col][nbColors[col]++] & (1 << tar)) != 0,
                    isCut = false
                };
            }
            else
            {
                fils[i] = null;
            }
        }

        panneaux = new Panneau[nbPanneaux];
        for (int i = 0; i < nbPanneaux; i++)
        {
            Panneau panneau = Instantiate(prefabPanneau);
            panneau.SetModule(this);
            panneau.SetLabels(i);
            panneau.SetFils(fils.GetRange(3 * i, 3));
            panneaux[i] = panneau;
        }
    }

    bool CheckRepartition(IEnumerable<bool> rep)
    {
        for (int i = 0; i < nbPanneaux; i++)
        {
            if (rep.Skip(3 * i).Take(3).All(b => !b))
            {
                return false;
            }
        }

        return true;
    }

    void Haut()
    {
        if (!Desamorce && currentLayer > 0)
        {
            panneaux[currentLayer].gameObject.SetActive(false);
            panneaux[--currentLayer].gameObject.SetActive(true);
        }
    }

    void Bas()
    {
        if (Desamorce)
        {
            return;
        }

        if (panneaux[currentLayer].Complete)
        {
            succes[currentLayer].enabled = true;
            panneaux[currentLayer].gameObject.SetActive(false);
            if (++currentLayer == nbPanneaux)
            {
                Resolu();
            }
            else
            {
                panneaux[currentLayer].gameObject.SetActive(true);
            }
        }
        else
        {
            Faute();
        }
    }
}
