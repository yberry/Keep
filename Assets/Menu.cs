using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour {

    [SerializeField]
    private TMP_InputField timeInput = default;

    [SerializeField]
    private TMP_InputField modulesInput = default;

    private int time = GameManager.DEFAULT_TIME;
    private int modules = GameManager.DEFAULT_MODULES;

    public bool Hardcore { get; set; } = GameManager.DEFAULT_HARDCORE;
    public bool Needy { get; set; } = GameManager.DEFAULT_NEEDY;

    public void CheckTime(string t)
    {
        int intT = int.Parse(t);
        time = Mathf.Clamp(intT, 30, 600);
        timeInput.SetTextWithoutNotify(time.ToString());
    }

    public void CheckModules(string m)
    {
        int intM = int.Parse(m);
        modules = Mathf.Clamp(intM, 3, 11);
        modulesInput.SetTextWithoutNotify(modules.ToString());
    }

    public void GO()
    {
        GameManager.instance.SetProps(time, modules, Hardcore, Needy);
        SceneManager.LoadScene("Bombe");
    }

}
