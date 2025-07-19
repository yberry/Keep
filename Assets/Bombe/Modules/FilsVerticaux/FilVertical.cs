using UnityEngine;
using System.Collections.Generic;

public class FilVertical : MonoBehaviour {

    private static readonly Color[] couleursDispo = new Color[]
    {
        Color.blue,
        Color.red,
        Color.white,
        Color.yellow
    };

    [SerializeField]
    private Light LED;
    [SerializeField]
    private GameObject etoile;

    private FilsVerticaux fils;
    private List<Color> couleurs;
    private bool aCouper = false;
    private bool estCoupe = false;

    public bool Complet
    {
        get
        {
            return !aCouper || estCoupe;
        }
    }

	// Use this for initialization
	void Start () {
        Restart();
	}

    public void SetModule(FilsVerticaux f)
    {
        fils = f;
    }

    public void Restart()
    {
        int nbCouleurs = Random.Range(1, 3);
        couleurs = new List<Color>();
        for (int i = 0; i < nbCouleurs; i++)
        {
            int rand;

            do
            {
                rand = Random.Range(0, couleursDispo.Length);
            }
            while (couleurs.Contains(couleursDispo[rand]));

            couleurs.Add(couleursDispo[rand]);
        }
        LED.enabled = Bombe.HeadsOrTails;
        etoile.SetActive(Bombe.HeadsOrTails);
        Objectif();
    }

    void Objectif()
    {
        bool b = couleurs.Contains(Color.blue);
        bool r = couleurs.Contains(Color.red);
        bool l = LED.enabled;
        bool e = etoile.activeInHierarchy;

        if (!b && !l && (e || !r))
        {
            aCouper = true;
        }

        else if (b && ((e && r && !l) || (l && !r)))
        {
            aCouper = Bombe.instance.HasPort(Port.Type.Parallele);
        }

        else if (!e && ((r && (b || !l)) || (!r && b && !l)))
        {
            aCouper = Serial.instance.LastEven;
        }

        else if (!b && l && (r || e))
        {
            aCouper = Bombe.instance.NbPiles >= 2;
        }

        else
        {
            aCouper = false;
        }
    }

    public void Desaffiche()
    {
        LED.enabled = false;
        etoile.SetActive(false);
        gameObject.SetActive(false);
    }

    public void Coupe()
    {
        if (estCoupe)
        {
            return;
        }

        estCoupe = true;

        if (aCouper)
        {
            fils.Verif();
        }
        else
        {
            fils.Faute();
        }
    }
}
