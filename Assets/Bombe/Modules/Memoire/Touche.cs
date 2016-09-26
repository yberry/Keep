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
                affichageChiffre.text = chiffre.ToString();
            }
        }
    }

    private bool cible = false;

    public Text affichageChiffre;

    void Start()
    {

    }

    public void Restart()
    {
        affichageChiffre.text = "";
        cible = false;
    }

    public void SetCible()
    {
        cible = true;
    }

    public void Clic()
    {
        Memoire memoire = transform.parent.GetComponent<Memoire>();
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