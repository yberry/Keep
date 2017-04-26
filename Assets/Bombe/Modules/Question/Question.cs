using UnityEngine;
using UnityEngine.UI;

public class Question : Needy {

    [Header("Question")]
    public Text question;
    public Button yes;
    public Button no;

    private const string yesResponse = "VENT GAS ?";
    private const string noResponse = "DETONATE ?";

    protected override void Restart()
    {
        base.Restart();

        if (Random.Range(0, 2) == 0)
        {
            question.text = yesResponse;
            yes.onClick.AddListener(Desamorce);
            no.onClick.AddListener(Faute);
        }
        else
        {
            question.text = noResponse;
            yes.onClick.AddListener(Faute);
            no.onClick.AddListener(Desamorce);
        }
    }

    protected override void Ecoule()
    {
        Faute();
    }

    protected override void Desamorce()
    {
        base.Desamorce();
        question.text = "";
        yes.onClick.RemoveAllListeners();
        no.onClick.RemoveAllListeners();
    }
}
