using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Bombe : Instance<Bombe> {

    public static bool HeadsOrTails => Random.Range(0, 2) == 0;

    #region NumSerie
    [Header("Numéro de série")]
    [SerializeField, Tooltip("Affichage du numéro de série")]
    private Serial serial;
    #endregion

#if UNITY_EDITOR
    #region Debug
    [Header("Debug")]
    [SerializeField]
    private bool debugMode = false;
    [SerializeField]
    private bool debugHoriWires = false;
    [SerializeField]
    private bool debugBigButton = false;
    [SerializeField]
    private bool debugSymbols = false;
    [SerializeField]
    private bool debugSimon = false;
    [SerializeField]
    private bool debugWhoFirst = false;
    [SerializeField]
    private bool debugMemory = false;
    [SerializeField]
    private bool debugMorse = false;
    [SerializeField]
    private bool debugVertWires = false;
    [SerializeField]
    private bool debugSequence = false;
    [SerializeField]
    private bool debugLabyrinth = false;
    [SerializeField]
    private bool debugPassword = false;
    [SerializeField]
    private bool debugQuestion = false;
    [SerializeField]
    private bool debugCondenser = false;
    [SerializeField]
    private bool debugKnob = false;
    #endregion
#endif

    #region Prefabs
    [Header("Préfabs de modules")]
    [SerializeField, Tooltip("Préfab de timer")]
    private Timer preTimer;
    [SerializeField, Tooltip("Préfab de module vide")]
    private Transform dummyPrefab;
    [SerializeField, Tooltip("Préfab de fils horizontaux")]
    private HorizontalWires filsHorizontaux;
    [SerializeField, Tooltip("Préfab de bouton")]
    private Bouton bouton;
    [SerializeField, Tooltip("Préfab de symboles")]
    private Symbols symboles;
    [SerializeField, Tooltip("Préfab de simon")]
    private Simon simon;
    [SerializeField, Tooltip("Préfab de qui est le premier")]
    private QuiEstLePremier quiEstLePremier;
    [SerializeField, Tooltip("Préfab de memoire")]
    private Memoire memoire;
    [SerializeField, Tooltip("Préfab de morse")]
    private Morse morse;
    [SerializeField, Tooltip("Préfab de fils verticaux")]
    private FilsVerticaux filsVerticaux;
    [SerializeField, Tooltip("Préfab de séquences de fils")]
    private SequencesFils sequencesFils;
    [SerializeField, Tooltip("Préfab de labyrinthe")]
    private Labyrinthe labyrinthe;
    [SerializeField, Tooltip("Préfab de mot de passe")]
    private MotDePasse motDePasse;

    [Header("Préfabs de needy")]
    [SerializeField, Tooltip("Préfab de question")]
    private Question question;
    [SerializeField, Tooltip("Préfab de condensateur")]
    private Condensateur condensateur;
    [SerializeField, Tooltip("Préfab de knob")]
    private Knob knob;
    #endregion

    #region Places
    [Header("Places")]
    [SerializeField, Tooltip("Places pour carrés")]
    private List<Transform> places;
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
        serial.Generate();
        SetIndics();
        SetPorts();
        SetPiles();

        hardcore = GameManager.instance.Hardcore;
        SetTimer();
#if UNITY_EDITOR
        if (debugMode)
        {
            SetDebugModules();
        }
        else
        {
            SetModules();
        }
#else
        SetModules();
#endif
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
        timer.StartTime();
        carres.Add(timer);
        Destroy(place.gameObject);
    }

    private void SetModules()
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

        for (int i = 0; i < places.Count; i++)
        {
            Transform place = places[i];
            Instantiate(dummyPrefab, place.position, place.rotation, transform);
        }
    }

#if UNITY_EDITOR
    private void SetDebugModules()
    {
        List<Module> preModules = new List<Module>();

        if (debugHoriWires)
        {
            preModules.Add(filsHorizontaux);
        }
        if (debugBigButton)
        {
            preModules.Add(bouton);
        }
        if (debugSymbols)
        {
            preModules.Add(symboles);
        }
        if (debugSimon)
        {
            preModules.Add(simon);
        }
        if (debugWhoFirst)
        {
            preModules.Add(quiEstLePremier);
        }
        if (debugMemory)
        {
            preModules.Add(memoire);
        }
        if (debugMorse)
        {
            preModules.Add(morse);
        }
        if (debugVertWires)
        {
            preModules.Add(filsVerticaux);
        }
        if (debugSequence)
        {
            preModules.Add(sequencesFils);
        }
        if (debugLabyrinth)
        {
            preModules.Add(labyrinthe);
        }
        if (debugPassword)
        {
            preModules.Add(morse);
        }

        if (preModules.Count == 0)
        {
            preModules.Add(filsHorizontaux);
        }

        List<Needy> needy = new List<Needy>();
        if (debugQuestion)
        {
            needy.Add(question);
        }
        if (debugCondenser)
        {
            needy.Add(condensateur);
        }
        if (debugKnob)
        {
            needy.Add(knob);
        }
        
        while (preModules.Count + needy.Count > 11)
        {
            needy.RemoveRandom();
        }

        foreach (Module preModule in preModules)
        {
            Transform place = RandomPlace;
            Module module = Instantiate(preModule, place.position, place.rotation, transform);
            modules.Add(module);
            carres.Add(module);
            Destroy(place.gameObject);
        }

        foreach (Needy need in needy)
        {
            Transform place = RandomPlace;
            carres.Add(Instantiate(need, place.position, place.rotation, transform));
            Destroy(place.gameObject);
        }

        for (int i = 0; i < places.Count; i++)
        {
            Transform place = places[i];
            Instantiate(dummyPrefab, place.position, place.rotation, transform);
        }
    }
#endif

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform == transform)
            {
                Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red);
            }
        }
    }

    public bool HasLightIndic(string ind)
    {
        return indicateurs.Any(i => i.Mention == ind && i.lumiere.enabled);
    }

    public bool HasTimerNum(char num)
    {
        return timer.HasNum(num);
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
            timer.AddError();
            foreach (Module module in modules)
            {
                if (module is Simon simon)
                {
                    simon.CheckReponse();
                }
            }
        }
    }

    void Defused()
    {
        timer.StopTime();
    }

    public void Mort()
    {
        Destroy(gameObject);
    }
}
