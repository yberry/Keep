using UnityEngine;
using UnityEngine.UI;

public class Mot : MonoBehaviour {

    private bool aCliquer = false;

    public Button mot;

	// Use this for initialization
	void Start () {
        mot.onClick.AddListener(Clic);
	}

    public void Restart()
    {
        mot.GetComponentInChildren<Text>().text = "";
        aCliquer = false;
    }

    public void Show(string texte, bool clic)
    {
        mot.GetComponentInChildren<Text>().text = texte;
        aCliquer = clic;
    }

    void Clic()
    {
        QuiEstLePremier qui = GetComponentInParent<QuiEstLePremier>();
        if (aCliquer)
        {
            qui.Verif();
        }
        else
        {
            qui.Faute();
        }
    }
}
