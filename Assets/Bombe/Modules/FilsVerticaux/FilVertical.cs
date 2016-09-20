using UnityEngine;
using System.Collections.Generic;

public class FilVertical : MonoBehaviour {

    private static Color[] couleursDispo =
    {
        Color.blue,
        Color.red,
        Color.white
    };

    private List<Color> couleurs;
    public Light LED;
    public GameObject etoile;

    private bool aCouper = false;
    private bool estCoupe = false;

	// Use this for initialization
	void Start () {
        int nbCouleurs = Random.Range(1, 3);
        couleurs = new List<Color>();
        for (int i = 0; i < nbCouleurs; i++)
        {
            int rand = Random.Range(0, couleursDispo.Length);
            while (couleurs.Contains(couleursDispo[rand]))
            {
                rand = Random.Range(0, couleursDispo.Length);
            }
            couleurs.Add(couleursDispo[rand]);
        }
        LED.enabled = Random.Range(0, 2) == 0;
        etoile.SetActive(Random.Range(0, 2) == 0);
	}

    void Objectif()
    {

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Coupe()
    {
        if (estCoupe)
        {
            return;
        }

        estCoupe = true;
        FilsVerticaux filsVert = transform.parent.GetComponent<FilsVerticaux>();
        if (aCouper)
        {
            filsVert.Verif();
        }
        else
        {
            filsVert.Faute();
        }
    }

    public bool Complet()
    {
        return !aCouper || (aCouper && estCoupe);
    }
}
