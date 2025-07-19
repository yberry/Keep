using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Simon : Module {

    private static Color[] couleursDispo = new Color[]
    {
        Color.red,
        Color.blue,
        Color.green,
        Color.yellow
    };

    private const float tempsBoucle = 1.5f;
    private const float tempsAttente = 3f;

    [SerializeField]
    private Losange rouge;
    [SerializeField]
    private Losange bleu;
    [SerializeField]
    private Losange vert;
    [SerializeField]
    private Losange jaune;

    private float tempsReponse = 0f;

    private bool reponseRecue = false;

    private int nbCombo;
    private int nbEtapes = 0;

    private List<Color> flashs;
    private List<Losange> signaux;
    private List<Losange> reponse;
    private List<Losange> reponseJoueur;
    private Dictionary<Color, Losange[]> corresp;

    // Use this for initialization
    void Start () {
        nbCombo = Random.Range(3, 6);

        rouge.SetModule(this);
        bleu.SetModule(this);
        vert.SetModule(this);
        jaune.SetModule(this);

        flashs = new List<Color>();
        signaux = new List<Losange>();
        reponse = new List<Losange>();
        reponseJoueur = new List<Losange>();

        if (Serial.instance.HasVowel)
        {
            corresp = new Dictionary<Color, Losange[]>()
            {
                { Color.red, new Losange[] { bleu, jaune, vert } },
                { Color.blue, new Losange[] { rouge, vert, rouge } },
                { Color.green, new Losange[] { jaune, bleu, jaune } },
                { Color.yellow, new Losange[] { vert, rouge, bleu } }
            };
        }
        else
        {
            corresp = new Dictionary<Color, Losange[]>()
            {
                { Color.red, new Losange[] { bleu, rouge, jaune } },
                { Color.blue, new Losange[] { jaune, bleu, vert } },
                { Color.green, new Losange[] { vert, jaune, bleu } },
                { Color.yellow, new Losange[] { rouge, vert, rouge } }
            };
        }

        AddColor();
	}

    void Update()
    {
        if (reponseRecue)
        {
            tempsReponse += Time.deltaTime;
            if (tempsReponse >= tempsAttente)
            {
                reponseRecue = false;
                reponseJoueur.Clear();
                tempsReponse = 0f;
                StartCoroutine(SendSignaux());
            }
        }
    }

    void AddColor()
    {
        Color color = couleursDispo.RandomItem();
        flashs.Add(color);

        if (color == Color.red)
        {
            signaux.Add(rouge);
        }
        else if (color == Color.blue)
        {
            signaux.Add(bleu);
        }
        else if (color == Color.green)
        {
            signaux.Add(vert);
        }
        else if (color == Color.yellow)
        {
            signaux.Add(jaune);
        }
        CheckReponse();
        StartCoroutine(SendSignaux());
    }

    public void CheckReponse()
    {
        reponse.Clear();
        int fautes = Bombe.instance.Erreurs;

        foreach (Color color in flashs)
        {
            reponse.Add(corresp[color][fautes]);
        }
    }

    IEnumerator SendSignaux()
    {
        while (true)
        {
            foreach (Losange losange in signaux)
            {
                losange.Flash();
                yield return new WaitForSeconds(Losange.tempsFlash);
            }
            yield return new WaitForSeconds(tempsBoucle);
        }
    }

    public void Clic(Losange losange)
    {
        reponseRecue = true;
        tempsReponse = 0f;
        StopCoroutine(SendSignaux());

        StopAllOthers(losange);

        if (reponse[reponseJoueur.Count] == losange)
        {
            reponseJoueur.Add(losange);
            if (reponse.Count == reponseJoueur.Count)
            {
                reponseRecue = false;
                ++nbEtapes;
                Verif();
            }
        }
        else
        {
            Faute();
            reponseJoueur.Clear();
            CheckReponse();
        }
    }

    void StopAllOthers(Losange losange)
    {
        Losange[] losanges = new Losange[] { vert, bleu, rouge, jaune };

        for (int i = 0; i < losanges.Length; i++)
        {
            if (losanges[i] != losange)
            {
                losange.Stop();
            }
        }
    }

    void Verif()
    {
        if (nbEtapes == nbCombo)
        {
            Resolu();
        }
        else
        {
            AddColor();
        }
    }
}
