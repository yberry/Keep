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

        hardcore = PlayerPrefs.GetInt("hardcore") == 1;
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
        timer.SetStart(PlayerPrefs.GetInt("time"), hardcore);
        timer.defile = true;
    }

    void SetModules()
    {
        int nbModules = PlayerPrefs.GetInt("modules");

        int nbHori = 0;
        int nbBouton = 0;
        int nbSymboles = 0;
        int nbSimon = 0;
        int nbQui = 0;
        int nbMemoire = 0;
        int nbMorse = 0;
        int nbVert = 0;
        int nbSeq = 0;
        int nbLaby = 0;
        int nbMdp = 0;

        for (int i = 0; i < nbModules; i++)
        {
            switch (Random.Range(0, 11))
            {
                case 0:
                    //nbHori++;
                    i--;
                    break;

                case 1:
                    //nbBouton++;
                    i--;
                    break;

                case 2:
                    //nbSymboles++;
                    i--;
                    break;

                case 3:
                    nbSimon++;
                    break;

                case 4:
                    nbQui++;
                    break;

                case 5:
                    nbMemoire++;
                    break;

                case 6:
                    nbMorse++;
                    break;

                case 7:
                    //nbVert++;
                    i--;
                    break;

                case 8:
                    //nbSeq++;
                    i--;
                    break;

                case 9:
                    //nbLaby++;
                    i--;
                    break;

                case 10:
                    nbMdp++;
                    break;
            }
        }

        for (int i = 0; i < nbHori; i++)
        {
            FilsHorizontaux p = Instantiate(filsHorizontaux, transform);
            modules.Add(p);
        }
        for (int i = 0; i < nbBouton; i++)
        {
            Bouton p = Instantiate(bouton, transform);
            modules.Add(p);
        }
        for (int i = 0; i < nbSymboles; i++)
        {
            Symboles p = Instantiate(symboles, transform);
            modules.Add(p);
        }
        for (int i = 0; i < nbSimon; i++)
        {
            Simon p = Instantiate(simon, transform);
            modules.Add(p);
        }
        for (int i = 0; i < nbQui; i++)
        {
            QuiEstLePremier p = Instantiate(quiEstLePremier, transform);
            modules.Add(p);
        }
        for (int i = 0; i < nbMemoire; i++)
        {
            Memoire p = Instantiate(memoire, transform);
            modules.Add(p);
        }
        for (int i = 0; i < nbMorse; i++)
        {
            Morse p = Instantiate(morse, transform);
            modules.Add(p);
        }
        for (int i = 0; i < nbVert; i++)
        {
            FilsVerticaux p = Instantiate(filsVerticaux, transform);
            modules.Add(p);
        }
        for (int i = 0; i < nbSeq; i++)
        {
            SequencesFils p = Instantiate(sequencesFils, transform);
            modules.Add(p);
        }
        for (int i = 0; i < nbLaby; i++)
        {
            Labyrinthe p = Instantiate(labyrinthe, transform);
            modules.Add(p);
        }
        for (int i = 0; i < nbMdp; i++)
        {
            MotDePasse p = Instantiate(motDePasse, transform);
            modules.Add(p);
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
            foreach (Module module in modules)
            {
                if (module is Simon)
                {
                    Simon simon = module as Simon;
                    simon.CheckReponse();
                }
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
