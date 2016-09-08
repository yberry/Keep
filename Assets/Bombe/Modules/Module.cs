using UnityEngine;

public abstract class Module : Carre {

    protected bool desamorce = false;
    public bool Desamorce
    {
        get
        {
            return desamorce;
        }
    }
    public Light lumiere;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Resolu()
    {
        desamorce = true;
        Bombe.Get.Verif();
    }
}
