using UnityEngine;


public class CambiarColorBien : MonoBehaviour
{
    public GameObject model;
    public Color color;
    public Material colorMaterial;
    public int turnoColor, tempColor = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CambiarColor_BTN()
    {
        turnoColor = Random.Range(1, 6);
        while (turnoColor == tempColor)
        {
            turnoColor = Random.Range(1, 6);
        }
        tempColor = turnoColor;
        switch (turnoColor)
        {
            case 1:
                model.GetComponent<Renderer>().material.color = Color.white;
                break;

            case 2:
                model.GetComponent<Renderer>().material.color = Color.gray;
                break;

            case 3:
                model.GetComponent<Renderer>().material.color = Color.red;
                break;

            case 4:
                model.GetComponent<Renderer>().material.color = Color.green;
                break;

            case 5:
                model.GetComponent<Renderer>().material.color = Color.blue;
                break;

        }
        colorMaterial.color = color;
    }
}
