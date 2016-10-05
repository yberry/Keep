﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Button))]
public class Symbole : MonoBehaviour {

    private Symboles symboles;

    private Button bouton;
    private Symboles.Type type;
    private bool suivant = false;
    private bool appuye = false;
    public bool Appuye
    {
        get
        {
            return appuye;
        }
    }
    private const float tempsFaute = 0.5f;

    public Light bande;
    public Image image;

	// Use this for initialization
	void Start () {
        bande.enabled = false;
        bouton = GetComponent<Button>();
        bouton.onClick.AddListener(Clic);
	}

    public void SetModule(Symboles s)
    {
        symboles = s;
    }

    public void SetSymbole(Sprite sprite)
    {
        image.sprite = sprite;
    }

    public void SetSuivant()
    {
        suivant = true;
    }

    void Clic()
    {
        if (appuye)
        {
            return;
        }

        if (suivant)
        {
            appuye = true;
            bouton.interactable = false;
            bande.color = Color.green;
            suivant = false;
            bande.enabled = true;
            symboles.Verif();
        }
        else
        {
            symboles.Faute();
            StartCoroutine(TempsFaute());
        }
    }

    IEnumerator TempsFaute()
    {
        bande.color = Color.red;
        bande.enabled = true;
        yield return new WaitForSeconds(tempsFaute);
        bande.enabled = false;
    }
}
