using UnityEngine;
using UnityEngine.UI;

public class Timer : Carre {

    private float tempsDepart;
    private float temps;
    private int[] chiffres;
    public bool defile = false;
    public Text affichageTemps;

    private int erreursMax;
    private int erreurs;

	// Use this for initialization
	void Start () {
        chiffres = new int[4];
        erreurs = 0;
	}
	
	// Update is called once per frame
	void Update () {
	    if (defile)
        {
            return;
        }

	}

    public void SetStart(int sec, int err)
    {
        tempsDepart = sec;
        temps = tempsDepart;
        erreursMax = err;
    }
}
