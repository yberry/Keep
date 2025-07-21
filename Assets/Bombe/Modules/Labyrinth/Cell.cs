using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField]
    private GameObject murHaut;
    [SerializeField]
    private GameObject murGauche;
    [SerializeField]
    private GameObject murBas;
    [SerializeField]
    private GameObject murDroit;

    [SerializeField]
    private Renderer square;
    [SerializeField]
    private Transform triangle;
    [SerializeField]
    private GameObject circle;

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