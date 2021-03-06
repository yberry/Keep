﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Simon : Module {

    private static Color[] couleursDispo = new Color[]
    {
        Color.red,
        Color.blue,
        Color.green,
        Color.yellow
    };

    private const float tempsBoucle = 1.5f;
    private const float tempsAttente = 3f;
    private float tempsReponse = 0f;

    private bool reponseRecue = false;

    private int nbCombo;
    private int nbEtapes = 0;

    private List<Color> flashs;
    private List<Losange> signaux;
    private List<Losange> reponse;
    private List<Losange> reponseJoueur;

    public Losange rouge;
    public Losange bleu;
    public Losange vert;
    public Losange jaune;

    // Use this for initialization
    void Start () {
        nbCombo = Random.Range(3, 6);

        rouge.SetModule(this);
        bleu.SetModule(this);
        vert.SetModule(this);
        jaune.SetModule(this);

        flashs = new List<Color>();
        signaux = new List<Losange>();
        reponse = new List<Losange>();
        reponseJoueur = new List<Losange>();
        AddColor();
	}

    void Update()
    {
        if (reponseRecue)
        {
            tempsReponse += Time.deltaTime;
            if (tempsReponse >= tempsAttente)
            {
                reponseRecue = false;
                reponseJoueur.Clear();
                tempsReponse = 0f;
                StartCoroutine(SendSignaux());
            }
        }
    }

    void AddColor()
    {
        Color color = couleursDispo.RandomItem();
        flashs.Add(color);

        if (color == Color.red)
        {
            signaux.Add(rouge);
        }
        else if (color == Color.blue)
        {
            signaux.Add(bleu);
        }
        else if (color == Color.green)
        {
            signaux.Add(vert);
        }
        else if (color == Color.yellow)
        {
            signaux.Add(jaune);
        }
        CheckReponse();
        StartCoroutine(SendSignaux());
    }

    public void CheckReponse()
    {
        reponse.Clear();
        bool voyelle = Bombe.Instance.Voyelle;
        int fautes = Bombe.Instance.Erreurs;

        foreach (Color color in flashs)
        {
            if (color == Color.red)
            {
                switch (fautes)
                {
                    case 0:
                        reponse.Add(bleu);
                        break;

                    case 1:
                        reponse.Add(voyelle ? jaune : rouge);
                        break;

                    case 2:
                        reponse.Add(voyelle ? vert : jaune);
                        break;
                }
            }
            else if (color == Color.blue)
            {
                switch (fautes)
                {
                    case 0:
                        reponse.Add(voyelle ? rouge : jaune);
                        break;

                    case 1:
                        reponse.Add(voyelle ? vert : bleu);
                        break;

                    case 2:
                        reponse.Add(voyelle ? rouge : vert);
                        break;
                }
            }
            else if (color == Color.green)
            {
                switch (fautes)
                {
                    case 0:
                        reponse.Add(voyelle ? jaune : vert);
                        break;

                    case 1:
                        reponse.Add(voyelle ? bleu : jaune);
                        break;

                    case 2:
                        reponse.Add(voyelle ? jaune : bleu);
                        break;
                }
            }
            else if (color == Color.yellow)
            {
                switch (fautes)
                {
                    case 0:
                        reponse.Add(voyelle ? vert : rouge);
                        break;

                    case 1:
                        reponse.Add(voyelle ? rouge : vert);
                        break;

                    case 2:
                        reponse.Add(voyelle ? bleu : rouge);
                        break;
                }
            }
        }
    }

    IEnumerator SendSignaux()
    {
        while (true)
        {
            foreach (Losange losange in signaux)
            {
                losange.Flash();
                yield return new WaitForSeconds(Losange.tempsFlash);
            }
            yield return new WaitForSeconds(tempsBoucle);
        }
    }

    public void Clic(Losange losange)
    {
        reponseRecue = true;
        tempsReponse = 0f;
        StopCoroutine(SendSignaux());

        StopAllOthers(losange);

        if (reponse[reponseJoueur.Count] == losange)
        {
            reponseJoueur.Add(losange);
            if (reponse.Count == reponseJoueur.Count)
            {
                reponseRecue = false;
                nbEtapes++;
                Verif();
            }
        }
        else
        {
            Faute();
            reponseJoueur.Clear();
            CheckReponse();
        }
    }

    void StopAllOthers(Losange losange)
    {
        Losange[] losanges = new Losange[] { vert, bleu, rouge, jaune };

    }

    void Verif()
    {
        if (nbEtapes == nbCombo)
        {
            Resolu();
        }
        else
        {
            AddColor();
        }
    }
}
