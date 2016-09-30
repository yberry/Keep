using UnityEngine;

public class Laby
{
    struct Place
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

    private Place[,] places;

    public Laby()
    {

    }

    private static readonly Laby[] labys;

    public static Laby RandomLaby()
    {
        return labys[Random.Range(0, labys.Length)];
    }
}