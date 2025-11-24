using UnityEngine;

public class InitialPlatform : MonoBehaviour
{
    [Header("Configuración")]
    [Tooltip("Velocidad a la que se irá la plataforma (debería ser igual o similar a la del Scroller).")]
    public float moveSpeed = 5f;

    [Tooltip("Distancia hacia la derecha para detectar la siguiente plataforma.")]
    public float detectionDistance = 10f;

    [Tooltip("Capa (Layer) de las plataformas para que el rayo las detecte.")]
    public LayerMask groundLayer;

    private bool isMoving = false;

    void Update()
    {
        if (isMoving)
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

            if (transform.position.x < -15f)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            DetectIncomingPlatform();
        }
    }

    void DetectIncomingPlatform()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, detectionDistance, groundLayer);

        if (hit.collider != null)
        {
            Debug.Log("¡Plataforma detectada! Iniciando movimiento.");
            isMoving = true;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * detectionDistance);
    }
}