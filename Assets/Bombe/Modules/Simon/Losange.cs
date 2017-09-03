﻿using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Losange : MonoBehaviour {

    public const float tempsFlash = 0.6f;

    private Simon simon;
    private float temps = 0f;

    public Light lumiere;
    

	// Use this for initialization
	void Start () {
        lumiere.enabled = false;
        GetComponent<Button>().onClick.AddListener(Clic);
	}

    void Update()
    {
        if (lumiere.enabled)
        {
            temps += Time.deltaTime;
            if (temps >= tempsFlash)
            {
                Stop();
            }
        }
    }

    public void Flash()
    {
        lumiere.enabled = true;
    }

    public void Stop()
    {
        temps = 0f;
        lumiere.enabled = false;
    }
	
	public void SetModule(Simon s)
    {
        simon = s;
    }

    void Clic()
    {
        Flash();
        simon.Clic(this);
    }
}
