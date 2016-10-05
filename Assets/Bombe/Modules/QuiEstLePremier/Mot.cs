using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Mot : MonoBehaviour {

    private bool aCliquer = false;

    private QuiEstLePremier qui;

    private Button mot;

	// Use this for initialization
	void Start () {
        mot = GetComponent<Button>();
        mot.onClick.AddListener(Clic);
	}

    public void SetModule(QuiEstLePremier q)
    {
        qui = q;
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
