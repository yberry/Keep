using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuiEstLePremier : Module {

    private static string[][] titresPossibles
    {
        get
        {
            return new string[][]
            {
                new string[] { "UR" },
                new string[] { "FIRST", "OKAY", "C" },
                new string[] { "YES", "NOTHING", "LED", "THEY ARE" },
                new string[] { "BLANK", "READ", "RED", "YOU", "YOUR", "YOU'RE", "THEIR" },
                new string[] { "", "REED", "LEED", "THEY'RE" },
                new string[] { "DISPLAY", "SAYS", "NO", "LEAD", "HOLD ON", "YOU ARE", "THERE", "SEE", "CEE" }
            };
        }
    }

    /*private static readonly string[] motsPossibles =
    {
        "READY", "FIRST", "NO", "BLANK", "NOTHING", "YES", "WHAT",
        "UHHH", "LEFT", "RIGHT", "MIDDLE", "OKAY", "WAIT", "PRESS",
        "YOU", "YOU ARE", "YOUR", "YOU'RE", "UR", "U", "UH HUH",
        "UH UH", "WHAT?", "DONE", "NEXT", "HOLD", "SURE", "LIKE"
    };*/

    private static Dictionary<string, List<string>> motsSuivants
    {
        get
        {
            return new Dictionary<string, List<string>>
            {
                { "READY", new List<string>() { "YES", "OKAY", "WHAT", "MIDDLE", "LEFT", "PRESS", "RIGHT", "BLANK", "READY", "NO", "FIRST", "UHHH", "NOTHING", "WAIT"} },
                { "FIRST", new List<string>() { "LEFT", "OKAY", "YES", "MIDDLE", "NO", "RIGHT", "NOTHING", "UHHH", "WAIT", "READY", "BLANK", "WHAT", "PRESS", "FIRST"} },
                { "NO", new List<string>() { "BLANK", "UHHH", "WAIT", "FIRST", "WHAT", "READY", "RIGHT", "YES", "NOTHING", "LEFT", "PRESS", "OKAY", "NO", "MIDDLE"} },
                { "BLANK", new List<string>() { "WAIT", "RIGHT", "OKAY", "MIDDLE", "BLANK", "PRESS", "READY", "NOTHING", "NO", "WHAT", "LEFT", "UHHH", "YES", "FIRST"} },
                { "NOTHING", new List<string>() { "UHHH", "RIGHT", "OKAY", "MIDDLE", "YES", "BLANK", "NO", "PRESS", "LEFT", "WHAT", "WAIT", "FIRST", "NOTHING", "READY"} },
                { "YES", new List<string>() { "OKAY", "RIGHT", "UHHH", "MIDDLE", "FIRST", "WHAT", "PRESS", "READY", "NOTHING", "YES", "LEFT", "BLANK", "NO", "WAIT"} },
                { "WHAT", new List<string>() { "UHHH", "WHAT", "LEFT", "NOTHING", "READY", "BLANK", "MIDDLE", "NO", "OKAY", "FIRST", "WAIT", "YES", "PRESS", "RIGHT"} },
                { "UHHH", new List<string>() { "READY", "NOTHING", "LEFT", "WHAT", "OKAY", "YES", "RIGHT", "NO", "PRESS", "BLANK", "UHHH", "MIDDLE", "WAIT", "FIRST"} },
                { "LEFT", new List<string>() { "RIGHT", "LEFT", "FIRST", "NO", "MIDDLE", "YES", "BLANK", "WHAT", "UHHH", "WAIT", "PRESS", "READY", "OKAY", "NOTHING"} },
                { "RIGHT", new List<string>() { "YES", "NOTHING", "READY", "PRESS", "NO", "WAIT", "WHAT", "RIGHT", "MIDDLE", "LEFT", "UHHH", "BLANK", "OKAY", "FIRST"} },
                { "MIDDLE", new List<string>() { "BLANK", "READY", "OKAY", "WHAT", "NOTHING", "PRESS", "NO", "WAIT", "LEFT", "MIDDLE", "RIGHT", "FIRST", "UHHH", "YES"} },
                { "OKAY", new List<string>() { "MIDDLE", "NO", "FIRST", "YES", "UHHH", "NOTHING", "WAIT", "OKAY", "LEFT", "READY", "BLANK", "PRESS", "WHAT", "RIGHT"} },
                { "WAIT", new List<string>() { "UHHH", "NO", "BLANK", "OKAY", "YES", "LEFT", "FIRST", "PRESS", "WHAT", "WAIT", "NOTHING", "READY", "RIGHT", "MIDDLE"} },
                { "PRESS", new List<string>() { "RIGHT", "MIDDLE", "YES", "READY", "PRESS", "OKAY", "NOTHING", "UHHH", "BLANK", "LEFT", "FIRST", "WHAT", "NO", "WAIT"} },
                { "YOU", new List<string>() { "SURE", "YOU ARE", "YOUR", "YOU'RE", "NEXT", "UH HUH", "UR", "HOLD", "WHAT?", "YOU", "UH UH", "LIKE", "DONE", "U"} },
                { "YOU ARE", new List<string>() { "YOUR", "NEXT", "LIKE", "UH HUH", "WHAT?", "DONE", "UH UH", "HOLD", "YOU", "U", "YOU'RE", "SURE", "UR", "YOU ARE"} },
                { "YOUR", new List<string>() { "UH UH", "YOU ARE", "UH HUH", "YOUR", "NEXT", "UR", "SURE", "U", "YOU'RE", "YOU", "WHAT?", "HOLD", "LIKE", "DONE"} },
                { "YOU'RE", new List<string>() { "YOU", "YOU'RE", "UR", "NEXT", "UH UH", "YOU ARE", "U", "YOUR", "WHAT?", "UH HUH", "SURE", "DONE", "LIKE", "HOLD"} },
                { "UR", new List<string>() { "DONE", "U", "UR", "UH HUH", "WHAT?", "SURE", "YOUR", "HOLD", "YOU'RE", "LIKE", "NEXT", "UH UH", "YOU ARE", "YOU"} },
                { "U", new List<string>() { "UH HUH", "SURE", "NEXT", "WHAT?", "YOU'RE", "UR", "UH UH", "DONE", "U", "YOU", "LIKE", "HOLD", "YOU ARE", "YOUR"} },
                { "UH HUH", new List<string>() { "UH HUH", "YOUR", "YOU ARE", "YOU", "DONE", "HOLD", "UH UH", "NEXT", "SURE", "LIKE", "YOU'RE", "UR", "U", "WHAT?"} },
                { "UH UH", new List<string>() { "UR", "U", "YOU ARE", "YOU'RE", "NEXT", "UH UH", "DONE", "YOU", "UH HUH", "LIKE", "YOUR", "SURE", "HOLD", "WHAT?"} },
                { "WHAT?", new List<string>() { "YOU", "HOLD", "YOU'RE", "YOUR", "U", "DONE", "UH UH", "LIKE", "YOU ARE", "UH HUH", "UR", "NEXT", "WHAT?", "SURE"} },
                { "DONE", new List<string>() { "SURE", "UH HUH", "NEXT", "WHAT?", "YOUR", "UR", "YOU'RE", "HOLD", "LIKE", "YOU", "U", "YOU ARE", "UH UH", "DONE"} },
                { "NEXT", new List<string>() { "WHAT?", "UH HUH", "UH UH", "YOUR", "HOLD", "SURE", "NEXT", "LIKE", "DONE", "YOU ARE", "UR", "YOU'RE", "U", "YOU"} },
                { "HOLD", new List<string>() { "YOU ARE", "U", "DONE", "UH UH", "YOU", "UR", "SURE", "WHAT?", "YOU'RE", "NEXT", "HOLD", "UH HUH", "YOUR", "LIKE"} },
                { "SURE", new List<string>() { "YOU ARE", "DONE", "LIKE", "YOU'RE", "YOU", "HOLD", "UH HUH", "UR", "SURE", "U", "WHAT?", "NEXT", "YOUR", "UH UH"} },
                { "LIKE", new List<string>() { "YOU'RE", "NEXT", "U", "UR", "HOLD", "DONE", "UH UH", "WHAT?", "UH HUH", "YOU", "LIKE", "SURE", "YOU ARE", "YOUR"} }
            };
        }
    }

    private const int objReussites = 3;
    private int nbReussites = 0;

    private int numCle;

    public Text titre;
    public Mot[] mots;

	// Use this for initialization
	void Start () {
        Restart();
	}

    void Restart()
    {
        numCle = Random.Range(0, mots.Length);
        int indice = Random.Range(0, titresPossibles[numCle].Length);
        titre.text = titresPossibles[numCle][indice];
        foreach (Mot mot in mots)
        {
            mot.Restart();
        }

        //string motCle = motsPossibles[Random.Range(0, motsPossibles.Length)];

        string[] motsCle = new string[motsSuivants.Keys.Count];
        motsSuivants.Keys.CopyTo(motsCle, 0);
        string motCle = motsCle[Random.Range(0, motsCle.Length)];

        Dictionary<string, int> motsMis = new Dictionary<string, int>();

        List<string> motsLigne = motsSuivants[motCle];
        int indexCle = motsLigne.IndexOf(motCle);

        for (int i = 0; i < mots.Length; i++)
        {
            if (i != numCle)
            {
                int index = Random.Range(0, motsLigne.Count);
                while (index == indexCle || motsMis.ContainsValue(index))
                {
                    index = Random.Range(0, motsLigne.Count);
                }
                motsMis.Add(motsLigne[index], index);
            }
            else
            {
                motsMis.Add(motCle, indexCle);
            }
        }

        int min = motsLigne.Count;

        foreach (int index in motsMis.Values)
        {
            if (min > index)
            {
                min = index;
            }
        }

        int j = 0;
        foreach (string mot in motsMis.Keys)
        {
            mots[j].Show(mot, motsMis[mot] == min);
            j++;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Verif()
    {
        nbReussites++;
        if (nbReussites >= objReussites)
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
