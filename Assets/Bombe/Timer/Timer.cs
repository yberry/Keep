using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Timer : Carre {

    [SerializeField]
    private bool defile = false;
    [SerializeField]
    private TextMeshPro counter;
    [SerializeField]
    private GameObject errorPanel;
    [SerializeField]
    private GameObject[] crosses;

    private float tempsDepart;
    private float temps;
    private string chiffres;

    private int errors;
    private float mult;

    private AudioSource source;

    // Use this for initialization
    void Start () {
        chiffres = "";
        errors = 0;
        mult = 1f;
        source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (defile)
        {
            temps -= Time.deltaTime * mult;
            if (temps <= 0f)
            {
                Bombe.instance.Mort();
            }
            SetChiffres();
        }
	}

    public void SetStart(int sec, bool hard)
    {
        tempsDepart = sec;
        temps = tempsDepart;
        errorPanel.SetActive(!hard);
        for (int i = 0; i < crosses.Length; i++)
        {
            crosses[i].SetActive(false);
        }
        SetChiffres();
    }

    public void StartTime()
    {
        defile = true;
    }

    public void StopTime()
    {
        defile = false;
    }

    void SetChiffres()
    {
        if (temps < 60f)
        {
            chiffres = temps.ToString("F2").PadLeft(5, '0');
        }
        else
        {
            int t = (int)temps;
            int sec = t % 60;
            int min = t / 60;

            chiffres = string.Format("{0:D2}:{1:D2}", min, sec);
        }

        counter.text = chiffres;
    }

    public bool HasNum(char num)
    {
        return chiffres.Contains(num);
    }

    public void AddError()
    {
        crosses[errors++].SetActive(true);
        mult += 0.25f;
    }
}
