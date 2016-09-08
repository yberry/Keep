﻿using UnityEngine;
using System.Collections;

public class FilHorizontal : MonoBehaviour {

    private static Color[] couleurs =
    {
        Color.white,
        Color.black,
        Color.blue,
        Color.yellow,
        Color.red
    };

    private Color couleur;
    public Color Couleur
    {
        get
        {
            return couleur;
        }
    }

    private bool aCouper = false;
    private bool coupe = false;

	// Use this for initialization
	void Start () {
        couleur = couleurs[Random.Range(0, couleurs.Length)];
        GetComponent<Renderer>().material.color = couleur;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Objectif()
    {
        aCouper = true;
    }
}