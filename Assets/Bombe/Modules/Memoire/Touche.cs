using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Touche : MonoBehaviour
{
    private int chiffre;
    private Memoire memoire;
    private bool cible = false;
    private Button affichageChiffre;

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
        affichageChiffre = GetComponent<Button>();
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