using UnityEngine;

public class Knob : Needy {

    public enum Direction
    {
        Haut,
        Droite,
        Bas,
        Gauche
    }

    [Header("Knob")]
    public Transform roulette;
    public Light[] lumieres;

    private static readonly bool[][] combi = new bool[][]
    {
        new bool[] { false, false, true, false, true, true, true, true, true, true, false, true },
        new bool[] { true, false, true, false, true, false, false, true, true, false, true, true },
        new bool[] { false, false, true, false, true, true, true, true, true, true, false, true },
        new bool[] { false, false, true, false, true, true, true, true, true, true, false, true },
        new bool[] { false, false, true, false, true, true, true, true, true, true, false, true },
        new bool[] { false, false, true, false, true, true, true, true, true, true, false, true },
        new bool[] { false, false, true, false, true, true, true, true, true, true, false, true },
        new bool[] { false, false, true, false, true, true, true, true, true, true, false, true }
    };

    Direction targetDirection;
    Direction currentDirection;

    protected override void Restart()
    {
        base.Restart();

        int random = Random.Range(0, 4);

        targetDirection = (Direction)random;
        currentDirection = Direction.Haut;

        bool[] serie = combi[Random.Range(0, combi.Length)];

        for (int i = 0; i < serie.Length; i++)
        {
            lumieres[i].enabled = serie[i];
        }
    }

    protected override void Desamorce()
    {
        base.Desamorce();

        foreach (Light lumiere in lumieres)
        {
            lumiere.enabled = false;
        }
    }

    protected override void Ecoule()
    {
        if (targetDirection == currentDirection)
        {
            Desamorce();
        }
        else
        {
            Faute();
        }
    }
}
