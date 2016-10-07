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
    private int erreurs = 0;
    public int Erreurs
    {
        get
        {
            return erreurs;
        }
    }
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
    void Start()
    {
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

    void SetTimer()
    {
        GameObject t = Instantiate(preTimer.gameObject) as GameObject;
        timer = t.GetComponent<Timer>();
        timer.transform.SetParent(transform);
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
            GameObject p = Instantiate(filsHorizontaux.gameObject) as GameObject;
            p.transform.SetParent(transform);
            modules.Add(p.GetComponent<FilsHorizontaux>());
        }
        for (int i = 0; i < nbBouton; i++)
        {
            GameObject p = Instantiate(bouton.gameObject) as GameObject;
            p.transform.SetParent(transform);
            modules.Add(p.GetComponent<Bouton>());
        }
        for (int i = 0; i < nbSymboles; i++)
        {
            GameObject p = Instantiate(symboles.gameObject) as GameObject;
            p.transform.SetParent(transform);
            modules.Add(p.GetComponent<Symboles>());
        }
        for (int i = 0; i < nbSimon; i++)
        {
            GameObject p = Instantiate(simon.gameObject) as GameObject;
            p.transform.SetParent(transform);
            modules.Add(p.GetComponent<Simon>());
        }
        for (int i = 0; i < nbQui; i++)
        {
            GameObject p = Instantiate(quiEstLePremier.gameObject) as GameObject;
            p.transform.SetParent(transform);
            modules.Add(p.GetComponent<QuiEstLePremier>());
        }
        for (int i = 0; i < nbMemoire; i++)
        {
            GameObject p = Instantiate(memoire.gameObject) as GameObject;
            p.transform.SetParent(transform);
            modules.Add(p.GetComponent<Memoire>());
        }
        for (int i = 0; i < nbMorse; i++)
        {
            GameObject p = Instantiate(morse.gameObject) as GameObject;
            p.transform.SetParent(transform);
            modules.Add(p.GetComponent<Morse>());
        }
        for (int i = 0; i < nbVert; i++)
        {
            GameObject p = Instantiate(filsVerticaux.gameObject) as GameObject;
            p.transform.SetParent(transform);
            modules.Add(p.GetComponent<FilsVerticaux>());
        }
        for (int i = 0; i < nbSeq; i++)
        {
            GameObject p = Instantiate(sequencesFils.gameObject) as GameObject;
            p.transform.SetParent(transform);
            modules.Add(p.GetComponent<SequencesFils>());
        }
        for (int i = 0; i < nbLaby; i++)
        {
            GameObject p = Instantiate(labyrinthe.gameObject) as GameObject;
            p.transform.SetParent(transform);
            modules.Add(p.GetComponent<Labyrinthe>());
        }
        for (int i = 0; i < nbMdp; i++)
        {
            GameObject p = Instantiate(motDePasse.gameObject) as GameObject;
            p.transform.SetParent(transform);
            modules.Add(p.GetComponent<MotDePasse>());
        }
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
