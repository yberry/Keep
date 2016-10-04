using UnityEngine;
using System.Collections.Generic;

public class Simon : Module {

    private static Color[] couleursDispo
    {
        get
        {
            return new Color[]
            {
                Color.red,
                Color.blue,
                Color.green,
                Color.yellow
            };
        }
    }

    private int nbCombo;
    private int nbEtapes = 0;

    private List<Color> flashs;
    private List<Losange> reponse;
    private List<Losange> reponseJoueur;

    public Losange rouge;
    public Losange bleu;
    public Losange vert;
    public Losange jaune;

    // Use this for initialization
    void Start () {
        nbCombo = Random.Range(3, 6);

        flashs = new List<Color>();
        for (int i = 0; i < nbCombo; i++)
        {
            flashs.Add(couleursDispo[Random.Range(0, couleursDispo.Length)]);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Verif()
    {
        if (nbEtapes == nbCombo)
        {
            Resolu();
        }
    }
}
