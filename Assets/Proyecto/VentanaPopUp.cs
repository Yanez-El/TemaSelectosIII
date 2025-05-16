using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class VentanaEmergente : MonoBehaviour
{
    public TMP_Text ventana; // Tu panel o ventana UI

    void Update()
    {
        // Detecta toque en móvil o clic en editor
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ventana.text = "Ahora toca un organelo para estudiarlo";
        }
    }
}