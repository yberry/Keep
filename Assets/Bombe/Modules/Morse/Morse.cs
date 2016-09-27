using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Morse : Module {

    private static readonly Dictionary<char, bool[]> alpha = new Dictionary<char, bool[]>()
    {
        { 'a', new bool[2] { true, false } },
        { 'b', new bool[4] { false, true, true, true } },
        { 'c', new bool[4] { false, true, false, true } },
        { 'd', new bool[3] { false, true, true } },
        { 'e', new bool[1] { true } },
        { 'f', new bool[4] { true, true, false, true } },
        { 'g', new bool[3] { false, false, true } },
        { 'h', new bool[4] { true, true, true, true } },
        { 'i', new bool[2] { true, true } },
        { 'j', new bool[4] { true, false, false, false } },
        { 'k', new bool[3] { false, true, false } },
        { 'l', new bool[4] { true, false, true, true } },
        { 'm', new bool[2] { false, false } },
        { 'n', new bool[2] { false, true } },
        { 'o', new bool[3] { false, false, false } },
        { 'p', new bool[4] { true, false, false, true } },
        { 'q', new bool[4] { false, false, true, false } },
        { 'r', new bool[3] { true, false, true } },
        { 's', new bool[3] { true, true, true } },
        { 't', new bool[1] { false } },
        { 'u', new bool[3] { true, true, false } },
        { 'v', new bool[4] { true, true, true, false } },
        { 'w', new bool[3] { true, false, false } },
        { 'x', new bool[4] { false, true, true, false } },
        { 'y', new bool[4] { false, true, false, false } },
        { 'z', new bool[4] { false, false, true, true } },
        { '1', new bool[5] { true, false, false, false, false } },
        { '2', new bool[5] { true, true, false, false, false } },
        { '3', new bool[5] { true, true, true, false, false } },
        { '4', new bool[5] { true, true, true, true, false } },
        { '5', new bool[5] { true, true, true, true, true } },
        { '6', new bool[5] { false, true, true, true, true } },
        { '7', new bool[5] { false, false, true, true, true } },
        { '8', new bool[5] { false, false, false, true, true } },
        { '9', new bool[5] { false, false, false, false, true } },
        { '0', new bool[5] { false, false, false, false, false } }
    };

    private static readonly string[] motsPossibles =
    {
        "shell", "halls", "slick", "trick",
        "boxes", "leaks", "strobe", "bistro",
        "flick", "bombs", "break", "brick",
        "steak", "sting", "vector", "beats"
    };

    private static readonly float[] freqPossibles =
    {
        3.505f, 3.515f, 3.522f, 3.532f,
        3.535f, 3.542f, 3.545f, 3.552f,
        3.555f, 3.565f, 3.572f, 3.575f,
        3.582f, 3.592f, 3.595f, 3.600f
    };

    private int current = 0;
    private float currentFreq
    {
        get
        {
            return freqPossibles[current];
        }
    }

    private const float tempsPoint = 0.3f;

    private string mot;
    private float freq;

    public Light signal;
    public Text affichageFreq;
    public Button gauche;
    public Button droite;
    public Button TX;

	// Use this for initialization
	void Start () {
        signal.enabled = false;

        int rand = Random.Range(0, motsPossibles.Length);
        mot = motsPossibles[rand];
        freq = freqPossibles[rand];

        Affiche();
        gauche.onClick.AddListener(Gauche);
        droite.onClick.AddListener(Droite);
        TX.onClick.AddListener(Verif);

        StartCoroutine(LireMot());
	}
	
	IEnumerator LireMot()
    {
        foreach (char c in mot)
        {
            foreach (bool court in alpha[c])
            {
                signal.enabled = true;
                yield return new WaitForSeconds(tempsPoint * (court ? 1f : 3f));
                signal.enabled = false;
                yield return new WaitForSeconds(tempsPoint);
            }
            yield return new WaitForSeconds(3 * tempsPoint);
        }
        yield return new WaitForSeconds(4 * tempsPoint);
        StartCoroutine(LireMot());
    }

    void Affiche()
    {
        affichageFreq.text = currentFreq + " MHz";
    }

    void Gauche()
    {
        current--;
        if (current < 0)
        {
            current = 0;
        }
        Affiche();
    }

    void Droite()
    {
        current++;
        if (current >= freqPossibles.Length)
        {
            current = freqPossibles.Length - 1;
        }
        Affiche();
    }

    void Verif()
    {
        if (currentFreq == freq)
        {
            Resolu();
        }
        else
        {
            Faute();
        }
    }
}
