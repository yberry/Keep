﻿using UnityEngine;
using System.Collections.Generic;

public class Symboles : Module {

    public enum Type
    {
        QBarre, Triforce, Lambda, Cicatrice, Vaisseau, X,
        Cenvers, Euro, Disney, EtoileBlanche, Interro,
        Copyright, Couilles, KMiroir, RPasFini, SixEcrase,
        Paragraphe, BDansT, Smiley, Trident, Cendroit,
        TroisAntennes, EtoileNoire, Inegal, AE, NEnvers, Omega
    }

    public Symbole[] symboles;

    [System.Serializable]
    public struct Img
    {
        public Type type;
        public Sprite sprite;
    }

    public Img[] images;

    private const int nbColonnes = 6;
    private const int nbSymboles = 7;

    private static Type[,] colonnes
    {
        get
        {
            return new Type[,]
            {
                { Type.QBarre, Type.Triforce, Type.Lambda, Type.Cicatrice, Type.Vaisseau, Type.X, Type.Cenvers },
                { Type.Euro, Type.QBarre, Type.Cenvers, Type.Disney, Type.EtoileBlanche, Type.X, Type.Interro },
                { Type.Copyright, Type.Couilles, Type.Disney, Type.KMiroir, Type.RPasFini, Type.Lambda, Type.EtoileBlanche },
                { Type.SixEcrase, Type.Paragraphe, Type.BDansT, Type.Vaisseau, Type.KMiroir, Type.Interro, Type.Smiley },
                { Type.Trident, Type.Smiley, Type.BDansT, Type.Cendroit, Type.Paragraphe, Type.TroisAntennes, Type.EtoileNoire },
                { Type.SixEcrase, Type.Euro, Type.Inegal, Type.AE, Type.Trident, Type.NEnvers, Type.Omega },
            };
        }
    }

    private List<int> ordre;

	// Use this for initialization
	void Start () {
        int col = Random.Range(0, nbColonnes);

        List<int> numPris = new List<int>();

        for (int i = 0; i < symboles.Length; i++)
        {
            int num = Random.Range(0, nbSymboles);
            while (numPris.Contains(num))
            {
                num = Random.Range(0, nbSymboles);
            }
            symboles[i].SetSymbole(GetSprite(colonnes[col, num]));
            numPris.Add(num);
        }

        ordre = new List<int>();

        for (int i = 0; i < symboles.Length; i++)
        {
            int min = numPris[0];
            int indexMin = 0;
            for (int j = 0; j < numPris.Count; i++)
            {
                if (min > numPris[j] && !ordre.Contains(j))
                {
                    min = numPris[j];
                    indexMin = j;
                }
            }
            ordre.Add(indexMin);
        }
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
        bool valide = true;
        foreach (Symbole symbole in symboles)
        {
            valide &= symbole.Appuye;
        }
        if (valide)
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
        symboles[ordre[0]].SetSuivant();
        ordre.RemoveAt(0);
    }
}
