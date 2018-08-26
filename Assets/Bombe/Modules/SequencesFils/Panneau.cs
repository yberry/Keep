using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Panneau : MonoBehaviour {

    private SequencesFils sequence;

    public Text[] labelsFils;

    public bool Complete
    {
        get
        {
            return true;
        }
    }

    public void SetModule(SequencesFils seq)
    {
        sequence = seq;
    }

    public void SetLabels(int nb)
    {
        int start = 3 * nb + 1;
        for (int i = 0; i < 3; i++)
        {
            labelsFils[i].text = (start + i).ToString();
        }
    }
}
