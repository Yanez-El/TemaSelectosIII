using System.Collections;
using UnityEngine;
using Vuforia;

public class Mover : MonoBehaviour
{
    public GameObject model;
    public ObserverBehaviour[] ImageTargets;
    public int currentTarget;
    public float speed = 1.0f;
    private bool isMoving = false;

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
        if (closestTarget == null)
        {
            isMoving = false;
            yield break;
        }

        Vector3 startPosition = model.transform.position;
        Vector3 endPosition = closestTarget.transform.position;

        float journey = 0;

        while (journey <= 1f)
        {
            journey += Time.deltaTime * speed;
            model.transform.position = Vector3.Lerp(startPosition, endPosition, journey);
            yield return null;
        }

        // Al finalizar el movimiento, actualizar el target actual
        currentTarget = (currentTarget + 1) % ImageTargets.Length;
        isMoving = false;
    }

    private ObserverBehaviour GetClosestTarget()
    {
        ObserverBehaviour closestTarget = null;
        float closestDistance = Mathf.Infinity; // Inicializamos la distancia más corta como infinito

        foreach (ObserverBehaviour target in ImageTargets)
        {
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
}
