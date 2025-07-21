using UnityEngine;

public class Laby
{
    [System.Flags]
    public enum Dir
    {
        Haut = 8,
        Gauche = 4,
        Bas = 2,
        Droite = 1
    }

    private static readonly Laby[] labys = new Laby[]
    {
        new Laby(new byte[,] {
            { 12, 10, 9, 12, 10, 11 },
            { 5, 12, 3, 6, 10, 9 },
            { 5, 6, 9, 12, 10, 1 },
            { 5, 14, 2, 3, 14, 1 },
            { 4, 10, 9, 12, 11, 5 },
            { 6, 11, 6, 3, 14, 3 }
        }, new Vector2(0f, 1f), new Vector2(5f, 2f)),

        new Laby(new byte[,] {
            { 14, 8, 11, 12, 8, 11 },
            { 12, 3, 12, 3, 6, 9 },
            { 5, 12, 3, 12, 10, 1 },
            { 4, 3, 12, 3, 13, 5 },
            { 5, 13, 5, 12, 3, 5 },
            { 7, 6, 3, 6, 10, 3 }
        }, new Vector2(1f, 3f), new Vector2(4f, 1f)),

        new Laby(new byte[,] {
            { 12, 10, 9, 13, 12, 9 },
            { 7, 13, 5, 6, 3, 5 },
            { 12, 1, 5, 12, 9, 5 },
            { 5, 5, 5, 5, 5, 5 },
            { 5, 6, 3, 5, 5, 5 },
            { 6, 10, 10, 3, 6, 3 }
        }, new Vector2(3f, 3f), new Vector2(5f, 3f)),

        new Laby(new byte[,] {
            { 12, 9, 14, 10, 10, 9 },
            { 5, 5, 12, 10, 10, 1 },
            { 5, 6, 3, 12, 11, 5 },
            { 5, 14, 10, 2, 10, 1 },
            { 4, 10, 10, 10, 9, 5 },
            { 6, 10, 11, 14, 3, 7 }
        }, new Vector2(0f, 0f), new Vector2(0f, 3f)),

        new Laby(new byte[,] {
            { 14, 10, 10, 10, 8, 9 },
            { 12, 10, 10, 8, 3, 7 },
            { 4, 9, 14, 3, 12, 9 },
            { 5, 6, 10, 9, 7, 5 },
            { 5, 12, 10, 2, 11, 5 },
            { 7, 6, 10, 10, 10, 3 }
        }, new Vector2(3f, 5f), new Vector2(4f, 2f)),

        new Laby(new byte[,] {
            { 13, 12, 9, 14, 8, 9 },
            { 5, 5, 5, 12, 3, 5 },
            { 4, 3, 7, 5, 12, 3 },
            { 6, 9, 12, 1, 5, 13 },
            { 12, 3, 7, 5, 6, 1 },
            { 6, 10, 10, 3, 14, 3 }
        }, new Vector2(2f, 4f), new Vector2(4f, 0f)),

        new Laby(new byte[,] {
            { 12, 10, 10, 9, 12, 9 },
            { 5, 12, 11, 6, 3, 5 },
            { 6, 3, 12, 11, 12, 3 },
            { 12, 9, 4, 10, 3, 13 },
            { 5, 7, 6, 10, 9, 5 },
            { 6, 10, 10, 10, 2, 3 }
        }, new Vector2(1f, 0f), new Vector2(1f, 5f)),

        new Laby(new byte[,] {
            { 13, 12, 10, 9, 12, 9 },
            { 4, 2, 11, 6, 3, 5 },
            { 5, 12, 10, 10, 9, 5 },
            { 5, 6, 9, 14, 2, 3 },
            { 5, 13, 6, 10, 10, 11 },
            { 6, 2, 10, 10, 10, 11 }
        }, new Vector2(3f, 0f), new Vector2(2f, 3f)),

        new Laby(new byte[,] {
            { 13, 12, 10, 10, 8, 9 },
            { 5, 5, 12, 11, 5, 5 },
            { 4, 2, 3, 12, 3, 5 },
            { 5, 5, 12, 3, 14, 1 },
            { 5, 5, 5, 12, 9, 7 },
            { 6, 3, 6, 3, 6, 11 }
        }, new Vector2(0f, 4f), new Vector2(2f, 1f))
    };

    private byte[,] places;
    private Vector2 rond1;
    private Vector2 rond2;

    public static Laby RandomLaby => labys.RandomItem();

    public Vector2[] Circles => new Vector2[] { rond1, rond2 };

    public Laby(byte[,] pl, Vector2 r1, Vector2 r2)
    {
        places = pl;
        rond1 = r1;
        rond2 = r2;
    }

    public bool HasWall(Vector2 pos, Dir dir)
    {
        byte pl = places[(int)pos.y, (int)pos.x];

        return (pl & (int)dir) != 0;
    }
}