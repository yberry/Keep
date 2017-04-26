using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Timer : Carre {

    #region Timer
    public static Timer instance { get; private set; }
    #endregion

    private float tempsDepart;
    private float temps;
    private string chiffres;

    private bool hardcore;
    private int erreurs;

    private AudioSource source;

    public bool defile = false;
    public Text affichageTemps;
    

    // Use this for initialization
    void Start () {
        if (instance == null)
        {
            instance = this;
        }
        chiffres = "";
        erreurs = 0;
        source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (defile)
        {
            temps -= Time.deltaTime * (1f + erreurs * 0.25f);
            if (temps <= 0f)
            {
                Bombe.instance.Mort();
            }
        }
        SetChiffres();
	}

    public void SetStart(int sec, bool hard)
    {
        tempsDepart = sec;
        temps = tempsDepart;
        hardcore = hard;
    }

    void SetChiffres()
    {
        int sec = (int)temps % 60;
        int min = ((int)temps - sec) / 60;
        if (min > 0)
        {
            chiffres = Get2Chiffres(min) + ":" + Get2Chiffres(sec);
        }
        else
        {
            int dec = Mathf.FloorToInt(100f * (temps - (int)temps));

            chiffres = Get2Chiffres(sec) + "." + Get2Chiffres(dec);
        }
        affichageTemps.text = chiffres;
    }

    public static string Get2Chiffres(int t)
    {
        return (t < 10 ? "0" : "") + t.ToString();
    }

    public bool HasNb(char nb)
    {
        return chiffres.IndexOf(nb) >= 0;
    }

    public void Erreur()
    {
        erreurs++;
    }
}
