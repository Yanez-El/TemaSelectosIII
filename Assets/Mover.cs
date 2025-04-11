using System.Collections;
using UnityEngine;
using Vuforia;
using TMPro;

public class Mover : MonoBehaviour
{
    public GameObject model;                // El objeto que se moverá
    public ObserverBehaviour[] ImageTargets; // Array de ImageTargets
    public int currentTarget;               // Índice del target actual
    public float speed = 1.0f;              // Velocidad de movimiento
    private bool isMoving = false;          // Bandera para evitar que se mueva mientras ya se está moviendo

    // Variable para reiniciar el juego
    public Vector3 originalPosition;  // Posición original del modelo (o de su primer ImageTarget)
    public Transform originalParent;  // El ImageTarget original al que se debe volver el modelo


    // Variable para referirse al GameObject específico que queremos hacer desaparecer
    public GameObject targetObjectToHide;   // El objeto que se va a hacer invisible
    public TextMeshProUGUI popupText;
    private bool alreadyVisited = false;

    public void Start()
    {
        targetObjectToHide.SetActive(false);
    }

    // Función para mover al siguiente ImageTarget

    private void Update()
    {
        // Comprobar si el modelo está emparejado con su ImageTarget original
        if (model.transform.parent == originalParent)
        {
            // Si el modelo está emparejado con el ImageTarget original, reiniciamos la bandera
            alreadyVisited = false;  // Reinicia la bandera para que el modelo pueda visitar el target original nuevamente
        }
    }
    public void moveToNextMarker()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveModel());
        }
    }

    private IEnumerator MoveModel()
    {
        isMoving = true;

        // Encuentra el ImageTarget más cercano
        ObserverBehaviour closestTarget = GetClosestTarget();
        if (closestTarget.gameObject.name == "nota1")
        {
            alreadyVisited = false;
        }

        if (closestTarget == null || alreadyVisited == true)
        {
            isMoving = false;
            yield break;
        }


        // Obtén las posiciones inicial y final
        Vector3 startPosition = model.transform.position;
        Vector3 endPosition = closestTarget.transform.position;

        float journey = 0;

        // Mueve el modelo hasta el ImageTarget más cercano
        while (journey <= 1f)
        {
            journey += Time.deltaTime * speed;
            if (closestTarget.gameObject.name == "nota6")
            {
                model.transform.position = Vector3.Lerp(startPosition, endPosition + (Vector3.back * 150f), journey);
            }
            else
            {
                model.transform.position = Vector3.Lerp(startPosition, endPosition, journey);
            }

            // Cuando el modelo esté lo suficientemente cerca, muestra el objeto asociado al ImageTarget
            if (Vector3.Distance(model.transform.position, endPosition) < 250f)
            {
                if (targetObjectToHide != null)
                {
                    targetObjectToHide.SetActive(true);  // Hacer el objeto visible
                }
            }

            yield return null;
        }

        // Una vez que el modelo llega al objetivo, espera 1 segundo
        yield return new WaitForSeconds(1f);

        // Desactivar el objeto original del ImageTarget
        if (targetObjectToHide != null)
        {
            targetObjectToHide.SetActive(false);
        }

        if (popupText != null)
        {
            string targetName = closestTarget.gameObject.name; // Obtén el nombre del ImageTarget

            switch (targetName)
            {
                case "nota1":
                    popupText.text = "¿Cuál es el toro mecánico? Selecciona tu respuesta";
                    break;
                case "ImageTarget":
                    popupText.text = "¡Muy bien! Ahora, ¿Cuál es el gato mecánico?";
                    break;
                case "nota2":
                    popupText.text = "Mimikyu falló al identificar al toro mecánico. Encontró un arma y explotó. Vuelve a comenzar";
                    ResetGame();
                    break;
                case "nota3":
                    popupText.text = "Lo estás haciendo bien. ¡Solo debes adivinar quién es el verdadero Sportacus y ganarás!";
                    break;
                case "nota4":
                    popupText.text = "Aquí no hay respuesta incorrecta. Los dos fueron grandes. Avanza al final";
                    break;
                case "nota5":
                    popupText.text = "Aquí no hay respuesta incorrecta. Los dos fueron grandes. Avanza al final";
                    break;
                case "nota6":
                    popupText.text = "¡Lograste superar las pruebas. Eres grande, Drake. Ahora, ¿no tendrás algo de comer de casualidad?";
                    break;
                case "inventario":
                    popupText.text = "Mimikyu encontró una granada que confundió con un aguacate. Explotó. Vuelve a comenzar.";
                    ResetGame();
                    break;

                // Agrega más casos según los nombres de tus ImageTargets
                default:
                    popupText.text = "Marcador no reconocido.";
                    break;
            }
        }

        // El modelo ahora se convierte en hijo del ImageTarget
        model.transform.SetParent(closestTarget.transform);

        // Actualiza el target actual
        alreadyVisited = true;
        currentTarget = (currentTarget + 1) % ImageTargets.Length;
        isMoving = false;
    }

    // Encuentra el ImageTarget más cercano al modelo
    private ObserverBehaviour GetClosestTarget()
    {
        ObserverBehaviour closestTarget = null;
        float closestDistance = Mathf.Infinity; // Inicializamos la distancia más corta como infinito

        foreach (ObserverBehaviour target in ImageTargets)
        {
            // Verifica si el target está siendo rastreado
            if (target != null && (target.TargetStatus.Status == Status.TRACKED || target.TargetStatus.Status == Status.EXTENDED_TRACKED))
            {
                float distance = Vector3.Distance(model.transform.position, target.transform.position); // Calcula la distancia

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = target; // Actualiza el target más cercano
                }
            }
        }

        return closestTarget;
    }

    private void ResetGame()
    {

        //model.transform.localPosition = Vector3.zero;  // Esto asegura que el modelo vuelva a su posición original relativa al ImageTarget

        //// Restablecer la jerarquía, asegurándonos de que el modelo se convierta en hijo del ImageTarget original
        //model.transform.SetParent(originalParent);

        // Restablecer la posición del modelo
        model.transform.position = originalParent.position;

        // Restablecer el modelo a su ImageTarget original
        model.transform.SetParent(originalParent);

        // Restablecer las banderas de los ImageTargets
        alreadyVisited = false;

        // También puedes resetear cualquier otro estado que necesites
        currentTarget = 0; // Reiniciar el índice del target
    }
}

