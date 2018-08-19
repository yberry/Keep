using UnityEngine;
using UnityEngine.UI;

public class Bouton : Module {

    #region Config bouton
    private static readonly Color[] couleurs = new Color[]
    {
        Color.yellow,
        Color.white,
        Color.red,
        Color.blue
    };
    private Color couleur;

    private static readonly string[] textes = new string[]
    {
        "DETONATE",
        "PRESS",
        "ABORT",
        "HOLD"
    };
    private string texte;

    private bool aMaintenir;
    private char chiffre;

    public Transform bouton;
    public Text affichage;
    public Light bande;
    #endregion

    private const float tempsActivation = 0.5f;
    private float temps;
    private bool maintien;

    // Use this for initialization
    void Start () {
        couleur = couleurs.RandomItem();
        texte = textes.RandomItem();
        bouton.GetComponent<Renderer>().material.color = couleur;
        affichage.text = texte;
        SetObjectif();
        bande.enabled = false;
        temps = 0f;
        maintien = false;
    }
	
	// Update is called once per frame
	void Update () {
	    if (maintien && !bande.enabled)
        {
            temps += Time.deltaTime;
            if (temps >= tempsActivation)
            {
                bande.color = couleurs.RandomItem();
                if (bande.color == Color.blue)
                {
                    chiffre = '4';
                }
                else if (bande.color == Color.yellow)
                {
                    chiffre = '5';
                }
                else
                {
                    chiffre = '1';
                }
                bande.enabled = true;
            }
        }
	}

    void SetObjectif()
    {
        Bombe bombe = Bombe.Instance;

        if (couleur == Color.blue && texte == "ABORT")
        {
            aMaintenir = true;
        }
        else if (bombe.NbPiles > 1 && texte == "DETONATE")
        {
            aMaintenir = false;
        }
        else if (couleur == Color.white && bombe.HasLightIndic("CAR"))
        {
            aMaintenir = true;
        }
        else if (bombe.NbPiles > 2 && bombe.HasLightIndic("FRK"))
        {
            aMaintenir = false;
        }
        else if (couleur == Color.yellow)
        {
            aMaintenir = true;
        }
        else if (couleur == Color.red && texte == "HOLD")
        {
            aMaintenir = false;
        }
        else
        {
            aMaintenir = true;
        }
    }

    public void Maintien()
    {
        maintien = true;
    }

    public void Relache()
    {
        if (bande.enabled)
        {
            if (aMaintenir && Timer.Instance.HasNb(chiffre))
            {
                Resolu();
            }
            else
            {
                Faute();
            }
        }
        else
        {
            if (aMaintenir)
            {
                Faute();
            }
            else
            {
                Resolu();
            }
        }
        temps = 0f;
        maintien = false;
        bande.enabled = false;
    }
}
