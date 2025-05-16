using UnityEngine;

public class DesactivarPlanoPorToque : MonoBehaviour
{
    public GameObject planoEncontrador;

    void Update()
    {
        // Detecta toque en móvil o clic en editor
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            planoEncontrador.SetActive(false);
            Debug.Log("PlanoEncontrador desactivado por toque.");
        }
        else if (Input.GetMouseButtonDown(0)) // para prueba con mouse en editor
        {
            planoEncontrador.SetActive(false);
            Debug.Log("PlanoEncontrador desactivado por clic.");
        }
    }
}
