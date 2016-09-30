using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Labyrinthe : Module {

    private const int nbColonnes = 6;
    private const int nbLignes = 6;

    private Vector2 triangle;
    private Vector2 carre;

    private Laby laby;

    public Button flecheHaut;
    public Button flecheGauche;
    public Button flecheBas;
    public Button flecheDroite;

    // Use this for initialization
    void Start () {

        laby = Laby.RandomLaby;

        int tx = Random.Range(0, nbLignes);
        int ty = Random.Range(0, nbColonnes);
        triangle = new Vector2(tx, ty);

        int cx = Random.Range(0, nbLignes);
        int cy = Random.Range(0, nbColonnes);
        while (cx == tx && ty == cy)
        {
            cx = Random.Range(0, nbLignes);
            cy = Random.Range(0, nbColonnes);
        }
        carre = new Vector2(cx, cy);

        flecheHaut.onClick.AddListener(Haut);
        flecheGauche.onClick.AddListener(Gauche);
        flecheBas.onClick.AddListener(Bas);
        flecheDroite.onClick.AddListener(Droite);
    }

    void Haut()
    {

    }

    void Gauche()
    {

    }

    void Bas()
    {

    }

    void Droite()
    {

    }

}
