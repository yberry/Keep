using UnityEngine;
using UnityEngine.UI;

public class Condensateur : Needy {

    [Header("Condensateur")]
    public Transform manette;
    public Image jauge;

    protected override void Ecoule()
    {
        Faute();
    }

    public void Appuye()
    {
        active = false;
    }

    public void Relache()
    {
        active = true;
    }

    protected override void UpdateNeedy()
    {
        if (!active)
        {
            temps += 2f * Time.deltaTime;
            if (temps > tempsDepart)
            {
                temps = tempsDepart;
            }
        }
        jauge.fillAmount = 1f - temps / tempsDepart;
    }
}
