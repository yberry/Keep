using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Morse : Module {

    private static readonly Dictionary<char, string> alpha = new Dictionary<char, string>()
    {
        { 'a', ".-" },
        { 'b', "-..." },
        { 'c', "-.-." },
        { 'd', "-.." },
        { 'e', "." },
        { 'f', "..-." },
        { 'g', "--." },
        { 'h', "...." },
        { 'i', ".." },
        { 'j', ".---" },
        { 'k', "-.-" },
        { 'l', ".-.." },
        { 'm', "--" },
        { 'n', "-." },
        { 'o', "---" },
        { 'p', ".--." },
        { 'q', "--.-" },
        { 'r', ".-." },
        { 's', "..." },
        { 't', "-" },
        { 'u', "..-" },
        { 'v', "...-" },
        { 'w', ".--" },
        { 'x', "-..-" },
        { 'y', "-.--" },
        { 'z', "--.." },
        { '1', ".----" },
        { '2', "..---" },
        { '3', "...--" },
        { '4', "....-" },
        { '5', "....." },
        { '6', "-...." },
        { '7', "--..." },
        { '8', "---.." },
        { '9', "----." },
        { '0', "-----" }
    };

    private static readonly string[] motsPossibles = new string[]
    {
        "shell", "halls", "slick", "trick",
        "boxes", "leaks", "strobe", "bistro",
        "flick", "bombs", "break", "brick",
        "steak", "sting", "vector", "beats"
    };

    private static readonly float[] freqPossibles = new float[]
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
        slider.interactable = false;

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
        while (true)
        {
            foreach (char c in mot)
            {
                foreach (char signe in alpha[c])
                {
                    signal.enabled = true;
                    yield return new WaitForSeconds(tempsPoint * (signe == '.' ? 1f : 3f));
                    signal.enabled = false;
                    yield return new WaitForSeconds(tempsPoint);
                }
                yield return new WaitForSeconds(2f * tempsPoint);
            }
            yield return new WaitForSeconds(4f * tempsPoint);
        }
    }

    void Affiche()
    {
        affichageFreq.text = currentFreq + " MHz";
        slider.value = currentFreq;
    }

    void Gauche()
    {
        current = Mathf.Clamp(current - 1, 0, freqPossibles.Length - 1);
        Affiche();
    }

    void Droite()
    {
        current = Mathf.Clamp(current + 1, 0, freqPossibles.Length - 1);
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
