using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PasswordChar : MonoBehaviour
{
    [SerializeField]
    private Button flecheHaut;
    [SerializeField]
    private Button flecheBas;

    private char[] lettres;
    private int currentIndex = 0;
    private TMP_Text affichage;

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

    void Start()
    {
        flecheHaut.onClick.AddListener(Haut);
        flecheBas.onClick.AddListener(Bas);
    }

    public void SetLettres(char[] l)
    {
        affichage = GetComponent<TMP_Text>();
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