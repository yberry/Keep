using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MemoryButton : MonoBehaviour
{
    private int chiffre;
    private Memory module;
    private bool cible = false;
    private Button affichageChiffre;

    public int Chiffre
    {
        get => chiffre;

        set
        {
            if (value >= 1 && value <= 4)
            {
                chiffre = value;
                affichageChiffre.GetComponentInChildren<TMP_Text>().text = chiffre.ToString();
            }
        }
    }

    void Start()
    {
        affichageChiffre = GetComponent<Button>();
        affichageChiffre.onClick.AddListener(Clic);
    }

    public void SetModule(Memory m)
    {
        module = m;
    }

    public void Restart()
    {
        affichageChiffre = GetComponent<Button>();
        affichageChiffre.GetComponentInChildren<TMP_Text>().text = "";
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
            module.Verif();
        }
        else
        {
            module.Faute();
        }
    }
}