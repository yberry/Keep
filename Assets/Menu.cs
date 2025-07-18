using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public InputField time;
    public InputField modules;
    public Toggle hardcore;
    public Toggle needy;

	void Start ()
    {
        time.onEndEdit.AddListener(CheckTime);
        modules.onEndEdit.AddListener(CheckModules);
	}

    void CheckTime(string t)
    {
        int intT = int.Parse(t);
        time.text = Mathf.Clamp(intT, 30, 600).ToString();
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
