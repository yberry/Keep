using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Morse : Module {

    private static Dictionary<char, bool[]> alpha
    {
        get
        {
            return new Dictionary<char, bool[]>()
            {
                { 'a', new bool[] { true, false } },
                { 'b', new bool[] { false, true, true, true } },
                { 'c', new bool[] { false, true, false, true } },
                { 'd', new bool[] { false, true, true } },
                { 'e', new bool[] { true } },
                { 'f', new bool[] { true, true, false, true } },
                { 'g', new bool[] { false, false, true } },
                { 'h', new bool[] { true, true, true, true } },
                { 'i', new bool[] { true, true } },
                { 'j', new bool[] { true, false, false, false } },
                { 'k', new bool[] { false, true, false } },
                { 'l', new bool[] { true, false, true, true } },
                { 'm', new bool[] { false, false } },
                { 'n', new bool[] { false, true } },
                { 'o', new bool[] { false, false, false } },
                { 'p', new bool[] { true, false, false, true } },
                { 'q', new bool[] { false, false, true, false } },
                { 'r', new bool[] { true, false, true } },
                { 's', new bool[] { true, true, true } },
                { 't', new bool[] { false } },
                { 'u', new bool[] { true, true, false } },
                { 'v', new bool[] { true, true, true, false } },
                { 'w', new bool[] { true, false, false } },
                { 'x', new bool[] { false, true, true, false } },
                { 'y', new bool[] { false, true, false, false } },
                { 'z', new bool[] { false, false, true, true } },
                { '1', new bool[] { true, false, false, false, false } },
                { '2', new bool[] { true, true, false, false, false } },
                { '3', new bool[] { true, true, true, false, false } },
                { '4', new bool[] { true, true, true, true, false } },
                { '5', new bool[] { true, true, true, true, true } },
                { '6', new bool[] { false, true, true, true, true } },
                { '7', new bool[] { false, false, true, true, true } },
                { '8', new bool[] { false, false, false, true, true } },
                { '9', new bool[] { false, false, false, false, true } },
                { '0', new bool[] { false, false, false, false, false } }
            };
        }
    }

    private static string[] motsPossibles
    {
        get
        {
            return new string[]
            {
                "shell", "halls", "slick", "trick",
                "boxes", "leaks", "strobe", "bistro",
                "flick", "bombs", "break", "brick",
                "steak", "sting", "vector", "beats"
            };
        }
    }

    private static float[] freqPossibles
    {
        get
        {
            return new float[]
            {
                3.505f, 3.515f, 3.522f, 3.532f,
                3.535f, 3.542f, 3.545f, 3.552f,
                3.555f, 3.565f, 3.572f, 3.575f,
                3.582f, 3.592f, 3.595f, 3.600f
            };
        }
    }

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
    public Slider slider;
    public Text affichageFreq;
    public Button gauche;
    public Button droite;
    public Button TX;

	// Use this for initialization
	void Start () {
        signal.enabled = false;

        slider.wholeNumbers = false;
        slider.maxValue = freqPossibles[freqPossibles.Length - 1];
        slider.minValue = freqPossibles[0];

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
            yield return new WaitForSeconds(2f * tempsPoint);
        }
        yield return new WaitForSeconds(4f * tempsPoint);
        StartCoroutine(LireMot());
    }

    void Affiche()
    {
        affichageFreq.text = currentFreq + " MHz";
        slider.value = currentFreq;
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
