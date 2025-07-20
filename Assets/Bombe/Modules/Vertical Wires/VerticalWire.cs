using UnityEngine;

public class VerticalWire : MonoBehaviour {

    [SerializeField]
    private Renderer fullWire;
    [SerializeField]
    private Renderer[] cutWires;
    [SerializeField]
    private Light LED;
    [SerializeField]
    private GameObject star;

    private VerticalWires module;
    private VerticalWires.WireColor colors;
    private bool isTarget = false;
    private bool isCut = false;

    public bool Finished => !isTarget || isCut;

    public void SetModule(VerticalWires f)
    {
        module = f;
    }

    public void Restart(VerticalWires.WireMat[] wireMats)
    {
        int colorCount = Random.Range(1, 3);
        colors = VerticalWires.WireColor.None;
        for (int i = 0; i < colorCount; i++)
        {
            VerticalWires.WireColor color = (VerticalWires.WireColor)(1 << Random.Range(0, 4));
            colors |= color;
            //Set Material color
        }
        LED.enabled = Bombe.HeadsOrTails;
        star.SetActive(Bombe.HeadsOrTails);
        SetTarget();
    }

    void SetTarget()
    {
        bool b = (colors & VerticalWires.WireColor.Blue) != 0;
        bool r = (colors & VerticalWires.WireColor.Red) != 0;
        bool l = LED.enabled;
        bool e = star.activeSelf;

        if (!b && !l && (e || !r))
        {
            isTarget = true;
        }

        else if (b && ((e && r && !l) || (l && !r)))
        {
            isTarget = Bombe.instance.HasPort(Port.Type.Parallele);
        }

        else if (!e && ((r && (b || !l)) || (!r && b && !l)))
        {
            isTarget = Serial.instance.LastEven;
        }

        else if (!b && l && (r || e))
        {
            isTarget = Bombe.instance.NbPiles >= 2;
        }

        else
        {
            isTarget = false;
        }
    }

    void OnMouseDown()
    {
        if (isCut)
        {
            return;
        }

        isCut = true;
        fullWire.gameObject.SetActive(false);
        for (int i = 0; i < cutWires.Length; i++)
        {
            cutWires[i].gameObject.SetActive(false);
        }
        GetComponent<Collider>().enabled = false;

        if (isTarget)
        {
            module.Verif();
        }
        else
        {
            module.Faute();
        }
    }
}
