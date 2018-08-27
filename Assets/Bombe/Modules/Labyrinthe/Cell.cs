using UnityEngine;

public class Cell : MonoBehaviour
{
    public GameObject murHaut;
    public GameObject murGauche;
    public GameObject murBas;
    public GameObject murDroit;

    public Renderer square;
    public Transform triangle;
    public GameObject circle;

    private bool objective = false;
    private float angle = 0f;
    private Quaternion rotation = Quaternion.identity;

    private void Update()
    {
        if (objective)
        {
            angle += Time.deltaTime * Mathf.Deg2Rad * 50f;

            rotation.w = Mathf.Cos(angle);
            rotation.z = -Mathf.Sin(angle);

            triangle.localRotation = rotation;
        }
    }

    public void ShowMur(Laby.Dir dir)
    {
        switch (dir)
        {
            case Laby.Dir.Haut:
                murHaut.SetActive(true);
                break;

            case Laby.Dir.Gauche:
                murGauche.SetActive(true);
                break;

            case Laby.Dir.Bas:
                murBas.SetActive(true);
                break;

            case Laby.Dir.Droite:
                murDroit.SetActive(true);
                break;
        }
    }

    public void ShowSquare(bool show)
    {
        if (!objective)
        {
            square.material.SetColor("_Color", Color.white * (show ? 1f : 0.3f));
        }
    }

    public void ShowCircle()
    {
        circle.SetActive(true);
    }

    public void SetObjective()
    {
        triangle.gameObject.SetActive(true);
        objective = true;
    }


}