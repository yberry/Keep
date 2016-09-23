using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Symbole : MonoBehaviour {

    public enum Type
    {

    }

    private static Dictionary<Type, Sprite> dico = new Dictionary<Type, Sprite>()
    {

    };

    private Type type;
    private bool appuye = false;

    public Light bande;
    public Image image;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetSymbole(Type t)
    {
        type = t;
        image.sprite = dico[t];
    }
}
