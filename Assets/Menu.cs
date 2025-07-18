using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour {

    [SerializeField]
    private TMP_InputField timeInput = default;

    [SerializeField]
    private TMP_InputField modulesInput = default;

    private int time = 60;
    private int modules = 3;

    public bool Hardcore { get; set; } = false;
    public bool Needy { get; set; } = false;

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
