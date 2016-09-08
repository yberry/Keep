using UnityEngine;
using System.Collections;

public class Bouton : Module {

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

    public Light bande;

	// Use this for initialization
	void Start () {
        couleur = couleurs[Random.Range(0, couleurs.Length)];
        texte = textes[Random.Range(0, textes.Length)];
        bande.enabled = false;
        SetObjectif();
    }
	
	// Update is called once per frame
	void Update () {
	
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
}
