using UnityEngine;
using UnityEngine.UI;

public class Bombe : MonoBehaviour {

    #region Bombe
    private static Bombe instance;
    public static Bombe Get
    {
        get
        {
            return instance;
        }
    }
    #endregion

    #region NumSerie
    private const int nbCharSerie = 6;
    private string numSerie;
    public bool numPair
    {
        get
        {
            return numSerie[5] % 2 == 0;
        }
    }
    public bool voyelle
    {
        get
        {
            foreach (char c in numSerie)
            {
                if ("AEIOUY".IndexOf(c) >= 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
    [Tooltip("Affichage du numéro de série")]
    public Text textSerie;
    #endregion

    #region Carres
    private Carre[] carres;
    #endregion

    // Use this for initialization
    void Start () {
	    if (instance == null)
        {
            instance = this;
        }
        SetSerial();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void SetSerial()
    {
        numSerie = "";
        for (int i = 0; i < nbCharSerie; i++)
        {
            numSerie += RandomLetter(i == nbCharSerie - 1);
        }
    }

    char RandomLetter(bool last)
    {
        string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return alpha[Random.Range(last ? 26 : 0, alpha.Length)];
    }
}
