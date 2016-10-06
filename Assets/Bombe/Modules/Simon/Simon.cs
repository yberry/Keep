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
    private List<Losange> signaux;
    private List<Losange> reponse;
    private List<Losange> reponseJoueur;

    public Losange rouge;
    public Losange bleu;
    public Losange vert;
    public Losange jaune;

    // Use this for initialization
    void Start () {
        nbCombo = Random.Range(3, 6);

        rouge.SetModule(this);
        bleu.SetModule(this);
        vert.SetModule(this);
        jaune.SetModule(this);

        flashs = new List<Color>();
        signaux = new List<Losange>();
        reponse = new List<Losange>();
        reponseJoueur = new List<Losange>();
        AddColor();
	}

    void AddColor()
    {
        Color color = couleursDispo[Random.Range(0, couleursDispo.Length)];
        flashs.Add(color);

        if (color == Color.red)
        {
            signaux.Add(rouge);
        }
        else if (color == Color.blue)
        {
            signaux.Add(bleu);
        }
        else if (color == Color.green)
        {
            signaux.Add(vert);
        }
        else if (color == Color.yellow)
        {
            signaux.Add(jaune);
        }
        CheckReponse();
    }

    public void CheckReponse()
    {
        reponse.Clear();
        bool voyelle = Bombe.Get.voyelle;
        int fautes = Bombe.Get.Erreurs;

        foreach (Color color in flashs)
        {
            if (color == Color.red)
            {
                switch (fautes)
                {
                    case 0:
                        reponse.Add(bleu);
                        break;

                    case 1:
                        reponse.Add(voyelle ? jaune : rouge);
                        break;

                    case 2:
                        reponse.Add(voyelle ? vert : jaune);
                        break;
                }
            }
            else if (color == Color.blue)
            {
                switch (fautes)
                {
                    case 0:
                        reponse.Add(voyelle ? rouge : jaune);
                        break;

                    case 1:
                        reponse.Add(voyelle ? vert : bleu);
                        break;

                    case 2:
                        reponse.Add(voyelle ? rouge : vert);
                        break;
                }
            }
            else if (color == Color.green)
            {
                switch (fautes)
                {
                    case 0:
                        reponse.Add(voyelle ? jaune : vert);
                        break;

                    case 1:
                        reponse.Add(voyelle ? bleu : jaune);
                        break;

                    case 2:
                        reponse.Add(voyelle ? jaune : bleu);
                        break;
                }
            }
            else if (color == Color.yellow)
            {
                switch (fautes)
                {
                    case 0:
                        reponse.Add(voyelle ? vert : rouge);
                        break;

                    case 1:
                        reponse.Add(voyelle ? rouge : vert);
                        break;

                    case 2:
                        reponse.Add(voyelle ? bleu : rouge);
                        break;
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Verif()
    {
        if (nbEtapes == nbCombo)
        {
            Resolu();
        }
        else
        {
            AddColor();
        }
    }

    public override void Faute()
    {
        base.Faute();
        CheckReponse();
    }
}
