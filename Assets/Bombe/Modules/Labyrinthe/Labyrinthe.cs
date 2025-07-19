using UnityEngine;
using UnityEngine.UI;

public class Labyrinthe : Module {

    private const int nbColonnes = 6;
    private const int nbLignes = 6;

    [SerializeField]
    private Button flecheHaut;
    [SerializeField]
    private Button flecheGauche;
    [SerializeField]
    private Button flecheBas;
    [SerializeField]
    private Button flecheDroite;

    [SerializeField]
    private Transform place;
    [SerializeField]
    private Cell prefabCell;

    private Vector2 triangle;
    private Vector2 carre;

    private Laby laby;
    private Cell[,] cells;

    Cell CurrentCell
    {
        get
        {
            return cells[(int)carre.y, (int)carre.x];
        }
    }

    // Use this for initialization
    void Start () {

        laby = Laby.RandomLaby;
        cells = new Cell[nbLignes, nbColonnes];
        for (int i = 0; i < nbLignes; i++)
        {
            for (int j = 0; j < nbColonnes; j++)
            {
                Cell cell = Instantiate(prefabCell, place);
                cell.transform.localPosition = new Vector3(0.1f * j - 0.25f, 0.25f - 0.1f * i, 0f);
                cell.transform.localRotation = Quaternion.identity;
                cell.name = "Cell " + i.ToString() + " " + j.ToString();
                cells[i, j] = cell;
            }
        }
        foreach (Vector2 circle in laby.Circles)
        {
            cells[(int)circle.y, (int)circle.x].ShowCircle();
        }

        int tx = Random.Range(0, nbColonnes);
        int ty = Random.Range(0, nbLignes);
        triangle = new Vector2(tx, ty);
        cells[ty, tx].SetObjective();

        int cx, cy;

        do
        {
            cx = Random.Range(0, nbColonnes);
            cy = Random.Range(0, nbLignes);
        }
        while (cx == tx && cy == ty);

        carre = new Vector2(cx, cy);
        cells[cy, cx].ShowSquare(true);

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

        if (laby.HasWall(carre, Laby.Dir.Haut))
        {
            CurrentCell.ShowMur(Laby.Dir.Haut);
            Faute();
        }
        else
        {
            CurrentCell.ShowSquare(false);
            --carre.y;
            CurrentCell.ShowSquare(true);
            Verif();
        }
    }

    void Gauche()
    {
        if (carre.x == 0)
        {
            return;
        }

        if (laby.HasWall(carre, Laby.Dir.Gauche))
        {
            CurrentCell.ShowMur(Laby.Dir.Gauche);
            Faute();
        }
        else
        {
            CurrentCell.ShowSquare(false);
            --carre.x;
            CurrentCell.ShowSquare(true);
            Verif();
        }
    }

    void Bas()
    {
        if (carre.y == nbLignes - 1)
        {
            return;
        }

        if (laby.HasWall(carre, Laby.Dir.Bas))
        {
            CurrentCell.ShowMur(Laby.Dir.Bas);
            Faute();
        }
        else
        {
            CurrentCell.ShowSquare(false);
            ++carre.y;
            CurrentCell.ShowSquare(true);
            Verif();
        }
    }

    void Droite()
    {
        if (carre.x == nbColonnes - 1)
        {
            return;
        }

        if (laby.HasWall(carre, Laby.Dir.Droite))
        {
            CurrentCell.ShowMur(Laby.Dir.Droite);
            Faute();
        }
        else
        {
            CurrentCell.ShowSquare(false);
            ++carre.x;
            CurrentCell.ShowSquare(true);
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
