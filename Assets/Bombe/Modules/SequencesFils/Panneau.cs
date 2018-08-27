﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Panneau : MonoBehaviour {

    public struct Fil
    {
        public Color color;
        public int target;
        public bool mustCut;
        public bool isCut;
    }

    private SequencesFils sequence;
    private List<Fil> fils;

    public Text[] labelsFils;

    public bool Complete
    {
        get
        {
            return fils.TrueForAll(f => !f.mustCut || f.isCut);
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

    public void SetFils(IList<Fil?> list)
    {

    }
}
