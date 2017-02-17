using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class Bombe : MonoBehaviour {

    #region Bombe
    public static Bombe instance { get; private set; }
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
            return numSerie.Any(c => "AEIOUY".IndexOf(c) >= 0);
        }
    }
    [Header("Numéro de série")]
    [Tooltip("Affichage du numéro de série")]
    public Text textSerie;
    #endregion

    #region Prefabs
    [Tooltip("Préfab de timer")]
    public Timer preTimer;
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

    [Header("Préfabs de needy")]
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
    private bool hardcore = false;
    public int erreurs { get; private set; }
    #endregion

    #region Peripheriques
    private List<Indic> indicateurs;
    private List<Port> ports;
    private List<Pile> piles;
    public int NbPiles
    {
        get
        {
            return piles.Sum(p => p.nbPiles);
        }
    }
    #endregion

    // Use this for initialization
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        erreurs = 0;
        carres = new List<Carre>();
        modules = new List<Module>();
        SetSerial();
        SetIndics();
        SetPorts();
        SetPiles();

        hardcore = GameManager.instance.hardcore;
        SetTimer();
        SetModules();
    }

    void SetSerial()
    {
        numSerie = "";
        for (int i = 0; i < nbCharSerie; i++)
        {
            numSerie += RandomLetter(i == nbCharSerie - 1);
        }
        textSerie.text = numSerie;
    }

    char RandomLetter(bool last)
    {
        const string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
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

    void SetTimer()
    {
        timer = Instantiate(preTimer, transform);
        timer.SetStart(GameManager.instance.time, hardcore);
        timer.defile = true;
    }

    void SetModules()
    {
        int nbModules = GameManager.instance.modules;

        Dictionary<Module, int> dico = new Dictionary<Module, int>()
        {
            { filsHorizontaux, 0 },
            //{ bouton, 0 },
            //{ symboles, 0 },
            { simon, 0 },
            { quiEstLePremier, 0 },
            { memoire, 0 },
            { morse, 0 },
            { filsVerticaux, 0 },
            //{ sequencesFils, 0 },
            //{ labyrinthe, 0 },
            { motDePasse, 0 }
        };

        Dictionary<Needy, int> needy = new Dictionary<Needy, int>()
        {
            { question, 0 },
            { condensateur, 0 },
            { knob, 0 }
        };

        for (int i = 0; i < nbModules; i++)
        {
            int rand = Random.Range(0, dico.Count);

            Module module = dico.ElementAt(rand).Key;
            dico[module]++;
        }

        foreach (var pair in dico)
        {
            for (int i = 0; i < pair.Value; i++)
            {
                Module module = Instantiate(pair.Key, transform);
                modules.Add(module);
                carres.Add(module);
            }
        }

        foreach (var pair in needy)
        {
            for (int i = 0; i < pair.Value; i++)
            {
                carres.Add(Instantiate(pair.Key, transform));
            }
        }
    }

    public bool HasLightIndic(string ind)
    {
        return indicateurs.Any(i => i.mention == ind && i.lumiere.enabled);
    }

    public bool HasPort(Port.Type t)
    {
        return ports.Any(p => p.nom == t);
    }

    public void Verif()
    {
        if (modules.All(m => m.desamorce))
        {
            Defused();
        }
    }

    public void Erreur()
    {
        erreurs++;
        if (hardcore || erreurs >= 3)
        {
            Mort();
        }
        else
        {
            timer.Erreur();
            foreach (Simon simon in modules)
            {
                simon.CheckReponse();
            }
        }
    }

    void Defused()
    {
        timer.defile = false;
    }

    public void Mort()
    {
        instance = null;
        Destroy(gameObject);
    }
}
