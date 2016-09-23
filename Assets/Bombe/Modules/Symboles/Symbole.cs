using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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
    private float temps = 0f;
    private bool faute = false;

    public Light bande;
    public Image image;

	// Use this for initialization
	void Start () {
        bande.enabled = false;
	}

    void Update() {
        if (faute)
        {
            temps += Time.deltaTime;
            if (temps >= tempsFaute)
            {
                bande.enabled = false;
                temps = 0f;
                faute = false;
            }
        }
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
        }
        else
        {
            faute = true;
            bande.color = Color.red;
            temps = 0f;
            symboles.Faute();
        }
        bande.enabled = true;
    }
}
