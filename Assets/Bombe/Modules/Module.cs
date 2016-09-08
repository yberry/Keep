using UnityEngine;

public abstract class Module : Carre {

    protected bool desamorce = false;
    public Light lumiere;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Resolu()
    {
        desamorce = true;
        //bombe
    }
}
