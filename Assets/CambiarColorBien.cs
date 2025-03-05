using UnityEngine;


public class CambiarColorBien : MonoBehaviour
{
    public GameObject model;
    public Color color;
    public Material colorMaterial;
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
        model.GetComponent<Renderer>().material.color = color;
        colorMaterial.color = color;
    }
}
