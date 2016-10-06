using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
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

    private Memoire memoire;

    private bool cible = false;

    private Button affichageChiffre;

    void Start()
    {
        affichageChiffre = GetComponent<Button>();
        affichageChiffre.onClick.AddListener(Clic);
    }

    public void SetModule(Memoire m)
    {
        memoire = m;
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