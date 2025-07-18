using UnityEngine;

public class GameManager : Instance<GameManager> {

    public const int DEFAULT_TIME = 90;
    public const int DEFAULT_MODULES = 3;
    public const bool DEFAULT_HARDCORE = false;
    public const bool DEFAULT_NEEDY = false;

    public int Time { get; private set; } = DEFAULT_TIME;
    public int Modules { get; private set; } = DEFAULT_MODULES;
    public bool Hardcore { get; private set; } = DEFAULT_HARDCORE;
    public bool Needy { get; private set; } = DEFAULT_NEEDY;

    public void SetProps(int t, int m, bool h, bool n)
    {
        Time = t;
        Modules = m;
        Hardcore = h;
        Needy = n;
    }
}
