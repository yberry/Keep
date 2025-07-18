using UnityEngine;

public class GameManager : Instance<GameManager> {

    public int Time { get; private set; }
    public int Modules { get; private set; }
    public bool Hardcore { get; private set; }
    public bool Needy { get; private set; }

    public void SetProps(int t, int m, bool h, bool n)
    {
        Time = t;
        Modules = m;
        Hardcore = h;
        Needy = n;
    }
}
