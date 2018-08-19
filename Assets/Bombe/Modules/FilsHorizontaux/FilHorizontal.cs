using UnityEngine;

public class FilHorizontal : MonoBehaviour {

    private static readonly Color[] couleurs = new Color[]
    {
        Color.white,
        Color.black,
        Color.blue,
        Color.yellow,
        Color.red
    };
    public Color Couleur { get; private set; }

    private FilsHorizontaux fils;

    private bool aCouper = false;
    private bool estCoupe = false;

	// Use this for initialization
	void Start () {
        Couleur = couleurs.RandomItem();
        GetComponent<Renderer>().material.color = Couleur;
	}
	
	public void SetModule(FilsHorizontaux f)
    {
        fils = f;
    }

    public void Objectif()
    {
        aCouper = true;
    }

    void OnMouseDown()
    {
        if (estCoupe)
        {
            return;
        }

        estCoupe = true;

        if (aCouper)
        {
            fils.Resolu();
        }
        else
        {
            fils.Faute();
        }
    }
}
