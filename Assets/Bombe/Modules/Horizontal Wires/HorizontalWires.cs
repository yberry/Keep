using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class HorizontalWires : Module {

    public enum WireColor
    {
        White, Black, Blue, Yellow, Red
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
    private HorizontalWire[] startWires;

    private int wireCount;
    private List<HorizontalWire> wires;

	void Start ()
    {
        wireCount = Random.Range(3, 7);
        FillList();
        SetTarget();
	}

    void FillList()
    {
        wires = new List<HorizontalWire>();

        bool[] tab = Bombe.GetRepartition(wireCount, 6);

        for (int i = 0; i < 6; i++)
        {
            if (tab[i])
            {
                startWires[i].SetModule(this, wireMats.RandomItem());
                wires.Add(startWires[i]);
            }
            else
            {
                startWires[i].gameObject.SetActive(false);
            }
        }
    }

    void SetTarget()
    {
        switch (wireCount)
        {
            case 3:
                if (ColorCount(WireColor.Red) == 0)
                {
                    wires[1].SetTarget();
                }
                else if (wires[^1].Color == WireColor.White)
                {
                    wires[^1].SetTarget();
                }
                else if (ColorCount(WireColor.Blue) > 1)
                {
                    wires.Last(f => f.Color == WireColor.Blue).SetTarget();
                }
                else
                {
                    wires[^1].SetTarget();
                }
                return;

            case 4:
                if (ColorCount(WireColor.Red) > 1 && !Serial.instance.LastEven)
                {
                    wires.Last(f => f.Color == WireColor.Red).SetTarget();
                }
                else if (wires[^1].Color == WireColor.Yellow && ColorCount(WireColor.Red) == 0)
                {
                    wires[0].SetTarget();
                }
                else if (ColorCount(WireColor.Blue) == 1)
                {
                    wires[0].SetTarget();
                }
                else if (ColorCount(WireColor.Yellow) > 1)
                {
                    wires[^1].SetTarget();
                }
                else
                {
                    wires[1].SetTarget();
                }
                return;

            case 5:
                if (wires[^1].Color == WireColor.Black && !Serial.instance.LastEven)
                {
                    wires[3].SetTarget();
                }
                else if (ColorCount(WireColor.Red) == 1 && ColorCount(WireColor.Yellow) > 1)
                {
                    wires[0].SetTarget();
                }
                else if (ColorCount(WireColor.Black) == 0)
                {
                    wires[1].SetTarget();
                }
                else
                {
                    wires[0].SetTarget();
                }
                return;

            case 6:
                if (ColorCount(WireColor.Yellow) == 0 && !Serial.instance.LastEven)
                {
                    wires[2].SetTarget();
                }
                else if (ColorCount(WireColor.Yellow) == 1 && ColorCount(WireColor.White) > 1)
                {
                    wires[3].SetTarget();
                }
                else if (ColorCount(WireColor.Red) == 0)
                {
                    wires[^1].SetTarget();
                }
                else
                {
                    wires[3].SetTarget();
                }
                return;
        }
    }

    int ColorCount(WireColor color)
    {
        return wires.Count(f => f.Color == color);
    }
}
