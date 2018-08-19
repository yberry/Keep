using UnityEngine;

public class Pile : MonoBehaviour {

    public enum Type
    {
        AA,
        D
    }

    public int NbPiles { get; private set; }

    private Type type;

	// Use this for initialization
	void Start () {
        NbPiles = Random.Range(1, 3);
        type = NbPiles == 1 ? Type.D : Type.AA;
	}
}
