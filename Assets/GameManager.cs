using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int time { get; private set; }
    public int modules { get; private set; }
    public bool hardcore { get; private set; }
    public bool needy { get; private set; }

    public static GameManager instance { get; private set; }

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetProps(int t, int m, bool h, bool n)
    {
        time = t;
        modules = m;
        hardcore = h;
        needy = n;
    }
}
