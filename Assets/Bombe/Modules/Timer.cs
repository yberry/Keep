using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Timer : Carre {

    #region Timer
    public static Timer Instance { get; private set; }
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
        if (Instance == null)
        {
            Instance = this;
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
                Bombe.Instance.Mort();
            }
            SetChiffres();
        }
	}

    public void SetStart(int sec, bool hard)
    {
        tempsDepart = sec;
        temps = tempsDepart;
        hardcore = hard;
        SetChiffres();
    }

    void SetChiffres()
    {
        if (temps < 60f)
        {
            chiffres = temps.ToString("F2").PadLeft(5, '0');
        }
        else
        {
            int sec = (int)temps % 60;
            int min = ((int)temps - sec) / 60;

            chiffres = min.ToString("D2") + ":" + sec.ToString("D2");
        }

        affichageTemps.text = chiffres;
    }

    public bool HasNb(char nb)
    {
        return chiffres.IndexOf(nb) != -1;
    }

    public void Erreur()
    {
        ++erreurs;
    }
}
