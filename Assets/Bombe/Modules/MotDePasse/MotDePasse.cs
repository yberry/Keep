using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MotDePasse : Module {

    private static readonly string[] motsPossibles = new string[]
    {
        "ABOUT", "AFTER", "AGAIN", "BELOW", "COULD",
        "EVERY", "FIRST", "FOUND", "GREAT", "HOUSE",
        "LARGE", "LEARN", "NEVER", "OTHER", "PLACE",
        "PLANT", "POINT", "RIGHT", "SMALL", "SOUND",
        "SPELL", "STILL", "STUDY", "THEIR", "THERE",
        "THESE", "THING", "THINK", "THREE", "WATER",
        "WHERE", "WHICH", "WORLD", "WOULD", "WRITE"
    };

    private const string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const int choixParLettre = 6;

    private string mot;

    private List<char>[] choixLettres;
    public Lettre[] lettres;

    public Button submit;

	// Use this for initialization
	void Start () {
        mot = motsPossibles.RandomItem();

        choixLettres = new List<char>[mot.Length];

        do
        {
            Restart();
        }
        while (CheckMulitple());

        for (int i = 0; i < lettres.Length; i++)
        {
            lettres[i].SetLettres(choixLettres[i].ToArray());
        }
        submit.onClick.AddListener(Verif);
	}

    void Restart()
    {
        for (int i = 0; i < mot.Length; i++)
        {
            choixLettres[i] = new List<char>(alpha);
            
            while (choixLettres[i].Count > choixParLettre)
            {
                int rand;
                do
                {
                    rand = Random.Range(0, choixLettres[i].Count);
                }
                while (choixLettres[i][rand] == mot[i]);
                choixLettres[i].RemoveAt(rand);
            }

            choixLettres[i].Shuffle();
        }
    }

    bool CheckMulitple()
    {
        foreach (string m in motsPossibles)
        {
            if (m == mot)
            {
                continue;
            }

            bool contains = true;
            for (int i = 0; i < m.Length; i++)
            {
                if (!choixLettres[i].Contains(m[i]))
                {
                    contains = false;
                    break;
                }
            }
            
            if (contains)
            {
                return true;
            }
        }
        return false;
    }

    void Verif()
    {
        for (int i = 0; i < mot.Length; i++)
        {
            if (lettres[i].CurrentLettre != mot[i])
            {
                Faute();
                return;
            }
        }

        Resolu();
    }
}
