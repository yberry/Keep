using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Losange : MonoBehaviour {

    public const float tempsFlash = 0.6f;

    [SerializeField]
    private Light lumiere;

    private Simon module;
    private float temps = 0f;

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
        module = s;
    }

    void Clic()
    {
        Flash();
        module.Clic(this);
    }
}
