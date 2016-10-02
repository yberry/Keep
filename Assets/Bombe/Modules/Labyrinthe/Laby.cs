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

        public static Place mursHaut { get { return new Place(true, false, false, false); } }
        public static Place mursGauche { get { return new Place(false, true, false, false); } }
        public static Place mursBas { get { return new Place(false, false, true, false); } }
        public static Place mursDroite { get { return new Place(false, false, false, true); } }

        public static Place mursHautGauche { get { return new Place(true, true, false, false); } }
        public static Place mursHautBas { get { return new Place(true, false, true, false); } }
        public static Place mursHautDroite { get { return new Place(true, false, false, true); } }
        public static Place mursGaucheBas { get { return new Place(false, true, true, false); } }
        public static Place mursGaucheDroite { get { return new Place(false, true, false, true); } }
        public static Place mursBasDroite { get { return new Place(false, false, true, true); } }

        public static Place mursHautGaucheBas { get { return new Place(true, true, true, false); } }
        public static Place mursHautGaucheDroite { get { return new Place(true, true, false, true); } }
        public static Place mursHautBasDroite { get { return new Place(true, false, true, true); } }
        public static Place mursGaucheBasDroite { get { return new Place(false, true, true, true); } }
    }

    private static Laby[] labys
    {
        get
        {
            return new Laby[] {
                new Laby(new Place[,] {
                    {
                        new Place(false, false, true, true),
                        new Place(false, true, false, true),
                        new Place(false, true, true, false),
                        new Place(false, false, true, true),
                        new Place(false, true, false, true),
                        new Place(false, true, false, false)
                    },
                    {
                        new Place(true, false, true, false),
                        new Place(false, false, true, true),
                        new Place(true, true, false, false),
                        new Place(true, false, false, true),
                        new Place(false, true, false, true),
                        new Place(false, true, true, false)
                    },
                    {
                        new Place(false, false, false, false),
                        new Place(false, false, false, false),
                        new Place(false, false, false, false),
                        new Place(false, false, false, false),
                        new Place(false, false, false, false),
                        new Place(false, false, false, false)
                    },
                    {
                        new Place(false, false, false, false),
                        new Place(false, false, false, false),
                        new Place(false, false, false, false),
                        new Place(false, false, false, false),
                        new Place(false, false, false, false),
                        new Place(false, false, false, false)
                    },
                    {
                        new Place(false, false, false, false),
                        new Place(false, false, false, false),
                        new Place(false, false, false, false),
                        new Place(false, false, false, false),
                        new Place(false, false, false, false),
                        new Place(false, false, false, false)
                    },
                    {
                        new Place(false, false, false, false),
                        new Place(false, false, false, false),
                        new Place(false, false, false, false),
                        new Place(false, false, false, false),
                        new Place(false, false, false, false),
                        new Place(false, false, false, false)
                    }
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