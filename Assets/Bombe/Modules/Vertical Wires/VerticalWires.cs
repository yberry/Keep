using UnityEngine;
using System.Collections.Generic;

public class VerticalWires : Module {

    [SerializeField]
    private VerticalWire[] prefabsFils;

    private int wireCount;
    private List<VerticalWire> wires;

    private bool Complet => wires.TrueForAll(f => f.Complet);

	// Use this for initialization
	void Start () {
        wireCount = Random.Range(3, 7);
        RemplirListe();
	}

    void RemplirListe()
    {
        wires = new List<VerticalWire>();

        int plein = wireCount;
        int vide = 6 - wireCount;

        for (int i = 0; i < 6; i++)
        {
            if (plein > 0 && Bombe.HeadsOrTails)
            {
                prefabsFils[i].SetModule(this);
                wires.Add(prefabsFils[i]);
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
            wires.ForEach(f => f.Restart());
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
