using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Password : Module {

    private static readonly string[] wordsList = new string[]
    {
        "ABOUT", "AFTER", "AGAIN", "BELOW", "COULD",
        "EVERY", "FIRST", "FOUND", "GREAT", "HOUSE",
        "LARGE", "LEARN", "NEVER", "OTHER", "PLACE",
        "PLANT", "POINT", "RIGHT", "SMALL", "SOUND",
        "SPELL", "STILL", "STUDY", "THEIR", "THERE",
        "THESE", "THING", "THINK", "THREE", "WATER",
        "WHERE", "WHICH", "WORLD", "WOULD", "WRITE"
    };

    private const string ALPHA = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const int CHOICES_PER_CHAR = 6;

    [SerializeField]
    private PasswordChar[] passwordChars;
    [SerializeField]
    private Button submit;

    private string word;

    private List<char>[] choices;
    

	// Use this for initialization
	void Start () {
        word = wordsList.RandomItem();

        choices = new List<char>[word.Length];

        do
        {
            Restart();
        }
        while (CheckMulitple());

        for (int i = 0; i < passwordChars.Length; i++)
        {
            passwordChars[i].SetLettres(choices[i].ToArray());
        }
        submit.onClick.AddListener(Verif);
	}

    void Restart()
    {
        for (int i = 0; i < word.Length; i++)
        {
            choices[i] = new List<char>(ALPHA);
            
            while (choices[i].Count > CHOICES_PER_CHAR)
            {
                int rand;
                do
                {
                    rand = Random.Range(0, choices[i].Count);
                }
                while (choices[i][rand] == word[i]);
                choices[i].RemoveAt(rand);
            }

            choices[i].Shuffle();
        }
    }

    bool CheckMulitple()
    {
        foreach (string m in wordsList)
        {
            if (m == word)
            {
                continue;
            }

            bool contains = true;
            for (int i = 0; i < m.Length; i++)
            {
                if (!choices[i].Contains(m[i]))
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
        for (int i = 0; i < word.Length; i++)
        {
            if (passwordChars[i].CurrentLettre != word[i])
            {
                Faute();
                return;
            }
        }

        Resolu();
    }
}
