﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Symbols : Module {

    public enum Type
    {
        QBarre, Triforce, Lambda, Cicatrice, Vaisseau, X,
        Cenvers, Euro, Disney, EtoileBlanche, Interro,
        Copyright, Couilles, KMiroir, RPasFini, SixEcrase,
        Paragraphe, BDansT, Smiley, Trident, Cendroit,
        TroisAntennes, EtoileNoire, Inegal, AE, NEnvers, Omega
    }

    [System.Serializable]
    public struct Img
    {
        public Type type;
        public Sprite sprite;
    }

    private static readonly Type[,] colonnes = new Type[,]
    {
        { Type.QBarre, Type.Triforce, Type.Lambda, Type.Cicatrice, Type.Vaisseau, Type.X, Type.Cenvers },
        { Type.Euro, Type.QBarre, Type.Cenvers, Type.Disney, Type.EtoileBlanche, Type.X, Type.Interro },
        { Type.Copyright, Type.Couilles, Type.Disney, Type.KMiroir, Type.RPasFini, Type.Lambda, Type.EtoileBlanche },
        { Type.SixEcrase, Type.Paragraphe, Type.BDansT, Type.Vaisseau, Type.KMiroir, Type.Interro, Type.Smiley },
        { Type.Trident, Type.Smiley, Type.BDansT, Type.Cendroit, Type.Paragraphe, Type.TroisAntennes, Type.EtoileNoire },
        { Type.SixEcrase, Type.Euro, Type.Inegal, Type.AE, Type.Trident, Type.NEnvers, Type.Omega }
    };

    private const int COLUMNS = 6;
    private const int SYMB_PER_COL = 7;

    [SerializeField]
    private int nb = 4;
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private Symbol prefabSymbol;
    [SerializeField]
    private Img[] images;

    private List<int> ordre;
    private Symbol[] symbols;

    // Use this for initialization
    void Start () {

        symbols = new Symbol[nb];

        int col = Random.Range(0, COLUMNS);

        List<int> numPris = new List<int>();

        for (int i = 0; i < nb; i++)
        {
            int num;

            do
            {
                num = Random.Range(0, SYMB_PER_COL);
            }
            while (numPris.Contains(num));

            symbols[i] = Instantiate(prefabSymbol, canvas.transform);
            symbols[i].SetModule(this);
            symbols[i].SetSymbol(GetSprite(colonnes[col, num]));
            numPris.Add(num);
        }

        ordre = new List<int>(Enumerable.Range(0, nb));
        ordre.Sort((x, y) => numPris[x].CompareTo(numPris[y]));

        SetSuivant();
	}

    Sprite GetSprite(Type t)
    {
        foreach (Img img in images)
        {
            if (img.type == t)
            {
                return img.sprite;
            }
        }
        return null;
    }

    public void Verif()
    {
        if (symbols.All(s => s.Appuye))
        {
            Resolu();
        }
        else
        {
            SetSuivant();
        }
    }

    void SetSuivant()
    {
        symbols[ordre[0]].SetSuivant();
        ordre.RemoveAt(0);
    }
}
