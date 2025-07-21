using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WhoFirst : Module {

    private static readonly string[][] englishTitles = new string[][]
    {
        new string[] { "UR" },
        new string[] { "FIRST", "OKAY", "C" },
        new string[] { "YES", "NOTHING", "LED", "THEY ARE" },
        new string[] { "BLANK", "READ", "RED", "YOU", "YOUR", "YOU'RE", "THEIR" },
        new string[] { "", "REED", "LEED", "THEY'RE" },
        new string[] { "DISPLAY", "SAYS", "NO", "LEAD", "HOLD ON", "YOU ARE", "THERE", "SEE", "CEE" }
    };

    private static readonly string[][] frenchTitles = new string[][]
    {
        new string[] { "THON" },
        new string[] { "PREMIER", "OK", "C" },
        new string[] { "OUI", "RIEN", "MOT", "VERT" },
        new string[] { "VIDE", "BOUGE", "ROUGE", "TES", "TON", "TONS", "VERS" },
        new string[] { "", "AU", "EAU", "HAUT" },
        new string[] { "VERRE", "MOTS", "NON", "MAUX", "ATTENDS", "T'ES", "TU ES", "C'EST", "VER" }
    };

    private static readonly string[] englishKeywords = new string[] {
        "READY", "FIRST", "NO", "BLANK", "NOTHING", "YES", "WHAT", "UHHH", "LEFT", "RIGHT", "MIDDLE", "OKAY", "WAIT", "PRESS",
        "YOU", "YOU ARE", "YOUR", "YOU'RE", "UR", "U", "UH HUH", "UH UH", "WHAT?", "DONE", "NEXT", "HOLD", "SURE", "LIKE"
    };

    private static readonly string[] frenchKeywords = new string[] {
        "PRÊT", "PREMIER", "NON", "VIDE", "RIEN", "OUI", "EUX", "EUHHH", "GAUCHE", "DROITE", "MILIEU", "E", "ATTENDS", "APPUIE",
        "TOI", "THON", "TON", "TONS", "T'ES", "TES", "AVANT", "QUOI", "QUOI ?", "FAIT", "SUIVANT", "MAINTIENS", "OK", "COMME"
    };

    private static readonly int[][] nextWords = new int[][]
    {
        new int[] { 5, 11, 6, 10, 8, 13, 9, 3, 0, 2, 1, 7, 4, 12 },
        new int[] { 8, 11, 5, 10, 2, 9, 4, 7, 12, 0, 3, 6, 13, 1 },
        new int[] { 3, 7, 12, 1, 6, 0, 9, 5, 4, 8, 13, 11, 2, 10 },
        new int[] { 12, 9, 11, 10, 3, 13, 0, 4, 2, 6, 8, 7, 5, 1 },
        new int[] { 7, 9, 11, 10, 5, 3, 2, 13, 8, 6, 12, 1, 4, 0 },
        new int[] { 11, 9, 7, 10, 1, 6, 13, 0, 4, 5, 8, 3, 2, 12 },
        new int[] { 7, 6, 8, 4, 0, 3, 10, 2, 11, 1, 12, 5, 13, 9 },
        new int[] { 0, 4, 8, 6, 11, 5, 9, 2, 13, 3, 7, 10, 12, 1 },
        new int[] { 9, 8, 1, 2, 10, 5, 3, 6, 7, 12, 13, 0, 11, 4 },
        new int[] { 5, 4, 0, 13, 2, 12, 6, 9, 10, 8, 7, 3, 11, 1 },
        new int[] { 3, 0, 11, 6, 4, 13, 2, 12, 8, 10, 9, 1, 7, 5 },
        new int[] { 10, 2, 1, 5, 7, 4, 12, 11, 8, 0, 3, 13, 6, 9 },
        new int[] { 7, 2, 3, 11, 5, 8, 1, 13, 6, 12, 4, 0, 9, 10 },
        new int[] { 9, 10, 5, 0, 13, 11, 4, 7, 3, 8, 1, 6, 2, 12 },
        new int[] { 26, 15, 16, 17, 24, 20, 18, 25, 22, 14, 21, 27, 23, 19 },
        new int[] { 16, 24, 27, 20, 22, 23, 21, 25, 14, 19, 17, 26, 18, 15 },
        new int[] { 21, 15, 20, 16, 24, 18, 26, 19, 17, 14, 22, 25, 27, 23 },
        new int[] { 14, 17, 18, 24, 21, 15, 19, 16, 22, 20, 26, 23, 27, 25 },
        new int[] { 23, 19, 18, 20, 22, 26, 16, 25, 17, 27, 24, 21, 15, 14 },
        new int[] { 20, 26, 24, 22, 17, 18, 21, 23, 19, 14, 27, 25, 15, 16 },
        new int[] { 20, 16, 15, 14, 23, 25, 21, 24, 26, 27, 17, 18, 19, 22 },
        new int[] { 18, 19, 15, 17, 24, 21, 23, 14, 20, 27, 16, 26, 25, 22 },
        new int[] { 14, 25, 17, 16, 19, 23, 21, 27, 15, 20, 18, 24, 22, 26 },
        new int[] { 26, 20, 24, 22, 16, 18, 17, 25, 27, 14, 19, 15, 21, 23 },
        new int[] { 22, 20, 21, 16, 25, 26, 24, 27, 23, 15, 18, 17, 19, 14 },
        new int[] { 15, 19, 23, 21, 14, 18, 26, 22, 17, 24, 25, 20, 16, 27 },
        new int[] { 15, 23, 27, 17, 14, 25, 20, 18, 26, 19, 22, 24, 16, 21 },
        new int[] { 17, 24, 19, 18, 25, 23, 21, 22, 20, 14, 27, 26, 15, 16 }
    };

    private const int objReussites = 3;

    [SerializeField]
    private TMP_Text keywordDisplay;
    [SerializeField]
    private WhoFirstWord[] words;
    [SerializeField]
    private Light[] successes;

    private int nbReussites = 0;

    private int numCle;

	// Use this for initialization
	void Start () {
        foreach (WhoFirstWord mot in words)
        {
            mot.SetModule(this);
        }
        foreach (Light light in successes)
        {
            light.color = Color.green;
            light.enabled = false;
        }
        Restart();
	}

    void Restart()
    {
        numCle = Random.Range(0, words.Length);

        keywordDisplay.text = englishTitles[numCle].RandomItem();
        foreach (WhoFirstWord mot in words)
        {
            mot.Restart();
        }

        int rand = Random.Range(0, englishKeywords.Length);

        Dictionary<int, int> indicesMis = new Dictionary<int, int>();

        List<int> indicesLigne = new List<int>(nextWords[rand]);
        int indexCle = indicesLigne.IndexOf(rand);

        int min = indexCle;

        for (int i = 0; i < words.Length; i++)
        {
            if (i != numCle)
            {
                int index;

                do
                {
                    index = Random.Range(0, indicesLigne.Count);
                }
                while (index == indexCle || indicesMis.ContainsValue(index));

                indicesMis.Add(indicesLigne[index], index);
                if (min > index)
                {
                    min = index;
                }
            }
            else
            {
                indicesMis.Add(rand, indexCle);
            }
        }

        int j = 0;
        foreach (KeyValuePair<int, int> pair in indicesMis)
        {
            words[j++].Show(englishKeywords[pair.Key], pair.Value == min);
        }
    }

    public void Verif()
    {
        successes[nbReussites].enabled = true;

        if (++nbReussites >= objReussites)
        {
            Resolu();
        }
        else
        {
            Restart();
        }
    }

    public override void Faute()
    {
        base.Faute();
        Restart();
    }
}
