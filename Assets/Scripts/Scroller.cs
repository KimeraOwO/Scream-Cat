using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    [Tooltip("La velocidad a la que se moverá el objeto hacia la izquierda.")]
    public float moveSpeed = 5f;

    [Tooltip("La posición X a la izquierda de la pantalla donde el objeto se reciclará.")]
    public float destroyXPosition = -15f;

    private PatternPooler poolManager;

    public void SetManager(PatternPooler manager)
    {
        poolManager = manager;
    }

    void Update()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

        if (transform.position.x < destroyXPosition)
        {
            if (poolManager != null)
            {
                poolManager.RecyclePattern(this.gameObject);
            }
        }
    }
}
