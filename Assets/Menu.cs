using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public InputField time;
    public InputField modules;
    public Toggle hardcore;
    public Toggle needy;

	// Use this for initialization
	void Start () {
        PlayerPrefs.SetInt("time", 60);
        PlayerPrefs.SetInt("modules", 3);
        PlayerPrefs.SetInt("hardcore", 0);
        PlayerPrefs.SetInt("needy", 0);

        time.onEndEdit.AddListener(CheckTime);
        modules.onEndEdit.AddListener(CheckModules);
	}

    void CheckTime(string t)
    {
        int intT = int.Parse(t);
        time.text = Mathf.Clamp(intT, 60, 600).ToString();
    }

    void CheckModules(string m)
    {
        int intM = int.Parse(m);
        modules.text = Mathf.Clamp(intM, 3, 11).ToString();
    }

    public void GO()
    {
        GameManager.instance.SetProps(int.Parse(time.text), int.Parse(modules.text), hardcore.isOn, needy.isOn);
        SceneManager.LoadScene("Bombe");
    }

}
