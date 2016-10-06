using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Button))]
public class Losange : MonoBehaviour {

    public const float tempsFlash = 0.6f;

    private Simon simon;

    private Color couleur;
    private Button bouton;

    public Light lumiere;
    

	// Use this for initialization
	void Start () {
        lumiere.enabled = false;
        bouton = GetComponent<Button>();
        bouton.onClick.AddListener(Clic);
	}
	
	public void SetModule(Simon s)
    {
        simon = s;
    }

    public IEnumerator Flash()
    {
        lumiere.enabled = true;
        yield return new WaitForSeconds(tempsFlash);
        lumiere.enabled = false;
    }

    void Clic()
    {

    }
}
