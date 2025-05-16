using UnityEngine;
using TMPro;  // Importa el espacio de nombres de TextMeshPro

public class SonidoYAnimacionOrganelo : MonoBehaviour
{
    public GameObject[] organelos;        // Array de los organelos (célula animal)
    public AudioClip[] audiosOrganelos;   // Array de AudioClips correspondientes a los organelos
    public TMP_Text nombreOrganeloText;   // Usar TMP_Text en lugar de Text (TextMeshPro)
    private AudioSource audioSource;      // Componente de AudioSource
    private AudioClip audioActual;        // El audio que se está reproduciendo actualmente
    private Animator animador;            // Componente Animator para animar los organelos

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();  // Añadir AudioSource al GameObject que usa este script
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject objetoTocado = hit.transform.gameObject;

                // Verifica si el objeto tocado es un organelo
                for (int i = 0; i < organelos.Length; i++)
                {
                    if (objetoTocado == organelos[i]) // Si el objeto tocado es el i-ésimo organelo
                    {
                        // Detener audio si ya está reproduciéndose
                        if (audioActual != audiosOrganelos[i])
                        {
                            if (audioSource.isPlaying)
                            {
                                audioSource.Stop();
                            }
                            audioSource.clip = audiosOrganelos[i];
                            audioSource.Play();
                            audioActual = audiosOrganelos[i];  // Actualizamos el audio actual
                            Debug.Log("Reproduciendo audio del organelo: " + audiosOrganelos[i].name);
                        }

                        // Animar el organelo tocado (si tiene Animator)
                        animador = objetoTocado.GetComponent<Animator>();
                        if (animador != null)
                        {
                            animador.SetTrigger("Toque");  // Activa una animación al tocar el organelo
                        }

                        // Actualizar el nombre del audio en el TextMeshPro
                        nombreOrganeloText.text = "Audio: " + audiosOrganelos[i].name;
                    }
                }
            }
        }
    }
}
