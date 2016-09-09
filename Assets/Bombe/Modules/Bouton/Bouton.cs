using UnityEngine;
using UnityEngine.UI;

public class Bouton : Module {

    #region Config bouton
    private static Color[] couleurs =
    {
        Color.yellow,
        Color.white,
        Color.red,
        Color.blue
    };
    private Color couleur;

    private static string[] textes =
    {
        "DETONATE",
        "PRESS",
        "ABORT",
        "HOLD"
    };
    private string texte;

    private bool aMaintenir;

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
        bande.enabled = false;
        SetObjectif();
        temps = 0f;
        maintien = false;
    }
	
	// Update is called once per frame
	void Update () {
	    if (maintien && !lumiere.enabled)
        {
            temps += Time.deltaTime;
            if (temps >= tempsActivation)
            {
                lumiere.color = couleurs[Random.Range(0, couleurs.Length)];
                lumiere.enabled = true;
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
        if (lumiere.enabled)
        {
            if (aMaintenir)
            {
                if (lumiere.color == Color.blue)
                {
                    if (Timer.Get.HasNb("4"))
                    {
                        Resolu();
                    }
                    else
                    {
                        Faute();
                    }
                }
                else if (lumiere.color == Color.yellow)
                {
                    if (Timer.Get.HasNb("4"))
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
                    if (Timer.Get.HasNb("1"))
                    {
                        Resolu();
                    }
                    else
                    {
                        Faute();
                    }
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
        lumiere.enabled = false;
    }
}
