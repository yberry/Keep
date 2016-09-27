using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Lettre : MonoBehaviour
{
    private char[] lettres;
    private int current = 0;
    public char currentLettre
    {
        get
        {
            return lettres[current];
        }
    }

    private Text affichage;

    public Button flecheHaut;
    public Button flecheBas;

    void Start()
    {
        affichage = GetComponent<Text>();
        Affiche();
        flecheHaut.onClick.AddListener(Haut);
        flecheBas.onClick.AddListener(Bas);
    }

    void Affiche()
    {
        affichage.text = currentLettre.ToString();
    }

    void Haut()
    {
        current--;
        if (current < 0)
        {
            current = lettres.Length - 1;
        }
        Affiche();
    }

    void Bas()
    {
        current++;
        if (current >= lettres.Length)
        {
            current = 0;
        }
        Affiche();
    }
}