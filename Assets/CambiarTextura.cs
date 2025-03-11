using UnityEngine;

public class CambiarTexturaBien : MonoBehaviour
{
    public GameObject model;  // El modelo al que se le cambiará la textura

    // Referencias a las 5 texturas que tienes en los Assets
    public Texture textura1;
    public Texture textura2;
    public Texture textura3;
    public Texture textura4;
    public Texture textura5;

    private int turnoTextura;
    private int tempTextura = 0;

    // Método que se llama cuando presionas el botón
    public void CambiarTextura_BTN()
    {
        // Selección aleatoria de la textura
        turnoTextura = Random.Range(1, 6);

        // Asegurarse de que la textura no se repita
        while (turnoTextura == tempTextura)
        {
            turnoTextura = Random.Range(1, 6);
        }

        tempTextura = turnoTextura;

        // Cambiar la textura de acuerdo al valor aleatorio
        switch (turnoTextura)
        {
            case 1:
                model.GetComponent<Renderer>().material.SetTexture("_MainTex", textura1);
                break;

            case 2:
                model.GetComponent<Renderer>().material.SetTexture("_MainTex", textura2);
                break;

            case 3:
                model.GetComponent<Renderer>().material.SetTexture("_MainTex", textura3);
                break;

            case 4:
                model.GetComponent<Renderer>().material.SetTexture("_MainTex", textura4);
                break;

            case 5:
                model.GetComponent<Renderer>().material.SetTexture("_MainTex", textura5);
                break;
        }

        
    }
}
