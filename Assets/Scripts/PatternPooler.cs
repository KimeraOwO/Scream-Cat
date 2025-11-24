using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PatternPooler : MonoBehaviour
{
    [Header("Configuración del Pool")]
    [Tooltip("La lista de prefabs (plataformas, enemigos, etc.) que queremos usar.")]
    public GameObject[] patternPrefabs;

    [Tooltip("El número total de objetos que existirán en la piscina (ej. 10 ó 15).")]
    public int poolSize = 10;

    private List<GameObject> patternPool;


    [Header("Configuración del Spawner")]
    [Tooltip("El tiempo (en segundos) entre la aparición de cada patrón.")]
    public float spawnRate = 2.5f;

    [Tooltip("La posición X donde aparecerán los patrones (fuera de la pantalla, a la derecha).")]
    public float spawnXPosition = 15f;

    [Tooltip("La altura mínima (Y) donde pueden aparecer.")]
    public float minYPosition = -3f;

    [Tooltip("La altura máxima (Y) donde pueden aparecer.")]
    public float maxYPosition = 3f;

    private float timer;

    void Start()
    {
        patternPool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            int randomPrefabIndex = Random.Range(0, patternPrefabs.Length);
            GameObject prefabToPool = patternPrefabs[randomPrefabIndex];

            GameObject obj = Instantiate(prefabToPool);

            obj.GetComponent<Scroller>().SetManager(this);

            obj.SetActive(false);
            patternPool.Add(obj);
        }
        timer = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnRate)
        {

            SpawnPattern();

            timer = 0;
        }
    }

    void SpawnPattern()
    {
        if (patternPool.Count == 0)
        {
            Debug.LogWarning("La piscina está vacía. Aumenta el poolSize.");
            return;
        }

        int randomIndex = Random.Range(0, patternPool.Count);
        GameObject patternToSpawn = patternPool[randomIndex];

        patternPool.RemoveAt(randomIndex);

        float randomY = Random.Range(minYPosition, maxYPosition);
        patternToSpawn.transform.position = new Vector3(spawnXPosition, randomY, 0);

        patternToSpawn.SetActive(true);
    }

    public void RecyclePattern(GameObject patternToRecycle)
    {
        patternToRecycle.SetActive(false);

        patternPool.Add(patternToRecycle);
    }
}