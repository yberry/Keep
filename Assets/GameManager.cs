using UnityEngine;

public class GameManager : MonoBehaviour {

    public int Time { get; private set; }
    public int Modules { get; private set; }
    public bool Hardcore { get; private set; }
    public bool Needy { get; private set; }

    public static GameManager Instance { get; private set; }

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetProps(int t, int m, bool h, bool n)
    {
        Time = t;
        Modules = m;
        Hardcore = h;
        Needy = n;
    }
}
