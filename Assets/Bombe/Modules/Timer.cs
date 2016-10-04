using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Timer : Carre {

    #region Timer
    private static Timer instance;
    public static Timer Get
    {
        get
        {
            return instance;
        }
    }
    #endregion

    private float tempsDepart;
    private float temps;
    private string chiffres;

    private bool hardcore;
    private int erreurs;
    public int Erreurs
    {
        get
        {
            return erreurs;
        }
    }

    public bool defile = false;
    public Text affichageTemps;
    public AudioSource source;

    // Use this for initialization
    void Start () {
        if (instance == null)
        {
            instance = this;
        }
        chiffres = "";
        erreurs = 0;
	}
	
	// Update is called once per frame
	void Update () {
	    if (defile)
        {
            temps -= Time.deltaTime * (1f + erreurs / 4f);
            if (temps <= 0f)
            {
                Bombe.Get.Mort();
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
            int min0 = min % 10;
            int min1 = (min - (min % 10)) / 10;

            int sec0 = sec % 10;
            int sec1 = (sec - (sec % 10)) / 10;

            chiffres = min1.ToString() + min0.ToString() + ':' + sec1.ToString() + sec0.ToString();
        }
        else
        {
            int sec0 = sec % 10;
            int sec1 = (sec - (sec % 10)) / 10;

            int dec0 = (int)((temps * 100f) % 10f);
            int dec1 = (int)((temps * 10f) % 10f);

            chiffres = sec1.ToString() + sec0.ToString() + '.' + dec1.ToString() + dec0.ToString();
        }
        affichageTemps.text = chiffres;
    }

    public bool HasNb(char nb)
    {
        return chiffres.IndexOf(nb) >= 0;
    }

    public void Erreur()
    {
        erreurs++;
        if (hardcore)
        {
            Bombe.Get.Mort();
        }
        else if (erreurs == 3)
        {
            Bombe.Get.Mort();
        }
    }
}
