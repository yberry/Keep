using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuiEstLePremier : Module {

    private static string[][] titresPossibles = new string[6][]
    {
        new string[1] { "UR" },
        new string[3] { "FIRST", "OKAY", "C" },
        new string[4] { "YES", "NOTHING", "LED", "THEY ARE" },
        new string[7] { "BLANK", "READ", "RED", "YOU", "YOUR", "YOU'RE", "THEIR" },
        new string[4] { "", "REED", "LEED", "THEY'RE" },
        new string[9] { "DISPLAY", "SAYS", "NO", "LEAD", "HOLD ON", "YOU ARE", "THERE", "SEE", "CEE" }
    };

    private static Dictionary<string, string[]> motsPossibles = new Dictionary<string, string[]>
    {
        { "READY", new string[14] { "YES", "OKAY", "WHAT", "MIDDLE", "LEFT", "PRESS", "RIGHT", "BLANK", "READY", "NO", "FIRST", "UHHH", "NOTHING", "WAIT"} },
        { "FIRST", new string[14] { "LEFT", "OKAY", "YES", "MIDDLE", "NO", "RIGHT", "NOTHING", "UHHH", "WAIT", "READY", "BLANK", "WHAT", "PRESS", "FIRST"} },
        { "NO", new string[14] { "BLANK", "UHHH", "WAIT", "FIRST", "WHAT", "READY", "RIGHT", "YES", "NOTHING", "LEFT", "PRESS", "OKAY", "NO", "MIDDLE"} },
        { "BLANK", new string[14] { "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES"} },
        { "NOTHING", new string[14] { "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES"} },
        { "YES", new string[14] { "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES"} },
        { "WHAT", new string[14] { "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES"} },
        { "UHHH", new string[14] { "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES"} },
        { "LEFT", new string[14] { "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES"} },
        { "RIGHT", new string[14] { "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES"} },
        { "MIDDLE", new string[14] { "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES"} },
        { "OKAY", new string[14] { "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES"} },
        { "WAIT", new string[14] { "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES"} },
        { "PRESS", new string[14] { "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES"} },
        { "YOU", new string[14] { "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES"} },
        { "YOU ARE", new string[14] { "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES"} },
        { "YOUR", new string[14] { "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES"} },
        { "YOU'RE", new string[14] { "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES"} },
        { "UR", new string[14] { "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES"} },
        { "U", new string[14] { "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES"} },
        { "UH HUH", new string[14] { "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES"} },
        { "UH UH", new string[14] { "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES"} },
        { "WHAT?", new string[14] { "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES"} },
        { "DONE", new string[14] { "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES"} },
        { "NEXT", new string[14] { "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES"} },
        { "HOLD", new string[14] { "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES"} },
        { "SURE", new string[14] { "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES"} },
        { "LIKE", new string[14] { "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES", "YES"} }
    };

    private const int objReussites = 3;
    private int nbReussites = 0;

    private int motCle;

    public Text titre;
    public Mot[] mots;

	// Use this for initialization
	void Start () {
        Reset();
	}

    void Reset()
    {
        motCle = Random.Range(0, mots.Length);
        int indice = Random.Range(0, titresPossibles[motCle].Length);
        titre.text = titresPossibles[motCle][indice];
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
            Reset();
        }
    }

    public override void Faute()
    {
        base.Faute();
        Reset();
    }
}
