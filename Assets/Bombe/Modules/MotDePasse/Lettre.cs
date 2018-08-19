using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Lettre : MonoBehaviour
{
    private char[] lettres;
    private int currentIndex = 0;

    private int CurrentIndex
    {
        get
        {
            return currentIndex;
        }

        set
        {
            if (value < 0)
            {
                currentIndex = lettres.Length - 1;
            }
            else if (value >= lettres.Length)
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex = value;
            }

            Affiche();
        }
    }

    public char CurrentLettre
    {
        get
        {
            return lettres[currentIndex];
        }
    }

    private Text affichage;

    public Button flecheHaut;
    public Button flecheBas;

    void Start()
    {
        flecheHaut.onClick.AddListener(Haut);
        flecheBas.onClick.AddListener(Bas);
    }

    public void SetLettres(char[] l)
    {
        affichage = GetComponent<Text>();
        lettres = l;
        Affiche();
    }

    void Affiche()
    {
        affichage.text = CurrentLettre.ToString();
    }

    void Haut()
    {
        --CurrentIndex;
    }

    void Bas()
    {
        ++CurrentIndex;
    }
}