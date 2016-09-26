using UnityEngine;
using UnityEngine.UI;

public class Bouton : Module {

    #region Config bouton
    private static readonly Color[] couleurs =
    {
        Color.yellow,
        Color.white,
        Color.red,
        Color.blue
    };
    private Color couleur;

    private static readonly string[] textes =
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
        couleur = couleurs[Random.Range(0, couleurs.Length)];
        texte = textes[Random.Range(0, textes.Length)];
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
                bande.color = couleurs[Random.Range(0, couleurs.Length)];
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
        if (couleur == Color.blue && texte == "ABORT")
        {
            aMaintenir = true;
        }
        else if (Bombe.Get.NbPiles > 1 && texte == "DETONATE")
        {
            aMaintenir = false;
        }
        else if (couleur == Color.white && Bombe.Get.HasLightIndic("CAR"))
        {
            aMaintenir = true;
        }
        else if (Bombe.Get.NbPiles > 2 && Bombe.Get.HasLightIndic("FRK"))
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
            if (aMaintenir)
            {
                if (Timer.Get.HasNb(chiffre))
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
