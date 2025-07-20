using UnityEngine;
using System.Collections.Generic;

public class VerticalWires : Module {

    [System.Flags]
    public enum WireColor
    {
        None = 0,
        Blue = 1,
        Red = 2,
        White = 4, 
        Yellow = 8
    }

    [System.Serializable]
    public struct WireMat
    {
        public WireColor label;
        public Color color;
    }

    [SerializeField]
    private WireMat[] wireMats;

    [SerializeField]
    private VerticalWire[] startWires;

    private int wireCount;
    private List<VerticalWire> wires;

    private bool Finished => wires.TrueForAll(f => f.Finished);

	// Use this for initialization
	void Start () {
        wireCount = Random.Range(3, 7);
        FillList();
        SetTargets();
	}

    void FillList()
    {
        wires = new List<VerticalWire>();

        bool[] tab = Bombe.GetRepartition(wireCount, 6);

        for (int i = 0; i < 6; i++)
        {
            if (tab[i])
            {
                startWires[i].SetModule(this);
                wires.Add(startWires[i]);
            }
            else
            {
                startWires[i].gameObject.SetActive(false);
            }
        }       
    }

    private void SetTargets()
    {
        while (Finished)
        {
            wires.ForEach(f => f.Restart(wireMats));
        }
    }

    public void Verif()
    {
        if (Finished)
        {
            Resolu();
        }
    }
}
