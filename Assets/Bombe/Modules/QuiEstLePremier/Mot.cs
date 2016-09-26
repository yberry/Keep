﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Mot : MonoBehaviour {

    private bool aCliquer = false;

    public Text mot;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Restart()
    {
        mot.text = "";
        aCliquer = false;
    }

    public void Show(string texte, bool clic)
    {
        mot.text = texte;
        aCliquer = clic;
    }

    public void Clic()
    {
        QuiEstLePremier qui = transform.parent.GetComponent<QuiEstLePremier>();
        if (aCliquer)
        {
            qui.Verif();
        }
        else
        {
            qui.Faute();
        }
    }
}
