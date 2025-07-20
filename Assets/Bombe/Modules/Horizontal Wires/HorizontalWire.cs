using UnityEngine;

public class HorizontalWire : MonoBehaviour {

    [SerializeField]
    private Renderer fullWire;

    [SerializeField]
    private Renderer[] cutWires;

    private HorizontalWires fils;

    private bool isTarget = false;
    private bool isCut = false;

    public HorizontalWires.WireColor Color { get; private set; }

    public void SetModule(HorizontalWires f, HorizontalWires.WireMat wireMat)
    {
        fils = f;
        Color = wireMat.color;
        fullWire.material = wireMat.material;
        for (int i = 0; i < cutWires.Length; i++)
        {
            cutWires[i].material = wireMat.material;
            cutWires[i].gameObject.SetActive(false);
        }
    }

    public void SetTarget()
    {
        isTarget = true;
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
            cutWires[i].gameObject.SetActive(true);
        }
        GetComponent<Collider>().enabled = false;

        if (isTarget)
        {
            fils.Resolu();
        }
        else
        {
            fils.Faute();
        }
    }
}
