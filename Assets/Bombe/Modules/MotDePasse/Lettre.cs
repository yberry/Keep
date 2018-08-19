using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Lettre : MonoBehaviour
{
    private char[] lettres;
    private int index = 0;
    public char CurrentLettre
    {
        get
        {
            return lettres[index];
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
        if (--index < 0)
        {
            index = lettres.Length - 1;
        }
        Affiche();
    }

    void Bas()
    {
        if (++index >= lettres.Length)
        {
            index = 0;
        }
        Affiche();
    }
}