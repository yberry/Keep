using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class WhoFirstWord : MonoBehaviour {

    private bool isTarget = false;

    private WhoFirst module;

    private Button mot;

	// Use this for initialization
	void Start () {
        mot = GetComponent<Button>();
        mot.onClick.AddListener(Clic);
	}

    public void SetModule(WhoFirst q)
    {
        module = q;
    }

    public void Restart()
    {
        mot = GetComponent<Button>();
        mot.GetComponentInChildren<TMP_Text>().text = "";
        isTarget = false;
    }

    public void Show(string texte, bool clic)
    {
        mot.GetComponentInChildren<TMP_Text>().text = texte;
        isTarget = clic;
    }

    void Clic()
    {
        if (isTarget)
        {
            module.Verif();
        }
        else
        {
            module.Faute();
        }
    }
}
