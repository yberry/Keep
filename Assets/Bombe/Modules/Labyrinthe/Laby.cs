using UnityEngine;

public class Laby
{
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

    private static Laby[] labys
    {
        get
        {
            return new Laby[] {
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
                    { mursGaucheDroite, mursGaucheBas, mursHautDroite, mursHautGauche, mursHautBas, mursDroite },
                    { mursGaucheDroite, mursHautGaucheBas, mursBas, mursBasDroite, mursHautGaucheBas, mursDroite },
                    { mursGauche, mursHautBas, mursHautDroite, mursHautGauche, mursHautBasDroite, mursGaucheDroite },
                    { mursGaucheBas, mursHautBasDroite, mursGaucheBas, mursBasDroite, mursHautGaucheBas, mursBasDroite }
                }),

                new Laby(new Place[,] {
                    { mursHautGauche, mursHautBas, mursHautDroite, mursHautGauche, mursHautBas, mursHautBasDroite },
                    { mursGaucheDroite, mursHautGauche, mursBasDroite, mursGaucheBas, mursHautBas, mursHautDroite },
                    { mursGaucheDroite, mursGaucheBas, mursHautDroite, mursHautGauche, mursHautBas, mursDroite },
                    { mursGaucheDroite, mursHautGaucheBas, mursBas, mursBasDroite, mursHautGaucheBas, mursDroite },
                    { mursGauche, mursHautBas, mursHautDroite, mursHautGauche, mursHautBasDroite, mursGaucheDroite },
                    { mursGaucheBas, mursHautBasDroite, mursGaucheBas, mursBasDroite, mursHautGaucheBas, mursBasDroite }
                }),

                new Laby(new Place[,] {
                    { mursHautGauche, mursHautBas, mursHautDroite, mursHautGauche, mursHautBas, mursHautBasDroite },
                    { mursGaucheDroite, mursHautGauche, mursBasDroite, mursGaucheBas, mursHautBas, mursHautDroite },
                    { mursGaucheDroite, mursGaucheBas, mursHautDroite, mursHautGauche, mursHautBas, mursDroite },
                    { mursGaucheDroite, mursHautGaucheBas, mursBas, mursBasDroite, mursHautGaucheBas, mursDroite },
                    { mursGauche, mursHautBas, mursHautDroite, mursHautGauche, mursHautBasDroite, mursGaucheDroite },
                    { mursGaucheBas, mursHautBasDroite, mursGaucheBas, mursBasDroite, mursHautGaucheBas, mursBasDroite }
                }),

                new Laby(new Place[,] {
                    { mursHautGauche, mursHautBas, mursHautDroite, mursHautGauche, mursHautBas, mursHautBasDroite },
                    { mursGaucheDroite, mursHautGauche, mursBasDroite, mursGaucheBas, mursHautBas, mursHautDroite },
                    { mursGaucheDroite, mursGaucheBas, mursHautDroite, mursHautGauche, mursHautBas, mursDroite },
                    { mursGaucheDroite, mursHautGaucheBas, mursBas, mursBasDroite, mursHautGaucheBas, mursDroite },
                    { mursGauche, mursHautBas, mursHautDroite, mursHautGauche, mursHautBasDroite, mursGaucheDroite },
                    { mursGaucheBas, mursHautBasDroite, mursGaucheBas, mursBasDroite, mursHautGaucheBas, mursBasDroite }
                }),

                new Laby(new Place[,] {
                    { mursHautGauche, mursHautBas, mursHautDroite, mursHautGauche, mursHautBas, mursHautBasDroite },
                    { mursGaucheDroite, mursHautGauche, mursBasDroite, mursGaucheBas, mursHautBas, mursHautDroite },
                    { mursGaucheDroite, mursGaucheBas, mursHautDroite, mursHautGauche, mursHautBas, mursDroite },
                    { mursGaucheDroite, mursHautGaucheBas, mursBas, mursBasDroite, mursHautGaucheBas, mursDroite },
                    { mursGauche, mursHautBas, mursHautDroite, mursHautGauche, mursHautBasDroite, mursGaucheDroite },
                    { mursGaucheBas, mursHautBasDroite, mursGaucheBas, mursBasDroite, mursHautGaucheBas, mursBasDroite }
                })
            };
        }
    }

    public static Laby RandomLaby
    {
        get
        {
            return labys[Random.Range(0, labys.Length)];
        }        
    }

    private Place[,] places;
    private Vector2 rond1;
    private Vector2 rond2;

    public Laby(Place[,] pl)
    {
        places = pl;
    }
}