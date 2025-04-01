using UnityEngine;

public class ControlVentanas : MonoBehaviour
{
    GameObject Ventana;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void mostrarPopUp()
    {
        Ventana.SetActive(true);
    }

    public void ocultarPopUp()
    {
        Ventana.SetActive(false);
    }
}
