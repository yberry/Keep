using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MotDePasse : Module {

    private static readonly string[] motsPossibles =
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
        mot = motsPossibles[Random.Range(0, motsPossibles.Length)];
        Debug.Log(mot);
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
        choixLettres = new List<char>[mot.Length];

        for (int i = 0; i < mot.Length; i++)
        {
            choixLettres[i] = new List<char>();
            string alph = alpha;
            bool obli = false;
            int pasObli = choixParLettre - 1;

            int index = alph.IndexOf(mot[i]);
            alph.Remove(index, 1);

            for (int j = 0; j < choixParLettre; j++)
            {
                Debug.Log(j);
                if (pasObli > 0 && Random.Range(0, 3) > 0)
                {
                    int rand = Random.Range(0, alph.Length);
                    while (choixLettres[i].Contains(alph[rand]))
                    {
                        rand = Random.Range(0, alph.Length);
                    }
                    choixLettres[i].Add(alph[rand]);
                    alph.Remove(rand, 1);

                    pasObli--;
                }
                else if (!obli)
                {
                    choixLettres[i].Add(mot[i]);
                    obli = true;
                }
                else
                {
                    j--;
                }
            }
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
                contains &= choixLettres[i].Contains(m[i]);
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
        bool verif = true;
        for (int i = 0; i < mot.Length; i++)
        {
            verif &= lettres[i].currentLettre == mot[i];
        }

        if (verif)
        {
            Resolu();
        }
        else
        {
            Faute();
        }
    }
}
