using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Button))]
public class Symbole : MonoBehaviour {

    private const float tempsFaute = 0.5f;

    [SerializeField]
    private Light bande;
    [SerializeField]
    private Image image;

    private Symboles symboles;

    private Button bouton;
    private bool suivant = false;

    public bool Appuye { get; private set; }

    // Use this for initialization
    void Awake () {
        Appuye = false;
        bande.enabled = false;
        bouton = GetComponent<Button>();
        bouton.onClick.AddListener(Clic);
	}

    public void SetModule(Symboles s)
    {
        symboles = s;
    }

    public void SetSymbole(Sprite sprite)
    {
        image.sprite = sprite;
    }

    public void SetSuivant()
    {
        suivant = true;
    }

    void Clic()
    {
        if (Appuye)
        {
            return;
        }

        if (suivant)
        {
            Appuye = true;
            bouton.interactable = false;
            bande.color = Color.green;
            suivant = false;
            bande.enabled = true;
            symboles.Verif();
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
