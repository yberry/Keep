using UnityEngine;

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
    private float temps = 0f;
    private bool faute = false;

    [Header("Paramètres module")]
    [Tooltip("Lumière d'indication")]
    public Light lumiere;

	// Use this for initialization
	void Start () {
        lumiere.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	    if (faute)
        {
            temps += Time.deltaTime;
            if (temps >= tempsFaute)
            {
                lumiere.color = Color.green;
                lumiere.enabled = desamorce;
                temps = 0f;
                faute = false;
            }
        }
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
        temps = 0f;
        faute = true;
        lumiere.color = Color.red;
        lumiere.enabled = true;
    }
}
