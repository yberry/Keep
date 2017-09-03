using UnityEngine;

public class Pile : MonoBehaviour {

    public enum Type
    {
        AA,
        D
    }

    public int nbPiles { get; private set; }

    private Type type;

	// Use this for initialization
	void Start () {
        nbPiles = Random.Range(1, 3);
        type = nbPiles == 1 ? Type.D : Type.AA;
	}
}
