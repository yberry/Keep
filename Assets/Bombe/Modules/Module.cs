using UnityEngine;
using System.Collections;

public abstract class Module : Carre {

    public bool desamorce { get; private set; }

    private const float tempsFaute = 0.5f;

    [Header("Paramètres module")]
    [Tooltip("Lumière d'indication")]
    public Light lumiere;

	// Use this for initialization
	void Start () {
        desamorce = false;
        lumiere.enabled = false;
	}

    public void Resolu()
    {
        desamorce = true;
        lumiere.color = Color.green;
        lumiere.enabled = true;
        Bombe.instance.Verif();
    }

    public override void Faute()
    {
        base.Faute();
        StartCoroutine(TempsFaute());
    }

    IEnumerator TempsFaute()
    {
        lumiere.color = Color.red;
        lumiere.enabled = true;
        yield return new WaitForSeconds(tempsFaute);
        lumiere.color = Color.green;
        lumiere.enabled = desamorce;
    }
}
