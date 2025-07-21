using UnityEngine;
using UnityEngine.UI;

public class Labyrinth : Module {

    private const int COLUMNS = 6;
    private const int ROWS = 6;

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

    private Vector2Int triangle;
    private Vector2Int carre;

    private Laby laby;
    private Cell[,] cells;

    Cell CurrentCell => cells[carre.y, carre.x];

    // Use this for initialization
    void Start () {

        laby = Laby.RandomLaby;
        cells = new Cell[ROWS, COLUMNS];
        for (int i = 0; i < ROWS; i++)
        {
            for (int j = 0; j < COLUMNS; j++)
            {
                Cell cell = Instantiate(prefabCell, place);
                cell.transform.localPosition = new Vector3(0.1f * j - 0.25f, 0.25f - 0.1f * i, 0f);
                cell.transform.localRotation = Quaternion.identity;
                cell.name = $"Cell {i} {j}";
                cells[i, j] = cell;
            }
        }
        foreach (Vector2 circle in laby.Circles)
        {
            cells[(int)circle.y, (int)circle.x].ShowCircle();
        }

        int tx = Random.Range(0, COLUMNS);
        int ty = Random.Range(0, ROWS);
        triangle = new Vector2Int(tx, ty);
        cells[ty, tx].SetObjective();

        int cx, cy;

        do
        {
            cx = Random.Range(0, COLUMNS);
            cy = Random.Range(0, ROWS);
        }
        while (cx == tx && cy == ty);

        carre = new Vector2Int(cx, cy);
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
        if (carre.y == ROWS - 1)
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
        if (carre.x == COLUMNS - 1)
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
