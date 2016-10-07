using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public InputField time;
    public InputField modules;

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
        if (intT < 60)
        {
            time.text = "60";
            PlayerPrefs.SetInt("time", 60);
        }
        else if (intT > 600)
        {
            time.text = "600";
            PlayerPrefs.SetInt("time", 600);
        }
        else
        {
            PlayerPrefs.SetInt("time", intT);
        }
    }

    void CheckModules(string m)
    {
        int intM = int.Parse(m);
        if (intM < 3)
        {
            time.text = "3";
            PlayerPrefs.SetInt("modules", 3);
        }
        else if (intM > 11)
        {
            time.text = "11";
            PlayerPrefs.SetInt("modules", 11);
        }
        else
        {
            PlayerPrefs.SetInt("modules", intM);
        }
    }

    public void CheckHardcore(bool h)
    {
        PlayerPrefs.SetInt("hardcore", h ? 1 : 0);
    }

    public void CheckNeedy(bool n)
    {
        PlayerPrefs.SetInt("needy", n ? 1 : 0);
    }

    public void GO()
    {
        SceneManager.LoadScene("Bombe");
    }

}
