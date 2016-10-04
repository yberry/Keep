using UnityEngine;
using UnityEngine.UI;

public class Touche : MonoBehaviour
{
    private int chiffre;
    public int Chiffre
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
                affichageChiffre.GetComponentInChildren<Text>().text = chiffre.ToString();
            }
        }
    }

    private bool cible = false;

    public Button affichageChiffre;

    void Start()
    {
        affichageChiffre.onClick.AddListener(Clic);
    }

    public void Restart()
    {
        affichageChiffre.GetComponentInChildren<Text>().text = "";
        cible = false;
    }

    public void SetCible()
    {
        cible = true;
    }

    void Clic()
    {
        Memoire memoire = GetComponentInParent<Memoire>();
        if (cible)
        {
            memoire.Verif();
        }
        else
        {
            memoire.Faute();
        }
    }
}