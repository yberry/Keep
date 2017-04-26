using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Condensateur : Needy {

    [Header("Condensateur")]
    public Transform manette;
    public Image jauge;

    protected override void Ecoule()
    {
        Faute();
    }
}
