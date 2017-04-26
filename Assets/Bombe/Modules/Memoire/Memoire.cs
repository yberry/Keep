using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Memoire : Module
{
    public Touche[] touches;
    public Light[] succes;

    private int chiffre;
    private int Chiffre
    {
        get
        {
            return chiffre;
        }
        set
        {
            if (value >= 1 && value <= 4)
            {
                chiffre = value;
                affichageChiffre.text = chiffre.ToString();
            }
        }
    }

    private List<int> chiffres;
    private List<int> positions;

    private const int objEtapes = 5;
    private int nbEtapes = 0;

    public Text affichageChiffre;

    // Use this for initialization
    void Start()
    {
        chiffres = new List<int>();
        positions = new List<int>();
        foreach (Touche touche in touches)
        {
            touche.SetModule(this);
        }
        foreach (Light light in succes)
        {
            light.color = Color.green;
            light.enabled = false;
        }
        Restart();
    }

    void Restart()
    {
        Chiffre = Random.Range(1, 5);

        List<int> chiffresPris = new List<int>();

        foreach (Touche touche in touches)
        {
            touche.Restart();
            int ch;

            do
            {
                ch = Random.Range(1, 5);
            }
            while (chiffresPris.Contains(ch));

            chiffresPris.Add(ch);
            touche.Chiffre = ch;
        }

        switch (nbEtapes)
        {
            case 0:
                switch (Chiffre)
                {
                    case 1:
                    case 2:
                        ClickOnPosition(1);
                        break;

                    case 3:
                        ClickOnPosition(2);
                        break;

                    case 4:
                        ClickOnPosition(3);
                        break;
                }
                break;

            case 1:
                switch (Chiffre)
                {
                    case 1:
                        ClickOnChiffre(4);
                        break;

                    case 2:
                    case 4:
                        ClickOnPosition(positions[0]);
                        break;

                    case 3:
                        ClickOnPosition(0);
                        break;
                }
                break;

            case 2:
                switch (Chiffre)
                {
                    case 1:
                        ClickOnChiffre(chiffres[1]);
                        break;

                    case 2:
                        ClickOnChiffre(chiffres[0]);
                        break;

                    case 3:
                        ClickOnPosition(2);
                        break;

                    case 4:
                        ClickOnChiffre(4);
                        break;
                }
                break;

            case 3:
                switch (Chiffre)
                {
                    case 1:
                        ClickOnPosition(positions[0]);
                        break;

                    case 2:
                        ClickOnPosition(0);
                        break;

                    case 3:
                    case 4:
                        ClickOnPosition(positions[1]);
                        break;
                }
                break;

            case 4:
                switch (Chiffre)
                {
                    case 1:
                        ClickOnChiffre(chiffres[0]);
                        break;

                    case 2:
                        ClickOnChiffre(chiffres[1]);
                        break;

                    case 3:
                        ClickOnChiffre(chiffres[3]);
                        break;

                    case 4:
                        ClickOnChiffre(chiffres[2]);
                        break;
                }
                break;
        }
    }

    void ClickOnPosition(int pos)
    {
        positions.Add(pos);
        chiffres.Add(touches[pos].Chiffre);
        touches[pos].SetCible();
    }

    void ClickOnChiffre(int ch)
    {
        chiffres.Add(ch);
        int pos = GetPositionChiffre(ch);
        positions.Add(pos);
        touches[pos].SetCible();
    }

    int GetPositionChiffre(int ch)
    {
        int i = 0;
        while (touches[i].Chiffre != ch)
        {
            i++;
        }
        return i;
    }

    public void Verif()
    {
        succes[nbEtapes].enabled = true;
        nbEtapes++;
        if (nbEtapes >= objEtapes)
        {
            Resolu();
        }
        else
        {
            Restart();
        }
    }

    public override void Faute()
    {
        base.Faute();
        foreach (Light light in succes)
        {
            light.enabled = false;
        }
        nbEtapes = 0;
        chiffres.Clear();
        positions.Clear();
        Restart();
    }
}
