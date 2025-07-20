using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Button))]
public class Symbol : MonoBehaviour {

    private const float tempsFaute = 0.5f;

    [SerializeField]
    private Light bande;
    [SerializeField]
    private Image image;

    private Symbols module;

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

    public void SetModule(Symbols s)
    {
        module = s;
    }

    public void SetSymbol(Sprite sprite)
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
            module.Verif();
        }
        else
        {
            module.Faute();
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
