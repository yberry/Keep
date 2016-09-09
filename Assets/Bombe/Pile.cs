using UnityEngine;
using System.Collections;

public class Pile : MonoBehaviour {

    public enum Type
    {
        AA,
        D
    }

    private int nbPiles;
    public int NbPiles
    {
        get
        {
            return nbPiles;
        }
    }

    private Type type;

	// Use this for initialization
	void Start () {
        nbPiles = Random.Range(1, 3);
        type = nbPiles == 1 ? Type.D : Type.AA;
	}
}
