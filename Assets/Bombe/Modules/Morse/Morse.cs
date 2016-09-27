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

    private static readonly Dictionary<string, float> corresp = new Dictionary<string, float>()
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
        { "beats", 3.600f },
    };

    private const float tempsPoint = 0.5f;

    private string mot;
    private float freq;

    public Light signal;
    public Slider freqSlider;
    public Button TX;

	// Use this for initialization
	void Start () {
        signal.enabled = false;

        string[] mots = new string[corresp.Keys.Count];
        corresp.Keys.CopyTo(mots, 0);

        mot = mots[Random.Range(0, mots.Length)];
        freq = corresp[mot];

        TX.onClick.AddListener(Verif);

        StartCoroutine(LireMot());
	}
	
	IEnumerator LireMot()
    {
        foreach (char c in mot)
        {
            StartCoroutine(LireLettre(c));
            yield return new WaitForSeconds(3 * tempsPoint);
        }
        yield return new WaitForSeconds(4 * tempsPoint);
        StartCoroutine(LireMot());
    }

    IEnumerator LireLettre(char c)
    {
        foreach (bool court in alpha[c])
        {
            StartCoroutine(LireSignal(court));
            yield return new WaitForSeconds(tempsPoint);
        }
    }

    IEnumerator LireSignal(bool court)
    {
        signal.enabled = true;
        yield return new WaitForSeconds(tempsPoint * (court ? 1f : 3f));
        signal.enabled = false;
    }

    void Verif()
    {
        if (freqSlider.value == freq)
        {
            Resolu();
        }
        else
        {
            Faute();
        }
    }
}
