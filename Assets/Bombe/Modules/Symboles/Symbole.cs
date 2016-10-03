using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Symbole : MonoBehaviour {

    private Symboles.Type type;
    private bool suivant = false;
    private bool appuye = false;
    public bool Appuye
    {
        get
        {
            return appuye;
        }
    }
    private const float tempsFaute = 0.5f;

    public Light bande;
    public Image image;

	// Use this for initialization
	void Start () {
        bande.enabled = false;
	}

    public void SetSymbole(Sprite sprite)
    {
        image.sprite = sprite;
    }

    public void SetSuivant()
    {
        suivant = true;
    }

    public void Clic()
    {
        if (appuye)
        {
            return;
        }
        Symboles symboles = transform.parent.GetComponent<Symboles>();
        if (suivant)
        {
            appuye = true;
            bande.color = Color.green;
            suivant = false;
            symboles.Verif();
            bande.enabled = true;
        }
        else
        {
            symboles.Faute();
            StartCoroutine(TempsFaute());
        }
    }

    IEnumerator TempsFaute()
    {
        bande.color = Color.red;
        bande.enabled = true;
        yield return new WaitForSeconds(tempsFaute);
        bande.enabled = false;
    }
}
