using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SequencesFils : Module {

    private static readonly Dictionary<Color, List<string>> tab = new Dictionary<Color, List<string>>()
    {
        { Color.red, new List<string>() { "C", "B", "A", "AC", "B", "AC", "ABC", "AB", "B" } },
        { Color.blue, new List<string>() { "B", "AC", "B", "A", "B", "BC", "C", "AC", "A" } },
        { Color.black, new List<string>() { "ABC", "AC", "B", "AC", "B", "BC", "AB", "C", "C" } }
    };

    private const int nbPanneaux = 4;
    private int currentLayer = 0;
    private Panneau[] panneaux;

    public Button boutonHaut;
    public Button boutonBas;
    public Light[] succes;
    public Panneau prefabPanneau;

    private void Start()
    {
        foreach (Light light in succes)
        {
            light.color = Color.green;
            light.enabled = false;
        }

        boutonHaut.onClick.AddListener(Haut);
        boutonBas.onClick.AddListener(Bas);

        panneaux = new Panneau[nbPanneaux];
        for (int i = 0; i < nbPanneaux; i++)
        {
            panneaux[i] = Instantiate(prefabPanneau);
            panneaux[i].SetModule(this);
        }
    }

    void Restart()
    {

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
