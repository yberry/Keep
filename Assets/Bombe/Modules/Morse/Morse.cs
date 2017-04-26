using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

    private static readonly Dictionary<string, float> motsFreqs = new Dictionary<string, float>()
    {
        { "shell", 3.505f },
        { "halls", 3.515f },
        { "slick", 3.522f },
        { "trick", 3.532f },
        { "boxes", 3.535f },
        { "leaks", 3.542f },
        { "strobe", 3.545f },
        { "bistro", 3.552f },
        { "flick", 3.555f },
        { "bombs", 3.565f },
        { "break", 3.572f },
        { "brick", 3.575f },
        { "steak", 3.582f },
        { "sting", 3.592f },
        { "vector", 3.595f },
        { "beats", 3.600f }
    };

    private int current = 0;
    private float currentFreq
    {
        get
        {
            return motsFreqs.ElementAt(current).Value;
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

    private Coroutine lecture;

	// Use this for initialization
	void Start () {
        signal.enabled = false;

        slider.wholeNumbers = false;
        slider.maxValue = motsFreqs.Values.Max();
        slider.minValue = motsFreqs.Values.Min();
        slider.interactable = false;

        KeyValuePair<string, float> pair = motsFreqs.RandomItem();
        mot = pair.Key;
        freq = pair.Value;

        Affiche();
        gauche.onClick.AddListener(Gauche);
        droite.onClick.AddListener(Droite);
        TX.onClick.AddListener(Verif);

        lecture = StartCoroutine(LireMot());
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
        float cur = currentFreq;
        affichageFreq.text = cur.ToString() + " MHz";
        slider.value = cur;
    }

    void Gauche()
    {
        current = Mathf.Clamp(current - 1, 0, motsFreqs.Count - 1);
        Affiche();
    }

    void Droite()
    {
        current = Mathf.Clamp(current + 1, 0, motsFreqs.Count - 1);
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

    void OnDestroy()
    {
        StopCoroutine(lecture);
    }
}
