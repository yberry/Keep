using UnityEngine;
using System.Collections;

public abstract class Module : Carre {

    private bool desamorce = false;
    public bool Desamorce
    {
        get
        {
            return desamorce;
        }
    }

    private const float tempsFaute = 0.5f;

    [Header("Paramètres module")]
    [Tooltip("Lumière d'indication")]
    public Light lumiere;

	// Use this for initialization
	void Start () {
        lumiere.enabled = false;
	}

    public void Resolu()
    {
        desamorce = true;
        lumiere.color = Color.green;
        lumiere.enabled = true;
        Bombe.Get.Verif();
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
