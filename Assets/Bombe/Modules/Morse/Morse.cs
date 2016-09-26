using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Morse : Module {

    enum M
    {
        Court,
        Long
    }

    private static Dictionary<char, M[]> alpha = new Dictionary<char, M[]>()
    {
        { 'a', new M[2] { M.Court, M.Long } },
        { 'b', new M[4] { M.Long, M.Court, M.Court, M.Court } },
        { 'c', new M[4] { M.Long, M.Court, M.Long, M.Court } },
        { 'd', new M[3] { M.Long, M.Court, M.Court } },
        { 'e', new M[1] { M.Court } },
        { 'f', new M[4] { M.Court, M.Court, M.Long, M.Court } },
        { 'g', new M[3] { M.Long, M.Long, M.Court } },
        { 'h', new M[4] { M.Court, M.Court, M.Court, M.Court } },
        { 'i', new M[2] { M.Court, M.Court } },
        { 'j', new M[4] { M.Court, M.Long, M.Long, M.Long } },
        { 'k', new M[3] { M.Long, M.Court, M.Long } },
        { 'l', new M[4] { M.Court, M.Long, M.Court, M.Court } },
        { 'm', new M[2] { M.Long, M.Long } },
        { 'n', new M[2] { M.Long, M.Court } },
        { 'o', new M[3] { M.Long, M.Long, M.Long } },
        { 'p', new M[4] { M.Court, M.Long, M.Long, M.Court } },
        { 'q', new M[4] { M.Long, M.Long, M.Court, M.Long } },
        { 'r', new M[3] { M.Court, M.Long, M.Court } },
        { 's', new M[3] { M.Court, M.Court, M.Court } },
        { 't', new M[1] { M.Long } },
        { 'u', new M[3] { M.Court, M.Court, M.Long } },
        { 'v', new M[4] { M.Court, M.Court, M.Court, M.Long } },
        { 'w', new M[3] { M.Court, M.Long, M.Long } },
        { 'x', new M[4] { M.Long, M.Court, M.Court, M.Long } },
        { 'y', new M[4] { M.Long, M.Court, M.Long, M.Long } },
        { 'z', new M[4] { M.Long, M.Long, M.Court, M.Court } },
        { '1', new M[5] { M.Court, M.Long, M.Long, M.Long, M.Long } },
        { '2', new M[5] { M.Court, M.Court, M.Long, M.Long, M.Long } },
        { '3', new M[5] { M.Court, M.Court, M.Court, M.Long, M.Long } },
        { '4', new M[5] { M.Court, M.Court, M.Court, M.Court, M.Long } },
        { '5', new M[5] { M.Court, M.Court, M.Court, M.Court, M.Court } },
        { '6', new M[5] { M.Long, M.Court, M.Court, M.Court, M.Court } },
        { '7', new M[5] { M.Long, M.Long, M.Court, M.Court, M.Court } },
        { '8', new M[5] { M.Long, M.Long, M.Long, M.Court, M.Court } },
        { '9', new M[5] { M.Long, M.Long, M.Long, M.Long, M.Court } },
        { '0', new M[5] { M.Long, M.Long, M.Long, M.Long, M.Long } }
    };

    private static Dictionary<string, float> corresp = new Dictionary<string, float>()
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

	// Use this for initialization
	void Start () {
        signal.enabled = false;

        string[] mots = new string[corresp.Keys.Count];
        corresp.Keys.CopyTo(mots, 0);

        mot = mots[Random.Range(0, mots.Length)];
        freq = corresp[mot];
        
        while (true)
        {
            StartCoroutine(LireMot(mot));
        }
	}
	
	IEnumerator LireMot(string m)
    {
        foreach (char c in m)
        {
            StartCoroutine(LireLettre(c));
            yield return new WaitForSeconds(3 * tempsPoint);
        }
        yield return new WaitForSeconds(4 * tempsPoint);
    }

    IEnumerator LireLettre(char c)
    {
        foreach (M bip in alpha[c])
        {
            StartCoroutine(LireSignal(bip));
            yield return new WaitForSeconds(tempsPoint);
        }
    }

    IEnumerator LireSignal(M bip)
    {
        signal.enabled = true;
        yield return new WaitForSeconds(tempsPoint * (bip == M.Court ? 1f : 3f));
        signal.enabled = false;
    }
}
