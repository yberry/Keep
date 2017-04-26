using UnityEngine;

public class Laby
{
    public enum Dir
    {
        Haut,
        Gauche,
        Bas,
        Droite
    }

    public struct Place
    {
        public bool murHaut;
        public bool murGauche;
        public bool murBas;
        public bool murDroite;
        public Place(bool haut, bool gauche, bool bas, bool droite)
        {
            murHaut = haut;
            murGauche = gauche;
            murBas = bas;
            murDroite = droite;
        }
    }

    private static Place mursHaut { get { return new Place(true, false, false, false); } }
    private static Place mursGauche { get { return new Place(false, true, false, false); } }
    private static Place mursBas { get { return new Place(false, false, true, false); } }
    private static Place mursDroite { get { return new Place(false, false, false, true); } }

    private static Place mursHautGauche { get { return new Place(true, true, false, false); } }
    private static Place mursHautBas { get { return new Place(true, false, true, false); } }
    private static Place mursHautDroite { get { return new Place(true, false, false, true); } }
    private static Place mursGaucheBas { get { return new Place(false, true, true, false); } }
    private static Place mursGaucheDroite { get { return new Place(false, true, false, true); } }
    private static Place mursBasDroite { get { return new Place(false, false, true, true); } }

    private static Place mursHautGaucheBas { get { return new Place(true, true, true, false); } }
    private static Place mursHautGaucheDroite { get { return new Place(true, true, false, true); } }
    private static Place mursHautBasDroite { get { return new Place(true, false, true, true); } }
    private static Place mursGaucheBasDroite { get { return new Place(false, true, true, true); } }

    private static readonly Laby[] labys = new Laby[]
    {
        new Laby(new Place[,] {
            { mursHautGauche, mursHautBas, mursHautDroite, mursHautGauche, mursHautBas, mursHautBasDroite },
            { mursGaucheDroite, mursHautGauche, mursBasDroite, mursGaucheBas, mursHautBas, mursHautDroite },
            { mursGaucheDroite, mursGaucheBas, mursHautDroite, mursHautGauche, mursHautBas, mursDroite },
            { mursGaucheDroite, mursHautGaucheBas, mursBas, mursBasDroite, mursHautGaucheBas, mursDroite },
            { mursGauche, mursHautBas, mursHautDroite, mursHautGauche, mursHautBasDroite, mursGaucheDroite },
            { mursGaucheBas, mursHautBasDroite, mursGaucheBas, mursBasDroite, mursHautGaucheBas, mursBasDroite }
        }),

        new Laby(new Place[,] {
            { mursHautGaucheBas, mursHaut, mursHautBasDroite, mursHautGauche, mursHaut, mursHautBasDroite },
            { mursHautGauche, mursBasDroite, mursHautGauche, mursBasDroite, mursGaucheBas, mursHautDroite },
            { mursGaucheDroite, mursHautGauche, mursBasDroite, mursHautGauche, mursHautBas, mursDroite },
            { mursGauche, mursBasDroite, mursHautGauche, mursBasDroite, mursHautGaucheDroite, mursGaucheDroite },
            { mursGaucheDroite, mursHautGaucheDroite, mursGaucheDroite, mursHautGauche, mursBasDroite, mursGaucheDroite },
            { mursGaucheBasDroite, mursGaucheBas, mursBasDroite, mursGaucheBas, mursHautBas, mursBasDroite }
        }),

        new Laby(new Place[,] {
            { mursHautGauche, mursHautBas, mursHautDroite, mursHautGaucheDroite, mursHautGauche, mursHautDroite },
            { mursGaucheBasDroite, mursHautGaucheDroite, mursGaucheDroite, mursGaucheBas, mursBasDroite, mursGaucheDroite },
            { mursHautGauche, mursDroite, mursGaucheDroite, mursHautGauche, mursHautDroite, mursGaucheDroite },
            { mursGaucheDroite, mursGaucheDroite, mursGaucheDroite, mursGaucheDroite, mursGaucheDroite, mursGaucheDroite },
            { mursGaucheDroite, mursGaucheBas, mursBasDroite, mursGaucheDroite, mursGaucheDroite, mursGaucheDroite },
            { mursGaucheBas, mursHautBas, mursHautBas, mursBasDroite, mursGaucheBas, mursBasDroite }
        }),

        new Laby(new Place[,] {
            { mursHautGauche, mursHautDroite, mursHautGaucheBas, mursHautBas, mursHautBas, mursHautDroite },
            { mursGaucheDroite, mursGaucheDroite, mursHautGauche, mursHautBas, mursHautBas, mursDroite },
            { mursGaucheDroite, mursGaucheBas, mursBasDroite, mursHautGauche, mursHautBasDroite, mursGaucheDroite },
            { mursGaucheDroite, mursHautGaucheBas, mursHautBas, mursBas, mursHautBas, mursDroite },
            { mursGauche, mursHautBas, mursHautBas, mursHautBas, mursHautDroite, mursGaucheDroite },
            { mursGaucheBas, mursHautBas, mursHautBasDroite, mursHautGaucheBas, mursBasDroite, mursGaucheBasDroite }
        }),

        new Laby(new Place[,] {
            { mursHautGaucheBas, mursHautBas, mursHautBas, mursHautBas, mursHaut, mursHautDroite },
            { mursHautGauche, mursHautBas, mursHautBas, mursHaut, mursBasDroite, mursGaucheBasDroite },
            { mursGauche, mursHautDroite, mursHautGaucheBas, mursBasDroite, mursHautGauche, mursHautDroite },
            { mursGaucheDroite, mursGaucheBas, mursHautBas, mursHautDroite, mursGaucheBasDroite, mursGaucheDroite },
            { mursGaucheDroite, mursHautGauche, mursHautBas, mursBas, mursHautBasDroite, mursGaucheDroite },
            { mursGaucheBasDroite, mursGaucheBas, mursHautBas, mursHautBas, mursHautBas, mursBasDroite }
        }),

        new Laby(new Place[,] {
            { mursHautGaucheDroite, mursHautGauche, mursHautDroite, mursHautGaucheBas, mursHaut, mursHautDroite },
            { mursGaucheDroite, mursGaucheDroite, mursGaucheDroite, mursHautGauche, mursBasDroite, mursGaucheDroite },
            { mursGauche, mursBasDroite, mursGaucheBasDroite, mursGaucheDroite, mursHautGauche, mursBasDroite },
            { mursGaucheBas, mursHautDroite, mursHautGauche, mursDroite, mursGaucheDroite, mursHautGaucheDroite },
            { mursHautGauche, mursBasDroite, mursGaucheBasDroite, mursGaucheDroite, mursGaucheBas, mursDroite },
            { mursGaucheBas, mursHautBas, mursHautBas, mursBasDroite, mursHautGaucheBas, mursBasDroite }
        }),

        new Laby(new Place[,] {
            { mursHautGauche, mursHautBas, mursHautBas, mursHautDroite, mursHautGauche, mursHautDroite },
            { mursGaucheDroite, mursHautGauche, mursHautBasDroite, mursGaucheBas, mursBasDroite, mursGaucheDroite },
            { mursGaucheBas, mursBasDroite, mursHautGauche, mursHautBasDroite, mursHautGauche, mursBasDroite },
            { mursHautGauche, mursHautDroite, mursGauche, mursHautBas, mursBasDroite, mursHautGaucheDroite },
            { mursGaucheDroite, mursGaucheBasDroite, mursGaucheBas, mursHautBas, mursHautDroite, mursGaucheDroite },
            { mursGaucheBas, mursHautBas, mursHautBas, mursHautBas, mursBas, mursBasDroite }
        }),

        new Laby(new Place[,] {
            { mursHautGaucheDroite, mursHautGauche, mursHautBas, mursHautDroite, mursHautGauche, mursHautDroite },
            { mursGauche, mursBas, mursHautBasDroite, mursGaucheBas, mursBasDroite, mursGaucheDroite },
            { mursGaucheDroite, mursHautGauche, mursHautBas, mursHautBas, mursHautDroite, mursGaucheDroite },
            { mursGaucheDroite, mursGaucheBas, mursHautDroite, mursHautGaucheBas, mursBas, mursBasDroite },
            { mursGaucheDroite, mursHautGaucheDroite, mursGaucheBas, mursHautBas, mursHautBas, mursHautBasDroite },
            { mursGaucheBas, mursBas, mursHautBas, mursHautBas, mursHautBas, mursHautBasDroite }
        }),

        new Laby(new Place[,] {
            { mursHautGaucheDroite, mursHautGauche, mursHautBas, mursHautBas, mursHaut, mursHautDroite },
            { mursGaucheDroite, mursGaucheDroite, mursHautGauche, mursHautBasDroite, mursGaucheDroite, mursGaucheDroite },
            { mursGauche, mursBas, mursBasDroite, mursHautGauche, mursBasDroite, mursGaucheDroite },
            { mursGaucheDroite, mursGaucheDroite, mursHautGauche, mursBasDroite, mursHautGaucheBas, mursDroite },
            { mursGaucheDroite, mursGaucheDroite, mursGaucheDroite, mursHautGauche, mursHautDroite, mursGaucheBasDroite },
            { mursGaucheBas, mursBasDroite, mursGaucheBas, mursBasDroite, mursGaucheBas, mursHautBasDroite }
        })
    };

    public static Laby RandomLaby
    {
        get
        {
            return labys.RandomItem();
        }        
    }

    private Place[,] places;
    private Vector2 rond1;
    private Vector2 rond2;

    public Laby(Place[,] pl)
    {
        places = pl;
    }

    public bool HasMur(Vector2 pos, Dir dir)
    {
        Place pl = places[(int)pos.x, (int)pos.y];

        switch (dir)
        {
            case Dir.Haut:
                return pl.murHaut;

            case Dir.Gauche:
                return pl.murGauche;

            case Dir.Bas:
                return pl.murBas;

            case Dir.Droite:
                return pl.murDroite;

            default:
                return false;
        }
    }
}