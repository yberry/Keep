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
        simon = GetComponentInParent<Simon>();
        lumiere.enabled = false;
        bouton = GetComponent<Button>();
        bouton.onClick.AddListener(Clic);
	}
	
	// Update is called once per frame
	void Update () {
	
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
