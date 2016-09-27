using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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
            return numSerie[nbCharSerie - 1] % 2 == 0;
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
    [Header("Numéro de série")]
    [Tooltip("Affichage du numéro de série")]
    public Text textSerie;
    #endregion

    #region Prefabs
    [Header("Préfabs de modules")]
    [Tooltip("Préfab de fils horizontaux")]
    public FilsHorizontaux filsHorizontaux;
    [Tooltip("Préfab de bouton")]
    public Bouton bouton;
    [Tooltip("Préfab de symboles")]
    public Symboles symboles;
    [Tooltip("Préfab de simon")]
    public Simon simon;
    [Tooltip("Préfab de qui est le premier")]
    public QuiEstLePremier quiEstLePremier;
    [Tooltip("Préfab de memoire")]
    public Memoire memoire;
    [Tooltip("Préfab de morse")]
    public Morse morse;
    [Tooltip("Préfab de fils verticaux")]
    public FilsVerticaux filsVerticaux;
    [Tooltip("Préfab de séquences de fils")]
    public SequencesFils sequencesFils;
    [Tooltip("Préfab de labyrinthe")]
    public Labyrinthe labyrinthe;
    [Tooltip("Préfab de mot de passe")]
    public MotDePasse motDePasse;
    [Tooltip("Préfab de question")]
    public Question question;
    [Tooltip("Préfab de condensateur")]
    public Condensateur condensateur;
    [Tooltip("Préfab de knob")]
    public Knob knob;
    #endregion

    #region Carres
    private Timer timer;
    private List<Carre> carres;
    private List<Module> modules;
    #endregion

    #region Fautes
    private int erreurs = 0;
    private bool hardcore = false;
    #endregion

    #region Peripheriques
    private List<Indic> indicateurs;
    private List<Port> ports;
    private List<Pile> piles;
    public int NbPiles
    {
        get
        {
            int nb = 0;
            foreach (Pile pile in piles)
            {
                nb += pile.NbPiles;
            }
            return nb;
        }
    }
    #endregion

    // Use this for initialization
    void Start () {
	    if (instance == null)
        {
            instance = this;
        }
        carres = new List<Carre>();
        modules = new List<Module>();
        SetSerial();
        SetIndics();
        SetPorts();
        SetPiles();
        timer = Timer.Get;


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

    void SetPorts()
    {
        ports = new List<Port>();
        Port.Reset();
    }

    void SetIndics()
    {
        indicateurs = new List<Indic>();
        Indic.Reset();
    }

    void SetPiles()
    {
        piles = new List<Pile>();
    }

    public bool HasLightIndic(string ind)
    {
        foreach (Indic indic in indicateurs)
        {
            if (indic.Mention == ind && indic.lumiere.enabled)
            {
                return true;
            }
        }
        return false;
    }

    public bool HasPort(Port.Type p)
    {
        foreach (Port port in ports)
        {
            if (port.Nom == p)
            {
                return true;
            }
        }
        return false;
    }

    public void Verif()
    {
        bool defused = true;
        foreach (Module module in modules)
        {
            defused &= module.Desamorce;
        }
        if (defused)
        {
            Defused();
        }
    }

    public void Defused()
    {
        timer.defile = false;
    }

    public void Mort()
    {
        instance = null;
        Destroy(gameObject);
    }
}
