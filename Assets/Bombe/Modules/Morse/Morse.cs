using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;

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

    private static readonly Dictionary<string, float> englishWords = new Dictionary<string, float>()
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

    private static readonly Dictionary<string, float> frenchWords = new Dictionary<string, float>()
    {
        { "vitre", 3.505f },
        { "ville", 3.515f },
        { "chose", 3.522f },
        { "signe", 3.532f },
        { "linge", 3.535f },
        { "ligne", 3.542f },
        { "champ", 3.545f },
        { "litre", 3.552f },
        { "phase", 3.555f },
        { "chaud", 3.565f },
        { "bille", 3.572f },
        { "balle", 3.575f },
        { "singe", 3.582f },
        { "plume", 3.592f },
        { "pluie", 3.595f },
        { "salle", 3.600f }
    };

    private const float tempsPoint = 0.3f;

    [SerializeField]
    private Light signal;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private TMP_Text freqDisplay;
    [SerializeField]
    private Button gauche;
    [SerializeField]
    private Button droite;
    [SerializeField]
    private Button TX;

    private int current = 0;

    private string mot;
    private int index;

    private Coroutine lecture;

    private int Current
    {
        get => current;

        set
        {
            current = Mathf.Clamp(value, 0, englishWords.Count - 1);
            UpdateFreq();
        }
    }

    private float CurrentFreq => englishWords.ElementAt(current).Value;

    // Use this for initialization
    void Start () {
        signal.enabled = false;

        slider.wholeNumbers = false;
        slider.maxValue = englishWords.Values.Max();
        slider.minValue = englishWords.Values.Min();
        slider.interactable = false;

        index = Random.Range(0, englishWords.Count);
        mot = englishWords.ElementAt(index).Key;

        UpdateFreq();
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

    void UpdateFreq()
    {
        float cur = CurrentFreq;
        freqDisplay.text = string.Format("{0:F4} MHz", cur);
        slider.value = cur;
    }

    void Gauche()
    {
        --Current;
    }

    void Droite()
    {
        ++Current;
    }

    void Verif()
    {
        if (current == index)
        {
            Resolu();
        }
        else
        {
            Faute();
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        StopCoroutine(lecture);
    }
}
