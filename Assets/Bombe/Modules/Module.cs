using UnityEngine;
using System.Collections;

public abstract class Module : Carre {

    public bool Desamorce { get; private set; }

    private const float tempsFaute = 0.5f;

    [Header("Paramètres module")]
    [Tooltip("Lumière d'indication")]
    public Light lumiere;

    private Coroutine coroutineFaute;

	// Use this for initialization
	void Start () {
        Desamorce = false;
        lumiere.enabled = false;
	}

    public void Resolu()
    {
        Desamorce = true;
        lumiere.color = Color.green;
        lumiere.enabled = true;
        Bombe.instance.Verif();
    }

    public override void Faute()
    {
        base.Faute();
        if (coroutineFaute != null)
        {
            StopCoroutine(coroutineFaute);
        }
        coroutineFaute = StartCoroutine(TempsFaute());
    }

    IEnumerator TempsFaute()
    {
        lumiere.color = Color.red;
        lumiere.enabled = true;
        yield return new WaitForSeconds(tempsFaute);
        lumiere.color = Color.green;
        lumiere.enabled = Desamorce;
    }

    protected virtual void OnDestroy()
    {
        if (coroutineFaute != null)
        {
            StopCoroutine(coroutineFaute);
        }
    }
}
