using UnityEngine;

public class FilHorizontal : MonoBehaviour {

    private static Color[] couleurs
    {
        get
        {
            return new Color[]
            {
                Color.white,
                Color.black,
                Color.blue,
                Color.yellow,
                Color.red
            };
        }
    }
    private Color couleur;
    public Color Couleur
    {
        get
        {
            return couleur;
        }
    }

    private FilsHorizontaux fils;

    private bool aCouper = false;
    private bool estCoupe = false;

	// Use this for initialization
	void Start () {
        couleur = couleurs[Random.Range(0, couleurs.Length)];
        GetComponent<Renderer>().material.color = couleur;
	}
	
	public void SetModule(FilsHorizontaux f)
    {
        fils = f;
    }

    public void Objectif()
    {
        aCouper = true;
    }

    public void Coupe()
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
