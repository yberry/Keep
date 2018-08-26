using UnityEngine;
using UnityEngine.UI;

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

        int tx = Random.Range(0, nbColonnes);
        int ty = Random.Range(0, nbLignes);
        triangle = new Vector2(tx, ty);

        int cx, cy;

        do
        {
            cx = Random.Range(0, nbColonnes);
            cy = Random.Range(0, nbLignes);
        }
        while (cx == tx && cy == ty);

        carre = new Vector2(cx, cy);

        flecheHaut.onClick.AddListener(Haut);
        flecheGauche.onClick.AddListener(Gauche);
        flecheBas.onClick.AddListener(Bas);
        flecheDroite.onClick.AddListener(Droite);
    }

    void Haut()
    {
        if (carre.y == 0)
        {
            return;
        }

        if (laby.HasMur(carre, Laby.Dir.Haut))
        {
            Faute();
        }
        else
        {
            --carre.y;
            Verif();
        }
    }

    void Gauche()
    {
        if (carre.x == 0)
        {
            return;
        }

        if (laby.HasMur(carre, Laby.Dir.Gauche))
        {
            Faute();
        }
        else
        {
            --carre.x;
            Verif();
        }
    }

    void Bas()
    {
        if (carre.y == nbLignes - 1)
        {
            return;
        }

        if (laby.HasMur(carre, Laby.Dir.Bas))
        {
            Faute();
        }
        else
        {
            ++carre.y;
            Verif();
        }
    }

    void Droite()
    {
        if (carre.x == nbColonnes - 1)
        {
            return;
        }

        if (laby.HasMur(carre, Laby.Dir.Droite))
        {
            Faute();
        }
        else
        {
            ++carre.x;
            Verif();
        }
    }

    void Verif()
    {
        if (carre == triangle)
        {
            Resolu();
        }
    }
}
