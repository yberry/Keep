﻿using UnityEngine;
using UnityEngine.UI;

public abstract class Needy : Carre {

    protected bool active;
    protected const float tempsDepart = 45f;
    protected float temps;

    [Header("Paramètres Needy")]
    public Text compteur;

	void Start ()
    {
        temps = tempsDepart;
        compteur.gameObject.SetActive(false);
	}
	
	void Update ()
    {
	    if (active)
        {
            temps -= Time.deltaTime;
            if (temps <= 0f)
            {
                Ecoule();
            }
        }
        UpdateNeedy();
        AfficheTemps();
	}

    protected virtual void UpdateNeedy()
    {
        // A compléter si besoin
    }

    void AfficheTemps()
    {
        compteur.text = ((int)temps).ToString("D2");
    }

    protected virtual void Restart()
    {
        temps = tempsDepart;
        active = true;
        compteur.gameObject.SetActive(true);
    }

    protected abstract void Ecoule();

    protected virtual void Desamorce()
    {
        active = false;
        compteur.gameObject.SetActive(false);
    }

    public override void Faute()
    {
        base.Faute();
        Desamorce();
    }

    
}
