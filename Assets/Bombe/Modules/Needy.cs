using UnityEngine;
using UnityEngine.UI;

public abstract class Needy : Carre {

    protected bool active;
    protected const float tempsDepart = 45f;
    protected float temps;
    public Text compteur;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (active)
        {
            temps -= Time.deltaTime;
            if (temps <= 0)
            {
                Faute();
                active = false;
            }
        }
        AfficheTemps();
	}

    void AfficheTemps()
    {
        int sec = (int)temps;
        compteur.text = sec.ToString();
    }

    protected virtual void Restart()
    {
        temps = tempsDepart;
        active = true;
    }
}
