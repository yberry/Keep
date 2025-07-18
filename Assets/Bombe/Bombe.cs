using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class Bombe : Instance<Bombe> {

    #region NumSerie
    private const string charsSerie = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    private const int nbCharSerie = 6;
    private string numSerie;
    public bool NumPair
    {
        get
        {
            return (numSerie[nbCharSerie - 1] & 1) == 0;
        }
    }
    public bool Voyelle
    {
        get
        {
            return "AEIOUY".Any(c => numSerie.Contains(c));
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

    #region Places
    [Header("Places")]
    [Tooltip("Places pour carrés")]
    public List<Transform> places;
    private Transform RandomPlace
    {
        get
        {
            int index = Random.Range(0, places.Count);

            Transform place = places[index];

            places.RemoveAt(index);

            return place;
        }
    }
    #endregion

    #region Carres
    private Timer timer;
    private List<Carre> carres;
    private List<Module> modules;
    #endregion

    #region Fautes
    private bool hardcore = false;
    public int Erreurs { get; private set; }
    #endregion

    #region Peripheriques
    private List<Indic> indicateurs;
    private List<Port> ports;
    private List<Pile> piles;
    public int NbPiles
    {
        get
        {
            return piles.Sum(p => p.NbPiles);
        }
    }
    #endregion

    // Use this for initialization
    void Start()
    {
        Erreurs = 0;
        carres = new List<Carre>();
        modules = new List<Module>();
        SetSerial();
        SetIndics();
        SetPorts();
        SetPiles();

        hardcore = GameManager.instance.Hardcore;
        SetTimer();
        SetModules();
    }

    void SetSerial()
    {
        char[] tab = new char[nbCharSerie];
        for (int i = 0; i < nbCharSerie; i++)
        {
            tab[i] = RandomLetter(i == nbCharSerie - 1);
        }
        numSerie = new string(tab);
        textSerie.text = numSerie;
    }

    static char RandomLetter(bool last)
    {
        return charsSerie[Random.Range(last ? 26 : 0, 36)];
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
        Transform place = RandomPlace;
        timer = Instantiate(preTimer, place.position, place.rotation, transform);
        timer.SetStart(GameManager.instance.Time, hardcore);
        timer.defile = true;
        carres.Add(timer);
        Destroy(place.gameObject);
    }

    void SetModules()
    {
        int nbModules = GameManager.instance.Modules;

        Dictionary<Module, int> dico = new Dictionary<Module, int>()
        {
            { filsHorizontaux, 0 },
            //{ bouton, 0 },
            { symboles, 0 },
            { simon, 0 },
            { quiEstLePremier, 0 },
            { memoire, 0 },
            { morse, 0 },
            { filsVerticaux, 0 },
            //{ sequencesFils, 0 },
            { labyrinthe, 0 },
            { motDePasse, 0 }
        };

        Dictionary<Needy, int> needy = new Dictionary<Needy, int>()
        {
            //{ question, 0 },
            //{ condensateur, 0 },
            //{ knob, 0 }
        };

        for (int i = 0; i < nbModules; i++)
        {
            int rand = Random.Range(0, dico.Count);

            Module module = dico.ElementAt(rand).Key;
            dico[module]++;
        }

        foreach (KeyValuePair<Module, int> pair in dico)
        {
            for (int i = 0; i < pair.Value; i++)
            {
                Transform place = RandomPlace;
                Module module = Instantiate(pair.Key, place.position, place.rotation, transform);
                modules.Add(module);
                carres.Add(module);
                Destroy(place.gameObject);
            }
        }

        foreach (KeyValuePair<Needy, int> pair in needy)
        {
            for (int i = 0; i < pair.Value; i++)
            {
                Transform place = RandomPlace;
                carres.Add(Instantiate(pair.Key, place.position, place.rotation, transform));
                Destroy(place.gameObject);
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.transform == transform)
            {
                Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red);
            }
        }
    }

    public bool HasLightIndic(string ind)
    {
        return indicateurs.Any(i => i.Mention == ind && i.lumiere.enabled);
    }

    public bool HasPort(Port.Type t)
    {
        return ports.Any(p => p.Nom == t);
    }

    public void Verif()
    {
        if (modules.TrueForAll(m => m.Desamorce))
        {
            Defused();
        }
    }

    public void Erreur()
    {
        if (hardcore || ++Erreurs >= 3)
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
                    (module as Simon).CheckReponse();
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
        Destroy(gameObject);
    }
}
