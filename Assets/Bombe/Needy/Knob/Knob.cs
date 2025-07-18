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

    private static readonly short[] combi = new short[]
    {
        0x2fd, 0xa9b,
        0x67d, 0xa91,
        0x0a7, 0x086,
        0xbfa, 0xb3a
    };

    Direction targetDirection;
    Direction currentDirection;

    protected override void Restart()
    {
        base.Restart();

        int random = Random.Range(0, 8);

        targetDirection = (Direction)(random >> 1);
        currentDirection = Direction.Haut;

        short serie = combi[random];
        int and = 1 << (lumieres.Length - 1);

        for (int i = 0; i < lumieres.Length; i++)
        {
            lumieres[i].enabled = (serie & and) != 0;
            and >>= 1;
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
